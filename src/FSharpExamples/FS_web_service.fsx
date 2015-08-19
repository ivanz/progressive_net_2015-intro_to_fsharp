
(*

“Putting it all together: Building a web service in F#” 

For the final session, we will use everything we have learnt to tweak a complete web service, 
with validation, data access, logging, error handling, and more.

*)

(*

Exercise 0 - Compare the F# and C# implementations of the Customer DomainModel

Exercise 1 - in DomainModel.fs, create a new type called "Employee"

type Employee = {
    Id: EmployeeId.T 
    Name: EmployeeName
    Email: EmailAddress.T 
}

Exercise 2 - in Dtos.fs, create a new type called "EmployeeDto" and corresponding "EmployeeDtoConverter"
You should also create a EmployeeDomainMessage type to handle the errors

Exercise 3 - in SqlDatabase.fs, add a Employees table

Exercise 4 - in DataAccessLayer.fs, create a new interface called "IEmployeeDao" and "EmployeeDao" implementation

Exercise 5 - Create new controllers for Employee and add them to the dependencyResolver in Startup.fs

*)

