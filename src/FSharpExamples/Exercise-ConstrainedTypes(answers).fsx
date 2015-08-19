open System

module ConstrainedTypes =

    type NonZeroInteger = private NonZeroInteger of int

    /// Public constructor
    let createNonZeroInteger i = 
        if i = 0 then
            None
        else
            Some (NonZeroInteger i)
    
    /// Return the value
    let nonZeroIntegerValue input = 
        let (NonZeroInteger x) = input
        x

// test
//let x = ConstrainedTypes.NonZeroInteger 0
let x = ConstrainedTypes.createNonZeroInteger 0


// ====================================
// More...
// ====================================

module ConstrainedTypes2 =

    type AllDigitsString = private AllDigitsString of string

    /// Public constructor
    let createAllDigitsString (s:string) = 
        let chars = s.ToCharArray()
        if chars |> Array.forall Char.IsDigit then
            Some (AllDigitsString s)
        else
            None
    
    /// Return the value
    let allDigitsStringValue input = 
        let (AllDigitsString s) = input
        s

// test
open ConstrainedTypes2
let x2 = createAllDigitsString "123"
let inner = allDigitsStringValue (x2.Value)  // dont do this except for testing!

let x3 = createAllDigitsString "abc"
