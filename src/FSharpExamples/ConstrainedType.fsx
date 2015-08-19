open System

type String10 = private {value : string}
    with
    /// Public constructor
    static member Create s = 
        if String.IsNullOrEmpty(s) then
            None
        else if s.Length > 10 then
            None
        else
            Some {value=s}

    /// Property 
    member this.Value = this.value

    override this.ToString() = this.value

type EmailAddress = private {value : string}
    with
    /// Public constructor
    static member Create s = 
        if String.IsNullOrEmpty(s) then
            None
        else if s.Contains("@") |> not then
            None
        else
            Some {value=s}

    /// Property 
    member this.Value = this.value

    override this.ToString() = this.value


let valid = String10.Create("1234567890")
let invalid = String10.Create("12345678901")

let validEmail = EmailAddress.Create("a@example.com")
let invalidEmail = EmailAddress.Create("example.com")
