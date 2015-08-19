namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("FSharpExamples")>]
[<assembly: AssemblyProductAttribute("Intro_To_FP")>]
[<assembly: AssemblyDescriptionAttribute("Intro_To_FP")>]
[<assembly: AssemblyVersionAttribute("0.0.1")>]
[<assembly: AssemblyFileVersionAttribute("0.0.1")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.0.1"
