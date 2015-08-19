/// =============================================
/// FizzBuzz
/// =============================================


(*

Given a number
If it is divisible by 3, return "Fizz"
If it is divisible by 5, return "Buzz"
If it is divisible by 3 and 5, return "FizzBuzz"
Otherwise return the number as a string
Do NOT print anything

Challenge, write this using a piping model.

TIP: 
There is no "return" - the last value in the function is a return

TIP: to check for divisibility use %
10 % 5 = 0
10 % 4 = 0

TIP: to create a string from int use %i
sprintf "%i" 123

TIP: 
if/then expressions look like

if x then
   y
else
   z


TIP: 
* you will probably need to define an intermediate data structure 

type Data = {something:string; somethingElse:bool}; 
// to create and access
let data = {something="hello"; somethingElse=false}; 
let something = data.something 


*)

// uncomment this code to start

type Data = {carbonated: string; number: int}

let test3 data =
    // unprocessed
    if data.carbonated = "" then
        if data.number % 3 = 0 then 
            {carbonated="Fizz"; number = -1}
        else
            data // leave alone
    else
        data // leave alone

let test5 data =
    // unprocessed
    if data.carbonated = "" then
        if data.number % 5 = 0 then 
            {carbonated="Buzz"; number = -1}
        else
            data // leave alone
    else
        data // leave alone

let test15 data =
    // unprocessed
    if data.carbonated = "" then
        if data.number % 15 = 0 then 
            {carbonated="FizzBuzz"; number = -1}
        else
            data // leave alone
    else
        data // leave alone

let finalResult data =
    // unprocessed
    if data.carbonated = "" then
        sprintf "%i" data.number 
    else
        data.carbonated

// fizzBuzz takes an int and returns a string
let fizzBuzz (aNumber:int) :string = 
    let data = {carbonated=""; number=aNumber}
    data 
    |> test15 
    |> test3
    |> test5
    |> finalResult 

for i in [1..31] do
    printfn "%s" (fizzBuzz i)


// This code is very ugly!
// For extra credit, tidy it up so that test3, test5, and test15 can be combined into one function 
