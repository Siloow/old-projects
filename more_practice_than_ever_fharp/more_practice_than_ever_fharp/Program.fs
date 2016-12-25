//// Chapter 2
//// Page 7
//// Split a string into words at spaces
//let splitAtSpaces (text: string) =
//    text.Split ' '
//    |> Array.toList
//
//// Analyze a string for duplicate words
//let wordCount text =
//    let words = splitAtSpaces text
//    let wordSet = Set.ofList words
//    let numWords = words.Length
//    let numDups = words.Length - wordSet.Count
//    (numWords,numDups)
//
//// Analyze a string for duplicate words and display the results
//let showWordCount text =
//    let numWords,numDups = wordCount text
//    printfn "--> %d words in the text" numWords
//    printfn "--> %d duplicate words" numDups
//
//// Page 9
//let powerOfFour n =
//    let nSquared = n * n
//    nSquared * nSquared
//
//// Page 19
//open System.Windows.Forms
//
//let form = new Form(Visible=true, TopMost=true, Text="Welcome")
//
//let textB = new RichTextBox(Dock=DockStyle.Fill, Text="Here is some text")
//form.Controls.Add textB
//
////Page 20
//open System.IO
//open System.Net
//
//let http (url: string) =
//    // The first line of the code creates a WebRequest object using the static method Create, a member of
//    // the type System.Net.WebRequest. The result of this operation is an object that acts as a handle to a
//    // running request to fetch a web page—you could, for example, abandon the request or check to see
//    // whether the request has completed.
//    let req = System.Net.WebRequest.Create(url)
//
//    // calls the instance method GetResponse.
//    let resp = req.GetResponse()
//    
//    // The remaining lines of the sample get a stream of data from the response to the request using
//    // resp.GetResponseStream(), make an object to read this stream using new StreamReader(stream), and
//    // read the full text from this stream.
//    let stream = resp.GetResponseStream()
//    let reader = new StreamReader(stream)
//    let html = reader.ReadToEnd()
//    resp.Close()
//    html
//
//let google = http "http://www.google.com"
//textB.Text <- http "http://news.bbc.co.uk"

// Chapter 3
// Expects an int
let squareAndAdd a b = a * a + b

// To make it infer a float
let squareAndAdd' (a : float) b = a * a + b

// Page 33
// Basic list values
let oddPrimes = [3; 5; 7; 11]
let morePrimes = [13; 17;]
// Use the Cons (::) operation to concatenate elements
// The (@) operator concatenates two lists
// Lists are immutable: the cons :: and append @ operations don’t modify the original lists; instead,
// they create new lists.
let primes = 2 :: (oddPrimes @ morePrimes)

// Page 34
// Example of pattern matching with lists
let printFirst primes =
    match primes with
    | h :: t -> printfn "The first prime in the list is %d" h
    | [] -> printfn "No primes found"

// Page 36
// Example of list function
let headElement = List.head [5; 4; 3]
let tailElements = List.tail [5; 4; 3]
let mappedList = List.map (fun x -> x*x) [1; 2; 3]
let filteredList = List.filter (fun x -> x % 3 = 0) [2; 3; 5; 7; 9]

// Page 37
// Option values
type 'T option =
    | None
    | Some of 'T

let people = [  ("Adam", None);
                ("Eve" , None);
                ("Cain", Some("Adam","Eve"));
                ("Abel", Some("Adam","Eve")) ]

// Pattern matching is frequently used to examine option values
let showParents (name,parents) =
    match parents with
    | Some(dad,mum) -> printfn "%s has father %s, mother %s" name dad mum
    | None -> printfn "%s has no parents!" name

// Page 38
// Conditionals
let round x =
    if x >= 100 then 100
    elif x < 0 then 0
    else x

// Conditionals are really shorthand for pattern matching
// This is the same as the 
let round' x =
    match x with
    | _ when x >= 100   -> 100
    | _ when x < 0      -> 0
    | _                 -> x


// Page 39
// the function factorial 5 executes like this:

//factorial 5
//= 5 * factorial 4
//= 5 * (4 * factorial 3)
//= 5 * (4 * (3 * factorial 2))
//= 5 * (4 * (3 * (2 * factorial 1 )))
//= 5 * (4 * (3 * (2 * 1)))
//= 5 * (4 * (3 * 2))
//= 5 * (4 * 6)
//= 5 * 24
//= 120

// As with all calls, the execution of the currently executing instance of the function is suspended while
// a recursive call is made

// Lots os operators can be coded as recurise functions
let rec length l =
    match l with
    | [] -> 0
    | h :: t -> 1 + length t


// Page 40
// You can define multiple recursive function simultaneously by separating the definitions with "and".
// These are called mutually recursive functions.
let rec even n = (n = 0u) || odd(n-1u)
and     odd n = (n <> 0u) && even(n-1u)

// A more efficient way to code the latter part, which is non recursive
let even' (n:uint32) = (n % 2u) = 0u
let odd' (n:uint32) = (n % 2u) = 1u

// Page 41 : Function values
// List.map;;
// val it: ('T -> 'U) -> 'T list -> 'U list
// This says List.map accepts a function value as the first argument and a list as the second argument,
// and it returns a list as the result. The function argument can have any type 'T -> 'U, and the elements of
// the input list must have a corresponding type 'T. The symbols 'T and 'U are called type parameters, and
// functions that accept type parameters are called generic.

let primes' = [2; 3; 5; 7]
let primeCubes = List.map (fun n -> n * n * n) primes'

// List.map (fun (_,p) -> String.length p) resultsOfFetch
// Here you see two things:
// - The argument of the anonymous function is a tuple pattern. Using a tuple pattern
//   automatically extracts the second element from each tuple and gives it the name p
//   within the body of the anonymous function.
// - Part of the tuple pattern is a wildcard pattern, indicated by an underscore. This
//   indicates that you don’t care what the first part of the tuple is; you’re interested
//   only in extracting the length from the second part of the pair.

let delimiters = [| ' '; '\n'; '\t'; '<'; '>'; '='|]
let getWords (s:string) = s.Split delimiters
let getStats site =
    let url = "http://" + site
    // Uncomment the http function on line 39
    let html = http url
    let hwords = html |> getWords
    let hrefs = html |> getWords |> Array.filter (fun s -> s = "href")
    (site,html.Length, hwords.Length, hrefs.Length)

// To test the code above :
// let sites = [ "www.live.com";"www.google.com";"search.yahoo.com" ];;
// sites |> List.map getStats;;

// Page 44
// Pipelining with |>
// The |> forward pipe operator is perhaps the most important operator in F# programming. Its definition is deceptively simple:
// let (|>) x f = f x
// Here is how to use the operator to compute the cubes of three numbers:
// [1;2;3] |> List.map (fun x -> x * x * x)
// This produces [1;8;27], just as if you had written this:
// List.map (fun x -> x * x * x) [1;2;3]

// In a sense, (|>) is function application in reverse. However, using (|>) has distinct advantages
//      - Clarity: When used in conjuction with operators such as List.map, the (|>)
//                 operator allows you to perform the data transmissions and iterations in a
//                 forward-chaining, pipelined style.
//      - Type inference: Using the (|>) operator lets type information flow from input objects
//                        to the functions manipulating these objects. F# uses information collected from
//                        type inference to resolve some language constructs such as property accesses
//                        and method overloading. This relies on information beging propagated left to right
//                        through the text of a program. In particular, typing information to the right of a
//                        position isn't taken into account when resolving property acces and overloads.
// The type :
// (|>) : 'T -> ('T -> 'U) -> 'U


// Composing functions with (>>)
// Code example to show the differences between (|>) and (>>)
// let google = http "http://google.com"
// google |> getWords |> List.filter (fun s -> s = "href") |> List.length
// Rewriting the code above using function composition goes as follows
// let countLinks = getWords >> List.filter (fun s -> s = "href") >> List.length
// google |> countLinks
// The operator is defined as:
// let (>>) f g x = g(f(x))
// The type : 
// (>>) : ('T -> U) -> ('U -> 'c) -> ('T -> 'c)
// Note that >> is typically applied to only two arguments: those on either side of the binary operator,
// here named f and g. The final argument x is typically supplied at a later point.
// Leaving the final argument out = partial application

let shift (dx, dy) (px, py) = (px + dx, py + dy)
let shiftRight = shift (1, 0)
let shiftUp = shift (0, 1)
let shiftLeft = shift (-1, 0)
let shiftDown = shift (0, -1)

// Local variables example (mapx and mapy)
open System.Drawing;
let remap (r1: Rectangle) (r2: Rectangle) =
    let scalex = float r2.Width / float r1.Width
    let scaley = float r2.Height / float r1.Height
    let mapx x = int (float r2.Left + truncate (float (x - r1.Left) * scalex))
    let mapy y = int (float r2.Top + truncate (float (y - r1.Top) * scaley))
    let mapp (p:Point) = Point(mapx p.X, mapy p.Y)
    mapp

// Page 50 : Getting started with pattern matching
// Simple pattern match example
// The last three rules all use wildward patterns represented by the underscore character; these match all inputs.
let urlFilter url agent =
    match (url, agent) with
    | "http://www.control.org", 99 -> true
    | "http://www.kaos.org", _ -> false
    | _, 86 -> true
    | _ -> false

// Page 52 
// The final case of the following pattern uses the wildcard pattern to cover remaining cases.
// This makes the patterens exhaustive.
let highLow a b =
    match (a, b) with
    | ("lo", lo), ("hi", hi) -> (lo, hi)
    | ("hi", hi), ("lo", lo) -> (lo, hi)
    | _ -> failwith "expected a both a high and low value"


// Guarding rules and combining patterns
// Guard example
let sign x =
    match x with
    | _ when x < 0 -> -1
    | _ when x > 0 -> 1
    | _ -> 0

// Example of combining two patterns to represent two possible paths for matching
let getValue a =
    match a with
    | (("lo" | "low"), v) -> v
    | ("hi", v) | ("high", v) -> v
    | _ -> failwith "failed result"

// Individual patterns can’t bind the same variables twice. For example, a pattern (x,x) isn’t permitted,
// although (x,y) when x = y is permitted. Furthermore, each side of an “or” pattern must bind the same set of
// variables, and these variables must have the same types.

// Page 53 : Getting started with sequences
// Many programming tasks require the iteration, aggregation, and transformation of data streamed from
// various sources. One important and general way to code these tasks is in terms of values of the .NET type
// System.Collections.Generic.IEnumerable<type>, which is typically abbreviated to seq<type> in F# code.
// A seq<type> is a value that can be iterated, producing results of type type on demand. Sequences are
// used to wrap collections, computations, and data streams and are frequently used to represent the
// results of database queries.

// Generating a simple sequence using range expressions
let randomSequence = seq {0 .. 2}

// By default F# interactive shows the value of a sequence only to a limited depth. seq<'T> values are
// lazy in the sense that they compute and return the succesive elements on demand. This means you can
// create sequences representing very large ranges, and the elements of the sequence are computed only if
// they're required by a subsequent computation.

// The default increment for range is always 1. A different increment can be used via range
// expressions of the form seq { n .. skip .. }
// seq { 1 .. 2 .. 5 } ;;
// val it : seq<int> = seq [ 1; 3; 5 ]
// Note : if the skip causes the final element to be overshot, then the final element isn't included in the result

// Iterating a sequence example
let range = seq {0 .. 2 .. 6}
for i in range do
    printfn "i = %d" i

// Transforming sequences with aggregate operators
let range' = seq {0 .. 10}
let rangeResult = range' |> Seq.map(fun i ->(i,i*i))