// ================================================
// Higher Order Functions
// ================================================


// "Parameterize all the things"

let printListA() = 
   for i in [1..10] do
      printfn "the number is %i" i

let printListB aList = 
   for i in aList do
      printfn "the number is %i" i

let printListC anAction aList = 
   for i in aList do
      anAction i

let printAction i = printfn "the number is %i" i
[1..10] |> printListC printAction  


// ================================================
// Fold
// ================================================

(*
Here is some C# code for looping over numbers:

public static int Product(int n)
{
    int product = 1;
    for (int i = 1; i <= n; i++)
    {
        product *= i;
    }
    return product;
}

public static int SumOfOdds(int n)
{
    int sum = 0;
    for (int i = 1; i <= n; i++)
    {
        if (i % 2 != 0) { sum += i; }
    }
    return sum;
}

What do these implementations have in common? 

The looping logic! 

As programmers, we are told to remember the DRY principle ("don’t repeat yourself"), 
yet here we have repeated almost exactly the same loop logic each time. 


Let's see if we can extract just the differences between these three methods:

| Function	| Initial value	| Inner loop logic |
| Product	| product=1     | Multiply the i'th value with the running total |
| SumOfOdds	| sum=0         | Add the i'th value to the running total if not even |

Is there a way to strip the duplicate code and focus on the just the setup and inner loop logic? 

Yes there is. 

Here are the same three functions in F#:
*)

let product n = 
    let initialValue = 1
    let action productSoFar x = productSoFar * x
    [1..n] |> List.fold action initialValue

//test
product 10

let sumOfOdds n = 
    let initialValue = 0
    let action sumSoFar x = if x%2=0 then sumSoFar else sumSoFar+x 
    [1..n] |> List.fold action initialValue

//test
sumOfOdds 10

(*
And here's another one
*)
let alternatingSum n = 
    let initialValue = (true,0)
    let action (isNeg,sumSoFar) x = if isNeg then (false,sumSoFar-x)
                                             else (true ,sumSoFar+x)
    [1..n] |> List.fold action initialValue |> snd

//test
alternatingSum 100

(*
All three of these functions have the same pattern:

* Set up the initial value
* Set up an action function that will be performed on each element inside the loop.
* Call the library function List.fold. 
  This is a powerful, general purpose function which starts with the initial value and 
  then runs the action function for each element in the list in turn.

The action function always has two parameters: 
* a running total (or state) and 
* the list element to act on (called "x" in the above examples).

In the last function, alternatingSum, you will notice that 
it used a tuple (pair of values) for the initial value and the result of the action. 

This is because both the running total and the isNeg flag must be 
passed to the next iteration of the loop -- there are no "global" values that can be used. 

The final result of the fold is also a tuple so we have to use the 
"snd" (second) function to extract the final total that we want.

By using List.fold and avoiding any loop logic at all, the F# code gains a number of benefits:

* The key program logic is emphasized and made explicit. 
  The important differences between the functions become very clear, 
  while the commonalities are pushed to the background.

* The boilerplate loop code has been eliminated, and as a result the 
  code is more condensed than the C# version 
  (4-5 lines of F# code vs. at least 9 lines of C# code)

* There can never be a error in the loop logic (such as off-by-one) 
  because that logic is not exposed to us.

*)


// ================================================
// List transformations using higher-order functions
// ================================================


(*
It is obvious how transformation works for simple values such as ints and strings. 

But say that we want to transform a more complex structure.  
Perhaps we want to transform a list into a different list? How does that work? 

Rather that using `for` loops, we stick with the transformation approach and 
create a general function `map` that transforms a list into another list.

But how do we tell `map` what transformation we want to do?  We pass in a function as a parameter!

The function we pass in tells us how to transform one element of the list. 
And then `map` uses that to transform every element for us.

If you are used to LINQ or SQL, then you are already familiar with the `map` concept, but under the name `Select`!

Below, we create some list transformation functions using `map` and a function parameter
*)


let add1 x = x + 1
let double x = x * 2
let square x = x * x


let add1ToEveryElement = List.map add1
let doubleEveryElement = List.map double
let squareEveryElement = List.map square

(*
Now we can use these functions just like the transformations we did on simple integers
*)

[1..10] |> add1ToEveryElement

[1..10] |> add1ToEveryElement |> doubleEveryElement

(*
Or, putting each step on a new line:
*)
 
[1..10] 
|> add1ToEveryElement 
|> doubleEveryElement 
|> squareEveryElement

(*
Often, we won't bother to create special list transformation functions, 
but will instead use the `map` function "inline" as it were, like this:
*)

[1..10] 
|> List.map add1 
|> List.map double
|> List.map square

(*
And if we are really lazy will use lambdas to define the function parameter inline as well, like this:
*)

[1..10] 
|> List.map (fun x -> x + 1) 
|> List.map (fun x -> x * 2)
|> List.map (fun x -> x * x)

(*
The "map" concept appears everywhere in FP. 

Every common data structure (lists, sets, trees) generally has an associated `map` function.

The same transformation approach can be used for filtering.  

Again we want to transform one list into another, but instead of transforming each item,
we want to include only items that meet certain criteria.

Just as with `map`, there is a generic `filter` function that takes a function as parameter.

First let's define some functions that test individual elements:

*)

let isEven x = (x%2 = 0)
let isPositive x = (x > 0)

(*
Now we can use `filter` to create a new function that transforms lists:
*)

let onlyEvenElements = List.filter isEven
let onlyPositiveElements = List.filter isPositive

(*
And here it is in use
*)

[-5..5] 
|> onlyEvenElements  // = [-4; -2; 0; 2; 4]

[-5..5] 
|> onlyEvenElements |> onlyPositiveElements // = [2; 4]

(*
Again, because we're lazy, we tend to omit the explicit definitions and just write this:
*)

[-5..5] 
|> List.filter isEven
|> List.filter isPositive // = [2; 4]

