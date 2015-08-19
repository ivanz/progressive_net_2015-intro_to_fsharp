open System

module String10 =
    type T = private String10 of string

    let create s =
        if String.IsNullOrEmpty(s) then
            None
        else if (s.Length > 10) then
            None
        else 
            Some (String10 s)

    let value (String10 s) = s

// Say that you have a “PersonalName” type that is
// built from other types: String10 say

type PersonalName = {
    first: String10.T;
    last: String10.T
    }

// Now the String10 types have to be valid before they can be created.
//
// We have enforced this by having a constructor that does validation, 
// and which return an optional value: “Some” if the input is valid, 
// but “None” if the input is not valid.

// Here’s the problem: we want to create a valid PersonalName, 
// but the various inputs might be valid or not. 
// Only if all the inputs are valid should we create the PersonalName. 
// How can we do this?

// The imperative approach might be to do all the validations, 
// and then if they all valid, then create a new PersonalName
// with the valid values. 

// Here’s an example of how this might look:

let createNameFromOptions firstOpt lastOpt =
    match (firstOpt,lastOpt) with
    | (Some f, Some l) ->
        Some {first=f; last=l} 
    | _ ->
        None

// When we have 5,6,7 parameters, this gets messy
// and we are ignoring the error case
//
// It would be nice if we could build the PersonalName incrementally, 
// processing one input at a time, and abandoning it immediately 
// if we find a bad input. 
//
// But the PersonalName type is immutable and must be built all-or-nothing, 
// so we seem to be stuck.
//
// However, there *is* a way to work incrementally, and the secret 
// is to use partial application.



// First, we create a function that creates the 
// Person type in the normal way. It will assume all 
// the inputs are valid, so it is very simple. 

let createPersonalName first last = {first=first; last=last}

// Because it is a function, we can build it up incrementally, 
// processing one parameter at a time.

// First we pass in the first parameter, creating a partially 
// applied function. If the first parameter is successful, 
// then the partially applied function will be too (e.g. Something).

// But if the first parameter is not successful, then the partially 
// applied function will also be unsuccessful (e.g. None) 

// Next, we take the partially applied function and pass in the 
// next parameter. This time the result will be successful only 
// if both the “partial1” and the parameter are successful, 
// otherwise the result (“partial2”) will be unsuccessful. 

// So finally we have our result, which will be either success 
// or failure.

// Here’s the real code, with some helper functions “<^>” and “<*>” defined. 

/// Extend the Option module
module Option =
    // helper function to apply optional functions to optional values
    let apply vOpt fOpt = 
        match (fOpt,vOpt) with
        | Some f, Some v -> Some (f v)
        | _ -> None


let goodFirst = String10.create "Alice"
let goodLast = String10.create "Adams"
let badFirst = String10.create ""
let badLast = String10.create ""

let goodPersonalNameO = 
    createPersonalName
    |> Some 
    |> Option.apply goodFirst 
    |> Option.apply goodLast

let badPersonalNameO = 
    createPersonalName
    |> Some 
    |> Option.apply badFirst
    |> Option.apply goodLast

// and there are operators we can use too

let (<!>) f = Option.map f
let (<*>) f x = Option.apply x f