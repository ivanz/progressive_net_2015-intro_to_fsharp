
let formatOption opt =
    match opt with  
    | Some some -> sprintf "Some %i" some
    | None -> "None"


// test
let some1 = Some(1)
let none = None;

let actual = formatOption some1
let expected = "Some 1";
actual = expected 

let actual2 = formatOption none
let expected2 = "None";
actual2 = expected2



