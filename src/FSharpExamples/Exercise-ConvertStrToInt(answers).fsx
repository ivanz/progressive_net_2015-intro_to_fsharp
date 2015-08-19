open System

(*
Exercise for total functions

Create TWO functions that converts a string (e.g "123") 
into an int.

V1) Make the function total by constraining the input
V2) Make the function total by extending the output

// ====================
CODING TIPS

-- you can use System.Int32.TryParse to 
-- do the parsing. It returns a tuple (success, intValue)

*)

type AllDigitsString = AllDigitsString of string
// as to how to ensure that this type is created correctly, ask me!

/// With constrained input
let convertStrToInt_v1 input =
    let (AllDigitsString x) = input
    // should never need to check the success flag
    let success, i = System.Int32.TryParse x // converter
    i

// test
let input = AllDigitsString "123"
convertStrToInt_v1 input 


/// With extended output
let convertStrToInt_v2 input =
    let success, i = System.Int32.TryParse input
    if success then 
        Some i
    else
        None

// test
convertStrToInt_v2 "123"
convertStrToInt_v2 "abc"

