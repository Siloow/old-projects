// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

//[<EntryPoint>]
//let main argv = 
//    printfn "%A" argv
//    0 // return an integer exit code

open System
[<EntryPoint>]
let main (args : string[]) =
    if args.Length <> 2 then
        failwith "Error: Expected arguments <greeting> and <thing>"
    let greeting, thing = args.[0], args.[1]
    let timeOfDay = DateTime.Now.ToString("hh:mm tt")
    printfn "%s, %s at %s" greeting thing timeOfDay

    0
