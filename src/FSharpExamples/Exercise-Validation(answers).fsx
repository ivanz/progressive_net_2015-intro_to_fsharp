(*
Exercise

1) create a constrained type for Age (age must be between 0 and 130) 
2) create a constrained type for Email where it must contain an @ sign
3) reuse the PersonalName from the previous example
4) create a type Person with 
    * property Name of type PersonalName
    * property Age of type Age
    * property Email of type Email
4) create a type PersonDto with same properties as primitive types (string,int)
   (This will be used for XML or JSON serialization)
5) create a function "toDto" that converts a Person into a DTO
6) create a function "fromDto" that converts a DTO into Person 
*)    

open System

// Here I'm using modules to group related functions together

module String10 =
    type T = private String10 of string

    let create s =
        if String.IsNullOrEmpty(s) then None
        else if (s.Length > 10) then None
        else Some (String10 s)

    let value (String10 s) = s

module Age =
    type T = private Age of int

    let create i =
        if i < 0 then None
        else if i > 130 then None
        else Some (Age i)

    let value (Age s) = s

module Email =
    type T = private Email of string

    let create s =
        if String.IsNullOrEmpty(s) then None
        else if s.Contains("@") |> not then None
        else Some (Email s)

    let value (Email s) = s


type PersonalName = {
    first: String10.T
    last: String10.T 
    }

type Person = {
    name: PersonalName
    age: Age.T 
    email: Email.T
    }

type PersonDto = {
    first: string
    last: string
    age: int
    email: string
    }

/// Create a constructor to be used with partial application
let createName first last = {PersonalName.first=first; last=last}

/// Create a constructor to be used with partial application
let createPerson name age email = {Person.name=name; age=age; email=email}

/// Extend the Option module
module Option =
    // helper function to apply optional functions to optional values
    let apply vOpt fOpt = 
        match (fOpt,vOpt) with
        | Some f, Some v -> Some (f v)
        | _ -> None

// convert a person into a DTO
let toDto person =
    {
    first = String10.value person.name.first
    last = String10.value person.name.last
    age = Age.value person.age
    email = Email.value person.email
    }

// convert a DTO into a person 
let fromDto personDto =
    let first = String10.create personDto.first
    let last = String10.create personDto.last
    let age = Age.create personDto.age
    let email = Email.create personDto.email

    let name = 
        createName 
        |> Some 
        |> Option.apply first 
        |> Option.apply last

    let person = 
        createPerson 
        |> Some 
        |> Option.apply name 
        |> Option.apply age
        |> Option.apply email

    person // return

// test

let goodDto = {
    first = "Alice"
    last = "Adams"
    age = 1
    email = "x@example.com"
    }

let badDto = {
    first = ""
    last = "Adams"
    age = 1
    email = "x@example.com"
    }

goodDto |> fromDto
badDto |> fromDto

// ====================================
// short operators
// ====================================

let (<!>) f = Option.map f
let (<*>) f x = Option.apply x f

let fromDto2 personDto =
    let first = String10.create personDto.first
    let last = String10.create personDto.last
    let age = Age.create personDto.age
    let email = Email.create personDto.email

    let name = createName <!> first <*> last
    let person = createPerson <!> name <*> age <*>email

    person // return

goodDto |> fromDto2
badDto |> fromDto2
