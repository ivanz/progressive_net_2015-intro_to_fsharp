(*
Examples of OO code in F#
*)

// ===============================
// Creating objects directly from an interface
//
// F# has a useful feature called "object expressions". 
// This is the ability to directly create objects from an interface or 
// abstract class without having to define a concrete class first.
// ===============================


// In the example below, we create some objects that implement 
// IDisposable using a makeResource helper function.

// create a new object that implements IDisposable
let makeResource name = 
   { new System.IDisposable 
     with member this.Dispose() = printfn "%s disposed" name }

let useAndDisposeResources = 
    use r1 = makeResource "first resource"
    printfn "using first resource" 
    for i in [1..3] do
        let resourceName = sprintf "\tinner resource %d" i
        use temp = makeResource resourceName 
        printfn "\tdo something with %s" resourceName 
    use r2 = makeResource "second resource"
    printfn "using second resource" 
    printfn "done." 

// ===============================
// TryParse and TryGetValue
//
// The TryParse and TryGetValue functions for values and dictionaries 
// are frequently used to avoid extra exception handling. 
//
// But the C# syntax is a bit clunky. Using them from F# is more elegant 
// because F# will automatically convert the function into a tuple where 
// the first element is the function return value and the second is the "out" parameter.
// ===============================


//using an Int32
let (i1success,i1) = System.Int32.TryParse("123");
if i1success then printfn "parsed as %i" i1 else printfn "parse failed"

let (i2success,i2) = System.Int32.TryParse("hello");
if i2success then printfn "parsed as %i" i2 else printfn "parse failed"

//using a DateTime
let (d1success,d1) = System.DateTime.TryParse("1/1/1980");
let (d2success,d2) = System.DateTime.TryParse("hello");

//using a dictionary
let dict = new System.Collections.Generic.Dictionary<string,string>();
dict.Add("a","hello")
let (e1success,e1) = dict.TryGetValue("a");
let (e2success,e2) = dict.TryGetValue("b");


// ===============================
// Interfaces, abstract classes etc
// ===============================

// interface
type IEnumerator<'a> = 
    abstract member Current : 'a
    abstract MoveNext : unit -> bool 

// abstract base class with virtual methods
[<AbstractClass>]
type Shape() = 
    //readonly properties
    abstract member Width : int with get
    abstract member Height : int with get
    //non-virtual method
    member this.BoundingArea = this.Height * this.Width
    //virtual method with base implementation
    abstract member Print : unit -> unit 
    default this.Print () = printfn "I'm a shape"

// concrete class that inherits from base class and overrides 
type Rectangle(x:int, y:int) = 
    inherit Shape()
    override this.Width = x
    override this.Height = y
    override this.Print ()  = printfn "I'm a Rectangle"

//test
let r = Rectangle(2,3)
printfn "The width is %i" r.Width
printfn "The area is %i" r.BoundingArea
r.Print()

// ===============================
// Classes can have multiple constructors, mutable properties, and so on.
// ===============================

type Circle(rad:int) = 
    inherit Shape()

    //mutable field
    let mutable radius = rad
    
    //property overrides
    override this.Width = radius * 2
    override this.Height = radius * 2
    
    //alternate constructor with default radius
    new() = Circle(10)      

    //property with get and set
    member this.Radius
         with get() = radius
         and set(value) = radius <- value

    //auto property with get and set
    member val Label = "a circle" with get, set

// test constructors
let c1 = Circle()   // parameterless ctor
printfn "The width is %i" c1.Width
let c2 = Circle(2)  // main ctor
printfn "The width is %i" c2.Width

// test mutable property
c2.Radius <- 3
printfn "The width is %i" c2.Width

// =============================
// Generics
//
// F# supports generics and all the associated constraints.
// =============================

// standard generics
type KeyValuePair<'a,'b>(key:'a, value: 'b) = 
    member this.Key = key
    member this.Value = value
    
// generics with constraints
type Container<'a,'b 
    when 'a : equality 
    and 'b :> System.Collections.ICollection>
    (name:'a, values:'b) = 
    member this.Name = name
    member this.Values = values

// =============================
// Structs
//
// F# supports not just classes, but the .NET struct types as well, which can help to boost performance in certain cases.
// =============================


type Point2D =
   struct
      val X: float
      val Y: float
      new(x: float, y: float) = { X = x; Y = y }
   end

//test
let p = Point2D()  // zero initialized
let p2 = Point2D(2.0,3.0)  // explicitly initialized


// =============================
// Exceptions
//
// F# can create exception classes, raise them and catch them.
// =============================

// create a new Exception class
exception MyError of string

try
    let e = MyError("Oops!")
    raise e
with 
    | MyError msg -> 
        printfn "The exception error was %s" msg
    | _ -> 
        printfn "Some other exception" 


// =============================
// Extension methods
//
// Just as in C#, F# can extend existing classes with extension methods.
// =============================

type System.String with
    member this.StartsWithA = this.StartsWith "A"

//test
let s = "Alice"
printfn "'%s' starts with an 'A' = %A" s s.StartsWithA

type System.Int32 with
    member this.IsEven = this % 2 = 0

//test
let i = 20
if i.IsEven then printfn "'%i' is even" i


// =============================
// Parameter arrays
//
// Just like C#’s variable length “params” keyword, 
// this allows a variable length list of arguments 
// to be converted to a single array parameter.
// =============================

open System
type MyConsole() =
    member this.WriteLine([<ParamArray>] args: Object[]) =
        for arg in args do
            printfn "%A" arg

let cons = new MyConsole()
cons.WriteLine("abc", 42, 3.14, true)

// =============================
// Enums
//
// F# supports CLI enums types, which look similar to the "union" types, but are actually different behind the scenes.
// =============================


// enums
type Color = | Red=1 | Green=2 | Blue=3

let color1  = Color.Red    // simple assignment
let color2:Color = enum 2  // cast from int
// created from parsing a string
let color3 = System.Enum.Parse(typeof<Color>,"Green") :?> Color // :?> is a downcast

[<System.FlagsAttribute>]
type FileAccess = | Read=1 | Write=2 | Execute=4 
let fileaccess = FileAccess.Read ||| FileAccess.Write