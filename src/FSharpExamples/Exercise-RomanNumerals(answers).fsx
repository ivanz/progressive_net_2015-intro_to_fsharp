open System

(*
Exercise that uses everything so far

1) Define a type to represent RomanDigit "I", "V", "X"
   (keep it simple and just use these three for now.

2) Define a RomanNumber type that wraps a 
   list of RomanDigits

3) Define a romanNumberToInt function that converts a 
   RomanNumber to an int
   
   Suggested approach:
   a) create a function that converts each RomanDigit to an int
   b) Use List.map to convert the entire list
   c) Use List.sum to sum a list


4) Define a strToRomanNumber function that converts a 
   string to a RomanNumber. (Assume the string only contains
   valid characters)

   Suggested approach:
   a) create a function that converts each char to a RomanDigit
      You will need a wildcard case when matching. Use 
         | _ -> failwith "should not happen"

   b) Use .ToCharArray to convert a string to an char array
   c) Use List.ofArray to convert a array to a list
   d) Use List.map to convert each char to a Digit

   Enhancements:
   * If the input string contains "IV", replace it with "IIII"
   * If the input string contains "IX", replace it with "VIIII"
    
5) Define a StrToRomanNumber function that converts a 
   string to a RomanNumber. But DONT assume the string 
   contains valid characters. Return an error if the 
   input string was bad.

   Suggested approach:
   a) create a choice class that contains either a Success of 'T
      or a Failure string
   b) create a function that converts each char to a RomanDigit
      OR a failure.
   c) if there are any failures in the list of digits, the rest of the 
      processing is skipped

// ====================
CODING TIPS

-- For exercise 5, define a result type like this
type Result<'a> = Success of 'a | Failure of string

then the "charToRomanDigitWithError" function should have signature
 char -> Result<RomanDigit>

and the overall "strToRomanNumberWithError" function should have signature
 string -> Result<RomanNumber>
 
TIP - List.choose will filter and pick for a function that returns Some

For example:

    // define a function that returns Some for good values
    // and None for bad values
    let successDigit result =
        match result with
        | Success d -> Some d
        | Failure _ -> None

    // then the following will return only 
    // the successful digits
    digitsOrErrors |> List.choose successDigit 
*)

// ======================
// part 1
// ======================

type RomanDigit = I | V | X

// ======================
// part 2
// ======================

type RomanNumber = RomanNumber of RomanDigit list

// ======================
// part 3
// ======================

let digitToInt digit =
    match digit with
    | I -> 1
    | V -> 5
    | X -> 10

let romanNumberToInt romanNumber = 
    let (RomanNumber list) = romanNumber 
    let ints = list |> List.map digitToInt
    let sum = ints |> List.sum
    sum

// test
let romanNumber = RomanNumber [X; V; I]
romanNumberToInt romanNumber  

// ======================
// part 4
// ======================

let charToDigit ch =
    match ch with
    | 'I' -> I
    | 'V' -> V
    | 'X' -> X
    | _ -> failwith "should not happen"

let strToRomanNumber (str:string) =
    str.ToCharArray()       // to char array
    |> List.ofArray         // convert array to list
    |> List.map charToDigit // map each char to a digit
    |> RomanNumber          // wrap digits in a RomanNumber

// test
strToRomanNumber "XXI"
strToRomanNumber "VI"

// ======================
// Helpers for part 5
// ======================

type Result<'a> = Success of 'a | Failure of string

/// Helper to convert a list of Results into a Result of list
/// Result<'a> list -> Result<'a list> 
let resultListToResult resultList =
    let allFailuresStr = 
        resultList 
        |> List.choose (function Success _ -> None | Failure s -> Some s)
        |> String.concat "; "
    if allFailuresStr <> "" then
        Failure allFailuresStr
    else
        resultList 
        |> List.choose (function Success x -> Some x | Failure _ -> None)
        |> Success

/// Helper to map a function over a result
/// (f:'a->'b) -> Result<'a> -> Result<'b> 
let mapResult f result =
    match result with 
    | Success x -> Success (f x)
    | Failure err -> Failure err 


// ======================
// part 5 using helper methods
// ======================

let charToRomanDigitWithError ch =
    match ch with
    | 'I' -> Success I
    | 'V' -> Success V
    | 'X' -> Success X
    | _ -> Failure (sprintf "%c is not a valid char" ch)


let strToRomanNumberWithError_v1 (str:string) =
    str.ToCharArray()          // to char array     
    |> List.ofArray            // convert array to list
    |> List.map charToRomanDigitWithError  // map each char to a digitOrError
    |> resultListToResult      // convert a List of Result into a Result of list
    |> mapResult RomanNumber   // in the success case, convert to RomanNumber

// test
strToRomanNumberWithError_v1 "XXI"
strToRomanNumberWithError_v1 "VI"
strToRomanNumberWithError_v1 "ab"

// =======================
// Part 5 without using helper methods
// =======================


let failureMsg result =
    match result with
    | Success _ -> None
    | Failure s -> Some s

let successDigit result =
    match result with
    | Success d -> Some d
    | Failure _ -> None

let strToRomanNumberWithError_v2 (str:string) =
    let digitsWithErrs =
        str.ToCharArray() 
        |> List.ofArray
        |> List.map charToRomanDigitWithError
    let errors = 
        digitsWithErrs 
        |> List.choose failureMsg 
        |> String.concat "; "
    if errors <> "" then
        Failure errors
    else
        let digits =
            digitsWithErrs 
            |> List.choose successDigit
        let number = RomanNumber digits
        Success number

// test
strToRomanNumberWithError_v2 "XXI"
strToRomanNumberWithError_v2 "VI"
strToRomanNumberWithError_v2 "ab"

