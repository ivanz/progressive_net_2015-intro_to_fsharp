namespace FsWebApiExample.Dtos

open System
open FsWebApiExample.Rop
open FsWebApiExample.DomainPrimitiveTypes
open FsWebApiExample.DomainModel

// ============================== 
// DTOs
// ============================== 

/// Represents a DTO that is exposed on the wire.
/// This is a regular POCO class which can be null. 
/// To emulate the C# class, all the properties are initialized to null by default
///
/// Note that in F# you have to make quite an effort to create nullable classes with nullable fields
[<AllowNullLiteralAttribute>]
type ExtraDto() = 
    member val Key: string = null with get, set
    member val Value: string = null with get, set

[<AllowNullLiteralAttribute>]
type CustomerDto() = 
    member val Id = 0 with get, set
    member val FirstName : string = null with get, set
    member val LastName : string = null with get, set
    member val Email : string  = null with get, set
    member val Extras : ExtraDto[]  = null with get, set


// ============================== 
// DTO Converters
// ============================== 

module DtoConverter =

    let extraFromDto (dto:ExtraDto) = 
        createExtra 
        <!> (String10.create dto.Key)
        <*> (String10.create dto.Value)

    let extrasFromDtos (dtos:ExtraDto[]) = 
        if (dtos = null) then
            succeed []
        else
            let isFailure = either (fun _ -> false)(fun _ -> true)
            let successOnly = either (fun (x,_) -> Some x)(fun _ -> None)
            let results =
                dtos
                |> Array.map extraFromDto 
                |> Array.toList
            if List.exists isFailure results then
                fail CustomerIsRequired
            else
                results 
                |> List.choose successOnly 
                |> succeed
            

    /// Convert a DTO into a domain customer.
    ///
    /// We MUST handle the possibility of one or more errors
    /// because the Customer type has stricter constraints than CustomerDto
    /// and the conversion might fail.
    let dtoToCustomer (dto: CustomerDto) = 
        if dto = null then 
            fail CustomerIsRequired
        else
            // This is an example of the power of composition!
            // Each step returns a value OR an error.
            // These are then gradually combined to make bigger things, all the while preserving any errors 
            // that happen.

            // if the id is not valid, the createCustomerId function will return a Failure
            // hover over idOrError and you can see it has type RopResult<CustomerId,DomainEvent> rather than just CustomerId
            let idOrError = createCustomerId dto.Id

            // similarly for first and last name
            let firstNameOrError = createFirstName dto.FirstName
            let lastNameOrError = createLastName dto.LastName

            // the "createPersonalName" functions takes normal inputs, not inputs with errors, 
            // but we can use the "lift" function to convert it into one that does handle error input
            // the output has also changed from a normal name to one with errors
            let personalNameOrError = lift2R createPersonalName firstNameOrError lastNameOrError 

            // similarly try to make an email
            let emailOrError = createEmail dto.Email

            let extrasOrError = extrasFromDtos dto.Extras

            // finally add them all together to make a customers
            // the "createCustomer" takes three params, so use lift3 to convert it
            let customerOrError = lift4R createCustomer idOrError personalNameOrError emailOrError extrasOrError 
            customerOrError 

    // The code above is very explicit and was designed for beginners to understand.
    // Below is a more idiomatic version which uses the <!> and <*> operators rather than "lift".
    //
    // The <!> and <*> operators make it look complicated, but in fact it is always the same pattern.
    //  <!> is used for the first param
    //  <*> is used for the subsequent params
    //
    // so for example:
    //   existingFunction <!> firstParam <*> secondParam <*> thirdParam
    let dtoToCustomerIdiomatic (dto: CustomerDto) =
        if dto = null then 
            fail CustomerIsRequired
        else
            let customerIdOrError = 
                createCustomerId dto.Id

            let nameOrError = 
                createPersonalName
                <!> createFirstName dto.FirstName
                <*> createLastName dto.LastName

            createCustomer 
            <!> customerIdOrError 
            <*> nameOrError
            <*> createEmail dto.Email //inline this one
            <*> extrasFromDtos dto.Extras

    let extraToDto (extra:Extra) = 
        let k = extra.Key |> String10.value
        let v = extra.Value |> String10.value
        ExtraDto(Key=k, Value=v)

    /// Convert a domain Customer into a DTO.
    /// There is no possibility of an error 
    /// because the Customer type has stricter constraints than DTO.
    let customerToDto(cust:Customer) =
        // extract the raw int id from the CustomerId wrapper
        let custIdInt = cust.Id |> CustomerId.apply id

        // create the object and set the properties
        let customerDto = CustomerDto()
        customerDto.Id <- custIdInt 
        customerDto.FirstName <- cust.Name.FirstName |> String10.apply id
        customerDto.LastName <- cust.Name.LastName |> String10.apply id
        customerDto.Email <- cust.Email |> EmailAddress.apply id
        customerDto.Extras <- cust.Extras |> List.map extraToDto |> List.toArray
        customerDto