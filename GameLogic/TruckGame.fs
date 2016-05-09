module TruckGame

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input

type 

let r = new System.Random()

type List<'a> =
    | Empty
    | Node of 'a * List<'a>
let (<<) x xs = Node(x, xs)

let rec toFSharpList l =
    match l with
    | Empty -> []
    | Node(x, xs) -> x :: toFSharpList xs

let rec filter (p:'a->bool) (l:List<'a>) : List<'a> =
    match l with
    | Empty -> Empty
    | Node(x, xs) ->
        if p x then
            Node(x, filter p xs)
        else
            filter p xs

let rec map (f:'a->'b) (l:List<'a>) : List<'b> =
    match l with
    | Empty -> Empty
    | Node(x:'a, xs:List<'a>) ->
        let y:'b = f x
        let ys:List<'b> = map f xs
        Node(y,ys)

let rec length (l:List<'a>) =
    match l with
    | Empty -> 0
    | Node(x, xs) -> 1 + length xs

