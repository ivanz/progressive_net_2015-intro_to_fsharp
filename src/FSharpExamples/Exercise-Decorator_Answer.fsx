// ================================================
// Decorator pattern in FP
// ================================================

// Create an input log function and an output log function
// and then use them to create "logged" version of add1

let add1 x = x + 1

let inLog x = 
    printf "In=%i; " x
    x

let outLog x = 
    printfn "Out=%i; " x
    x

let add1Logged = inLog >> add1 >> outLog

// test
add1Logged 4

// test
[1..10] |> List.map add1Logged 