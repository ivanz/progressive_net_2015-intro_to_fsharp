(*
Exercise

1) create a constrained type for Age (age must be between 0 and 130) 
2) create a constrained type for Email where it must contain an @ sign
3) reuse the PersonalName from the previous example
4) create a type Person with 
    * property Name of type PersonalName
    * property Age of type Age
    * property Email of type Email
4) create a type PersonDto with same properties as primitive types (string,int)
   (This will be used for XML or JSON serialization)
5) create a function "toDto" that converts a Person into a DTO
6) create a function "fromDto" that converts a DTO into Person 
*)    

open System
