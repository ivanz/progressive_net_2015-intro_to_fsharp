open System

(*
Exercise for total functions

Create a function that converts a string 
(e.g "Sunday" or "Sun") into an DayOfWeek type

1) Define a DayOfWeek type with a case for each day
2) Define a strToDayOfWeek function  which is total

// ====================
CODING TIPS

You can pattern match on strings like this:

match x with
| "a" -> 
| "b" -> 
| etc
| _ ->  // wildcard matches anything


*)

type DayOfWeek = Sun | Mon | Tue | Wed | Thu | Fri | Sat

let strToDayOfWeek s =
    match s with 
    | "Sunday" | "Sun" -> Some Sun
    | "Monday" | "Mon" -> Some Mon
    | "Tuesday" | "Tue" -> Some Tue
    | "Wednesday" | "Wed" -> Some Wed
    | "Thursday" | "Thu" -> Some Thu
    | "Friday" | "Fri" -> Some Fri
    | "Saturday" | "Sat" -> Some Sat
    | _ -> None

// test
strToDayOfWeek "Sunday"
strToDayOfWeek "April"