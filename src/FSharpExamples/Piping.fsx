﻿// ================================================
// Transformation oriented programming
// ================================================

(*
In object-oriented programming, we focus on behavior and messages. 
Objects have internal state that is changed by calling methods or receiving messages.

In functional programming, the focus is on *transformation* of data. 

Unlike the object-oriented approach, data structures are kept separate from the functions that act on them.

Data structures are thus "dumb"; it is the functions that do all the work, 
transforming input data structures to (possibly different) output data structures.

One of my favorite analogies for this is to think of functions as little bits of railroad track, 
with a tunnel in the middle that tranforms the input into an output.

Here are some simple function definitions:
*)

let add1 x = x + 1
let double x = x * 2
let square x = x * x

(*    
The compiler outputs the following text:

val add1 : int -> int
val double : int -> int
val square : int -> int

You can read the arrow as "transforms", 
so `add1 : x:int -> int` means that the function `add1` takes 
an `int` parameter called `x` and transforms it to another `int`.

One way to apply this transformation is by calling the function 
in a familiar way, putting the parameter after the function, like this:

*)

add1 5  // = 6
double (add1 5)  // = 12
square (double (add1 5))  // = 144

// ================================================
// Piping
// ================================================


(*
But it is common in functional programming to chain a set of transformations (that is, functions) together. To do this we using a "pipe" model,
in which the output of one function is sent as the input to the next function in the chain.
This is, of course, similar to the UNIX "pipes and filters" pattern.  

In F# the pipe operator is written `|>` and piping works left to right. 
In Haskell it is written `$` and piping works right to left.

Here's some examples of piping in use:
*)

5 |> add1  // = 6
5 |> add1 |> double // = 12
5 |> add1 |> double |> square // = 144

(*
As the chains get longer, we often make it more readable by putting each step on a new line, like this:
*)

5
|> add1 
|> double 
|> square // = 144

