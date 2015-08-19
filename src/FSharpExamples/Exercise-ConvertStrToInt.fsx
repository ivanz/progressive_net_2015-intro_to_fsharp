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


/// With constrained input
let convertStrToInt_v1 input =
    ??

// test
convertStrToInt_v1 ??


/// With extended output
let convertStrToInt_v2 input =
    ??

// test
convertStrToInt_v2 "123"
convertStrToInt_v2 "abc"

