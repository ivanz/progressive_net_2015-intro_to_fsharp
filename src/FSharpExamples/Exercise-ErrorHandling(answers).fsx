System.IO.Directory.SetCurrentDirectory __SOURCE_DIRECTORY__

(*
Exercise for error handling

=====================
Exercise 1
=====================

1) create a function "chooseEven" that returns Some for even numbers and None otherwise
2) create a function "choosePositive" that returns Some for positive numbers and None otherwise
3) compose the two functions together into a "choosePositiveAndEven" function

TIP "%" is the modulo operator in F#

=====================
Exercise 2
=====================

1) create a function "makeCustomerId" that returns Success CustomerId for positive numbers otherwise Failure "bad customer id"
	type MakeCustomerId = int -> Either<string,CustomerId>
2) create a function "getCustomerFromDb" that takes a connection string and a customerId and  returns Success Customer for customer ids except 42 otherwise Failure "customer not found"
	type GetCustomerFromDb = ConnectionString -> CustomerId -> Either<string,Customer>
3) make a new function "getCustomer" from the other two
	type GetCustomer -> ConnectionString -> int -> Either<string,Customer>



*)    

open System
#load "RailwayOrientedProgramming.fsx"

// =====================
// Exercise 1
// =====================

let chooseEven i = 
	if i%2=0 then Some i else None
	
let choosePositive i = 
	if i>0 then Some i else None
	
let choosePositiveAndEven = chooseEven >> Option.bind choosePositive
	
// =====================
// Exercise 2
// =====================

type CustomerId = CustomerId of int
type Customer = Customer of string

let makeCustomerId i = 
	if i>0 then 
		succeedR (CustomerId i)
	else 
		failR "bad customer id"
	
let getCustomerFromDb connStr (CustomerId i) = 
	if i<>42 then 
		let customer = Customer "OK"
		succeedR customer
    else 			
		failR "customer not found"		

let getCustomer connStr = 		
	let getCustomerFromDbPA = getCustomerFromDb connStr
	makeCustomerId >> bindR getCustomerFromDbPA 
	