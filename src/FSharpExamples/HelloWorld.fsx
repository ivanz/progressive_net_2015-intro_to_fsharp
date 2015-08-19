// ================================
// My first F# program
// ================================

// It's traditional for tutorials to start with "hello world", so here it is in F#

printfn "Hello World"

(*

Even with such a simple bit of code, there are some interesting things to note:

* This snippet does not need a containing class. 
* It can be run directly in an interactive environment 
* There is a space between the `printfn` function and its parameter, rather than a parenthesis. 
  We'll see why this is important soon!
*)



let myName = "Scott"
printfn "my name is %s" myName


let add x y = x + y
add 1 2 |> printfn "1 + 2 = %i"

