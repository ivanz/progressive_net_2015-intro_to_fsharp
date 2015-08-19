open System

(*
Exercise for total functions

Create a function that divides 12 by an int.
The input will be in the range 12 to -12

1) Make the function total by constraining the input
2) Make the function total by extending the output

// ====================
CODING TIPS

-- To define a new "wrapper" type do this:
type NonZeroInteger = NonZeroInteger of int
-- as to how to ensure that this type is created correctly, ask me!

-- To create a wrapped value do this:
let wrapped = NonZeroInteger 12

-- To unwrap a wrapped value do this:
let (NonZeroInteger inner) = wrapped

-- To create "some" value do this:
let x = Some 1

-- To create "none" value do this:
let x = None

-- To do if/then/else
let isEven input =
    if input % 2 = 0 then
        "Yes"
    else
        "No"

*)

type NonZeroInteger = NonZeroInteger of int
// as to how to ensure that this type is created correctly, ask me!

/// With constrained input
let twelveDividedBy_v1 input =
    let (NonZeroInteger x) = input
    12 / x

// test
let input = NonZeroInteger 2
twelveDividedBy_v1 input 


/// With extended output
let twelveDividedBy_v2 input =
    if input = 0 then
        None
    else
        Some (12/input)

// test
twelveDividedBy_v2 2
twelveDividedBy_v2 0
