// ================================================
// Decorator pattern in FP
// ================================================

// Create an input log function and an output log function
// and then use them to create a "logged" version of add1

// TIP for add1Logged use piping "|>" or composition ">>"

let add1 x = x + 1

let inLog x = ??

let outLog x = ??

let add1Logged = ??

// test
add1Logged 4

// test
[1..10] |> List.map add1Logged 