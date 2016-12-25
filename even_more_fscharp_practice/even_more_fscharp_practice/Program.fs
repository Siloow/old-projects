namespace Chapter1
    module Math =
        [<Measure>] // meters
        type m

        [<Measure>] // kilograms 
        type kg

        [<Measure>] // seconds 
        type s

        [<Measure>] // newtowns lawl
        type N = kg*m/s^2

        type Vector2<[<Measure>] 'a> =
            {
                X : float<'a>
                Y : float<'a>
            }
 
            static member Zero : Vector2<'a> = 
                    { X = 0.0<_>; Y = 0.0<_> }

            static member (+)
                (v1:Vector2<'a>,v2:Vector2<'a>):Vector2<'a> =
                { X = v1.X+v2.X; Y = v1.Y+v2.Y }
            static member (+)
                (v:Vector2<'a>,k:float<'a>):Vector2<'a> =
                { X = v.X+k; Y = v.Y+k }
            static member (+)
                (k:float<'a>,v:Vector2<'a>):Vector2<'a> = v+k

            static member (~-) (v:Vector2<'a>):Vector2<'a> = 
                { X = -v.X; Y = -v.Y}

            static member (-)
                (v1:Vector2<'a>, v2:Vector2<'a>):Vector2<'a> = 
                v1+(-v2)
            static member (-)
                (v:Vector2<'a>, k:float<'a>):Vector2<'a> = v+(-k)
            static member (-)
                (k:float<'a>, v:Vector2<'a>):Vector2<'a> = k+(-v)

            static member (*)
                (v1:Vector2<'a>,v2:Vector2<'b>):Vector2<'a*'b> =
                { X = v1.X*v2.X; Y = v1.Y*v2.Y }
            static member (*)
                (v:Vector2<'a>,f:float<'b>):Vector2<'a*'b> =
                { X = v.X*f; Y = v.Y*f }
            static member (*)
                (f:float<'b>,v:Vector2<'a>):Vector2<'b*'a> =
                { X = f*v.X; Y = f*v.Y }

            static member (/)
                (v:Vector2<'a>,f:float<'b>):Vector2<'a/'b> =
                    v*(1.0/f)

            member this.Length : float<'a> = 
                sqrt((this.X*this.X+this.Y*this.Y))

            static member Distance(v1:Vector2<'a>,v2:Vector2<'a>) =
                (v1-v2).Length
            static member Normalize(v:Vector2<'a>):Vector2<1> =
                v/v.Length

namespace Chapter3
    module SmallAsteroidFiledSimulation = 
        open System
        open System.Threading

        open Chapter1.Math
    
        type Asteroid = 
            {
                Position : Vector2<m>
                Velocity : Vector2<m/s>
                Mass : float<kg>
                Name : string
            }

        let dt = 600.0<s>
        let G = 6.67e-11<m^3*kg^-1*s^-2> 

        let earth_radius = 6.37e6<m>
        let field_size = earth_radius*60.0
        let max_velocity = 2.3e4<m/s>
        let earth_mass = 5.97e24<kg>
        let moon_mass = 7.35e22<kg>

        let create_field num_asteroids = 
            let lerp (x:float<'u>) (y:float<'u>) (a:float) =
                x*a+y*(1.0-a)
            let rand = System.Random()

            [ 
            for i = 1 to num_asteroids do
                let m = 
                        (lerp earth_mass moon_mass (rand.NextDouble())) * 1.0e-4
                let x = lerp 0.0<m> field_size (rand.NextDouble())
                let y = lerp 0.0<m> field_size (rand.NextDouble())
                let vx = max_velocity*
                    (rand.NextDouble()*2.0-1.0)*0.1
                let vy = max_velocity*
                    (rand.NextDouble()*2.0-1.0)*0.1
                yield
                    {
                        Position = { X = x; Y = y; }
                        Velocity = { X = vx; Y = vy }
                        Mass = m
                        Name = "a"
                    }
            ]
                        
        let f0 = create_field 20

        let clamp (p:Vector2<_>,v:Vector2<_>) =
            let p,v =
                if p.X < 0.0<_> then
                    { p with X = 0.0<_>}, {v with X = -v.X }
                else p,v
            let p, v =
                if p.X > field_size then
                    { p with X = field_size }, {v with X = -v.X }
                else p,v
            let p,v =
                if p.Y < 0.0<_> then
                    { p with Y = 0.0<_> }, { v with Y = -v.Y }
                else p,v
            let p,v =
                if p.Y > field_size then
                    { p with Y = field_size }, { v with Y = -v.Y }
                else p,v
            p,v

        let force (a:Asteroid,a':Asteroid) =
            let dir = a'.Position-a.Position
            let dist = dir.Length+1.0<m>
            G*a.Mass*a'.Mass*dir/(dist*dist*dist)

        let simulation_step (asteroids:Asteroid list) = 
            [
            for a in asteroids do
                let forces =
                    [
                    for a' in asteroids do
                        if a' <> a then
                            yield force(a, a')
                    ]
                let F = List.sum forces

                let p', v' = clamp(a.Position, a.Velocity)
                yield
                    {
                        a with
                            Position = p'+dt*v'
                            Velocity = v'+dt*F/a.Mass
                    }
            ]

        let print_scene (asteroids:Asteroid list) =
            do Console.Clear()
            for i = 0 to 79 do
                Console.SetCursorPosition(i,0)
                Console.Write("*")
                Console.SetCursorPosition(i, 23)
                Console.Write("*")
            for j = 0 to 23 do
                Console.SetCursorPosition(0,j)
                Console.Write("*")
                Console.SetCursorPosition(79,j)
                Console.Write("*")
            let set_cursor_on_body b =
                Console.SetCursorPosition(
                    ((b.Position.X/4.0e8<m>*78.0+1.0) |> int),
                    ((b.Position.Y/4.0e8<m>*23.0+1.0) |> int))
            for a in asteroids do
                do set_cursor_on_body a
                do Console.Write(a.Name)
            do Thread.Sleep(100)

        let simulation() =
            let rec simulation m =
                do print_scene m
                let m' = simulation_step m
                do simulation m'
            do simulation f0


namespace general_discussion
module kaas = 
    open System

    let rec add_k k =
        function
        | [] -> []
        | x :: xs -> (x+k) :: (add_k k xs)

    let add_one' = add_k 1

    let rec map f =
        function
        | [] -> []
        | x :: xs -> (f x) :: (map f xs)

    let a = [1..10]

    let add_k' k = map (fun x -> x+k)

    let add_k'' k = map((+) k)
   

    let print_all l =
        for x in l do
            do System.Console.WriteLine(x.ToString())

    let rec filter p =
        function
        | [] -> []
        | x :: xs -> if p x then x :: filter p xs else filter p xs

    let rec fold s f =
        function
        | [] -> s
        | x :: xs -> fold (f s x) f xs

    let sincos_pairs = 
        [
        for i = 0 to 100 do
            let a = (float i)/100.0
            let theta = a*2.0*System.Math.PI
            yield cos theta, sin theta
        ]

    let incr_even l =
        [
            for x in l do
                if x % 2 = 0 then yield (x+1)
        ]

    let map'' f l = [ for x in l do yield f x ]
    let filter'' p l = [ for x in l do if p x then yield x ]

    let map_filter f p l = 
        [
            for x in l do
                if p x then yield f x
        ]
    
    let range = [1..10]
    let range_seq = Seq.ofList range
    let range' = 
        seq{
            for i = 1 to 100 do
                if i % 2 = 0 then
                    yield i /2,i*i
            } 
    let range'' = Seq.init 10 (fun x -> x,x*x)
    let N = Seq.initInfinite (fun x -> x)
    
    let rec funListLength = 
        function
        | [] -> 0
        | [_] -> 1
        | [_;_] -> 2
        | hd :: tail -> 1 + funListLength tail


    type Suit = 
        | Heart
        | Diamond
        | Spade
        | Club

    let suits = [ Heart; Diamond; Spade; Club ]

    type PlayingCard =
        | Ace of Suit
        | King of Suit
        | Queen of Suit
        | Jack of Suit
        | ValueCard of int * Suit

    let deckOfCards =
        [
            for suit in [ Spade; Club; Heart; Diamond ] do
                yield Ace(suit)
                yield King(suit)
                yield Queen(suit)
                yield Jack(suit)
                for value in 2 .. 10 do
                    yield ValueCard(value, suit)
        ]

    type BinaryTree =
        | Node of int * BinaryTree * BinaryTree
        | Empty

    let rec printInOrder tree =
        match tree with
        | Node (data, left, right)
            ->  printInOrder left
                printfn "Node %d" data
                printInOrder right
        | Empty
            -> ()

    let binTree =
        Node(2,
            Node(1, Empty, Empty),
            Node(4,
                Node(3, Empty, Empty),
                Node(5, Empty, Empty)
            )
        )

    let describeHoleCards cards =
        match cards with
        | []
        | [_]
            -> failwith "Too few cards."
        | cards when List.length cards > 2
            -> failwith "Too many cards."

        | [ Ace(_); Ace(_) ] -> "Pockets Rockets"
        | [ King(_); King(_) ] -> "Cowboys"

        | [ ValueCard(2, _); ValueCard(2,_)] -> "Ducks"

        | [ Queen(_); Queen(_) ]
        | [ Jack(_); Jack(_) ] -> "Pair of face cards"

        | [ ValueCard(x, _); ValueCard (y, _) ] when x = y -> "A pair"

        | [ first; second ] -> sprintf "Two cards: %A and %A" first second


    type Employee =
        | Manager of string * Employee list
        | Worker of string

    let rec printOrganization worker =
        match worker with
        | Worker(name) -> printfn "Employee %s" name
        | Manager(managerName, [ Worker(employeeName) ] )
            -> printfn "Manager %s with Worker %s" managerName employeeName

        | Manager(managerName, [ Worker(employee1); Worker(employee2) ] )
            -> printfn "Manager %s with two workers %s and %s" managerName employee1 employee2
        | Manager(managerName, workers)
            -> printfn "Manager %s with workers..." managerName 
               workers |> List.iter printOrganization


    type PlayingCard' =
        | Ace of Suit
        | King of Suit
        | Queen of Suit
        | Jack of Suit
        | ValueCard of int * Suit

        member this.Value =
            match this with
            | Ace(_) -> 11
            | King(_) | Queen (_) | Jack (_) -> 10
            | ValueCard(x, _) when x <= 10 && x >= 2
                              -> x
            | ValueCard (_) -> failwith "Card has an invalid value :("

    let highCard = Ace(Spade)
    let highCardValue = highCard.Value


    let planets =
        ref [
            "Mercury"; "Venus"; "Earth";
            "Mars"; "Jupiter"; "Saturn";
            "Uranus"; "Neptune"; "Pluto"
        ]
    open System

    type MutableCar = { Make : string; Model : string; mutable Miles : int }

    let driveForASeason car =
        let rng = new Random()
        car.Miles <- car.Miles + rng.Next() % 10000

    [<Measure>]
    type fahrenheit

    let printTemperature (temp : float<fahrenheit>) =
        if temp < 32.0<_> then
            printfn "Below freezing"
        elif temp < 65.0<_> then
            printfn "Cold"
        elif temp < 75.0<_> then
            printfn "Just right!"
        elif temp < 100.0<_> then
            printfn "Hot!"
        else
            printfn "lolol wat is deze"

    [<Measure>]
    type m

    type Point< [<Measure>] 'u >(x : float<'u>, y : float<'u>) =

        member this.X = x
        member this.Y = y

        member this.UnitlessX = float x
        member this.UnitlessY = float y

        member this.Length =
            let sqr x = x * x
            sqrt <| sqr this.X + sqr this.Y

        override this.ToString()=
            sprintf
                "{%f, %f}"
                this.UnitlessX
                this.UnitlessY


    let p = new Point<m>(10.0<m>, 10.0<m>)


    type ActiveCartData = { UnpaidItems: string list }
    type PaidCartData = { PaidItems: string list; Payment: float }

    type ShoppingCart =
        | EmptyCart
        | ActiveCart of ActiveCartData
        | PaidCart of PaidCartData


    let addItem cart item =
        match cart with
        | EmptyCart ->
            ActiveCart {UnpaidItems=[item]}
        | ActiveCart {UnpaidItems=existingItems} ->
            ActiveCart {UnpaidItems = item :: existingItems}
        | PaidCart _ ->
            cart

    let makePayment cart payment =
        match cart with
        | EmptyCart ->
            cart
        | ActiveCart {UnpaidItems=existingItems} ->
            PaidCart {PaidItems = existingItems; Payment = payment}
        | PaidCart _ ->
            cart

    open System

    type EmailAddress = string

    type UnverifiedData = EmailAddress

    type VerifiedData = EmailAddress * DateTime

    type T =
        | UnverifiedState of UnverifiedData
        | VerifiedState of VerifiedData

    let create email =
        UnverifiedState email

    let verified emailContactInfo dateVerified =
        match emailContactInfo with
        | UnverifiedState email ->
            VerifiedState (email, dateVerified)
        | VerifiedState _ ->
            emailContactInfo

    let sendVerificationEmail emailContactInfo =
        match emailContactInfo with
        | UnverifiedState email ->
            printfn "sending email"
        | VerifiedState _ ->
            ()

    let sendPasswordReset emailContactInfo =
        match emailContactInfo with
        | UnverifiedState email ->
            ()
        | VerifiedState _ ->
            printfn "sending password reset"

    let replace oldStr newStr (s:string) =
        s.Replace(oldValue = oldStr, newValue=newStr)

    let startsWith lookFor (s:string) =
        s.StartsWith(lookFor)

    let result =
        "hello"
        |> replace "h" "j"
        |> startsWith "j"

    let product n = 
        let initialValue = 1
        let action productSoFar x = productSoFar * x
        [1..n] |> List.fold action initialValue

    type NameAndSize = {Name:string;Size:int}

    let maxNameAndSize list =

        let innerMaxNameAndSize initialValue rest =
            let action maxSoFar x = if maxSoFar.Size < x.Size then x else maxSoFar
            rest |> List.fold action initialValue

        match list with
        | [] ->
            None
        | first::rest ->
            let max = innerMaxNameAndSize first rest
            Some max

    let list = [
        {Name="Alice"; Size=10}
        {Name="Bob"; Size=1}
        {Name="Carol"; Size=12}
        ]

    let maxNameAndSize' list =
        match list with
        | [] ->
            None
        | _ ->
            let max = list |> List.maxBy (fun item -> item.Size)
            Some max

    maxNameAndSize' list

    let mult3 x = x * 3
    let square x = x * x

    let logMsg msg x = printf "%s%i" msg x; x
    let logMsgN msg x = printfn "%s%i" msg x; x

    let mult3ThenSquareLogged =
        logMsg "before="
        >> mult3
        >> logMsg " after mult3="
        >> square
        >> logMsgN " result="

//    mult3ThenSquareLogged 5
//    [1..10] |> List.map mult3ThenSquareLogged


    type DateScale = Hour | Hours | Day | Days | Week | Weeks
    type DateDirection = Ago | Hence

    let getDate interval scale direction =
        let absHours = match scale with
                        | Hour | Hours -> 1 * interval
                        | Day | Days -> 24 * interval
                        | Week | Weeks -> 24 * 7 * interval
        let signedHours = match direction with
                            | Ago -> 1 * absHours
                            | Hence -> absHours
        System.DateTime.Now.AddHours(float signedHours)

//    let example1 = getDate 5 Days Ago
//    let example2 = getDate 1 Hour Hence


    type FluentShape = {
        label : string;
        color : string;
        onClick: FluentShape->FluentShape
        }

    let defaultShape =
        {label=""; color=""; onClick= fun shape -> shape}

    let click shape =
        shape.onClick shape

    let display shape =
        printfn "My label = %s and my color = %s" shape.label shape.color
        shape

    let setLabel label shape =
        {shape with FluentShape.label = label}

    let setColor color shape =
        {shape with FluentShape.color = color}

    let appendClickAction action shape =
        {shape with FluentShape.onClick = shape.onClick >> action}

    let setRedBox = setColor "red" >> setLabel "box"

    let setBlueBox = setRedBox >> setColor "blue"

    let changeColorOnClick color = appendClickAction (setColor color)

//    let redBox = defaultShape |> setRedBox
//    let blueBox = defaultShape |> setBlueBox
//
//    redBox
//        |> display
//        |> changeColorOnClick "green"
//        |> click
//        |> display
//
//    blueBox
//        |> display
//        |> appendClickAction (setLabel "box2" >> setColor "green")
//        |> click
//        |> display


    let firstPart, secondPart, _ = (1,2,3)

    let elem1::elem2::rest = [1..10]

    let listMatcher aList =
        match aList with
        | [] -> printfn "The list is empty"
        | [firstElement] -> printfn "The list has one element %A" firstElement
        | [first; second] -> printfn "list is %A and %A" first second
        | _ -> printfn "The list has more than two elements"

//    listMatcher [1;2;3;4]
//    listMatcher [1;2]
//    listMatcher [1]
//    listMatcher []


    type USAddress =
        {Street:string; City:string; State:string; Zip:string}
    type UKAddress =
        {Street:string; Town:string; PostCode:string}
    type Address = US of USAddress | UK of UKAddress
    type Person =
        {Name:string; Address:Address}

    let alice = {
        Name="Alice";
        Address = US {Street="123 Main"; City="LA"; State="CA"; Zip="123123"}}

    let bob = {
        Name="Bob"
        Address = UK {Street="123 Kaas"; Town="London"; PostCode="123aa"}}

//    printfn "Alice is %A" alice
//    printfn "Bob is %A" bob


    let addingCalculator input = input + 1

    let loggingCalculator innerCalculator input =
        printfn "input is %A" input
        let result = innerCalculator input
        printfn "result is %A" result
        result

    let add1 input = input + 1
    let times2 input = input * 2

    let genericLogger anyFunc input =
        printfn "input is %A" input
        let result = anyFunc input
        printfn "result is %A" result
        result

    let add1WithLogging = genericLogger add1
    let times2WithLogging = genericLogger times2

//    [1..5] |> List.map add1WithLogging


    type Animal(noiseMakingStrategy) =
        member this.MakeNoise =
            noiseMakingStrategy() |> printfn "Making noise %s"

    let meowing() = "meow"
    let cat = Animal(meowing)
    cat.MakeNoise

    let genericLogger' before after anyFunc input =
        before input
        let result = anyFunc input
        after result
        result

//    genericLogger'
//        (fun x -> printf "before=%i. " x)
//        (fun x -> printfn " after=%i." x)
//        add1
//        2
//
//    genericLogger'
//        (fun x -> printf "started with=%i " x)
//        (fun x -> printfn " ended with =%i" x)
//        add1
//        2


    let (|Int|_|) str =
        match System.Int32.TryParse(str) with
        | (true, int) -> Some(int)
        | _ -> None

    let (|Bool|_|) str =
        match System.Boolean.TryParse(str) with
        | (true, bool) -> Some(bool)
        | _ -> None

    let testParse str =
        match str with
        | Int i -> printfn "The value is an int %i" i
        | Bool b -> printfn "The value is a bool %b" b
        | _ -> printfn "The value %s is something else" str

//    testParse "12"
//    testParse "true"
//    testParse "abc"


    let (|MultOf3|_|) i = if i % 3 = 0 then Some MultOf3 else None
    let (|MultOf5|_|) i = if i % 5 = 0 then Some MultOf5 else None

    let fizzBuzz i =
        match i with
        | MultOf3 & MultOf5 -> printf "FizzBuz,  "
        | MultOf3 -> printf "Fizz, "
        | MultOf5 -> printf "Buzz, "
        | _ -> printf "%i " i

//    [1..20] |> List.iter fizzBuzz

    type State = New | Draft | Published | Inactive | Discontinued
    let handleState state =
        match state with
        | Inactive -> ()
        | Draft -> ()
        | New -> ()
        | Discontinued -> ()


    let rec movingAverages list =
        match list with
        | [] -> []
        | x::y::rest ->
            let avg = (x+y)/2.0
            avg :: movingAverages (y::rest)
        | [_] -> []

//    movingAverages [5.0;4.0;3.0]

//    type CartItem = string
//
//    type EmptyState = NoItems
//
//    type ActiveState = { UnpaidItems : CartItem list; }
//    type PaidForState = { PaidItems : CartItem list;
//                          Payment : decimal }
//
//    type Cart =
//        | Empty of EmptyState
//        | Active of ActiveState
//        | PaidFor of PaidForState
//
//    let addToEmptyState item =
//        Cart.Active {UnpaidItems=[item]}
//
//    let addToActiveState state itemToAdd =
//        let newList = itemToAdd :: state.UnpaidItems
//        Cart.Active {state with UnpaidItems=newList }
//
//    let removeFromActiveState state itemToRemove =
//        let newList = state.UnpaidItems
//                      |> List.filter (fun i -> i<>itemToRemove)
//
//        match newList with
//        | [] -> Cart.Empty NoItems
//        | _ -> Cart.Active {state with UnpaidItems=newList}
//
//    let payForActiveState state amount =
//        Cart.PaidFor {PaidItems=state.UnpaidItems; Payment=amount}
//
//    type EmptyState with
//        member this.Add = addToEmptyState
//
//    type ActiveState with
//        member this.Add = addToActiveState this
//        member this.Remove = removeFromActiveState this
//        member this.Pay = payForActiveState this
//
//    let addItemToCart cart item =
//        match cart with
//        | Empty state -> state.Add item
//        | Active state -> state.Add item
//        | PaidFor state ->
//            printfn "Error: The cart is paid for"
//            cart
//
//    let removeItemFromCart cart item =
//        match cart with
//        | Empty state ->
//            printfn "Error: The cart is empty"
//            cart
//        | Active state ->
//            state.Remove item
//        | PaidFor state ->
//            printfn "Error: The cart is paid for"
//            cart
//
//    let displayCart cart =
//        match cart with
//        | Empty state ->
//            printfn "The cart is empty"
//        | Active state ->
//            printfn "Th cart contains %A unpaid items"
//                                                    state.UnpaidItems
//        | PaidFor state ->
//            printfn "The cart contains %A paid items. Amount paid: %f"
//                                            state.PaidItems state.Payment
//
//    type Cart with
//        static member NewCart = Cart.Empty NoItems
//        member this.Add = addItemToCart this
//        member this.Remove = removeItemFromCart this
//        member this.Display = displayCart this
//
//    let emptyCart = Cart.NewCart
//    printf "emptyCart="; emptyCart.Display
//
//    let cartA = emptyCart.Add "A"
//    printf "cartA"; cartA.Display
//
//    let cartAB = cartA.Add "B"
//    printf "cartAB="; cartAB.Display
//
//    let cartB = cartAB.Remove "A"
//    printf "cartB="; cartB.Display
//
//    let emptyCart2 = cartB.Remove "B"
//    printf "emptyCart2="; emptyCart2.Display
//
//    let emptyCart3 = emptyCart2.Remove "B"
//    printf "emptyCart3="; emptyCart3.Display
//
//    let cartAPaid =
//        match cartA with
//        | Empty _ | PaidFor _ -> cartA
//        | Active state -> state.Pay 100m
//    printf "cartAPaid="; cartAPaid.Display
//
//    let emptyCartPaid =
//        match emptyCart with
//        | Empty _ | PaidFor _ -> emptyCart
//        | Active state -> state.Pay 100m
//    printf "emptyCartPaid="; emptyCartPaid.Display
//
//    let cartABPaid =
//        match cartAB with
//        | Empty _ | PaidFor _ -> cartAB
//        | Active state -> state.Pay 100m
//
//    let cartABPaidAgain =
//        match cartABPaid with
//        | Empty _ | PaidFor _ -> cartABPaid
//        | Active state -> state.Pay 100m

//    open System
//    open System.Threading
//
//    let userTimerWithAsync =
//
//        let timer = new System.Timers.Timer(2000.0)
//        let timerEvent = Async.AwaitEvent (timer.Elapsed) |> Async.Ignore
//
//        printfn "Waiting for timer at %O" DateTime.Now.TimeOfDay
//        timer.Start()
//
//        printfn "Doing something useful while waiting for event"
//
//        Async.RunSynchronously timerEvent
//
//        printfn "Timer ticked at %O" DateTime.Now.TimeOfDay
//
//
//    let sleepWorkflow = async{
//        printfn "Starting sleep workflow at %O" DateTime.Now.TimeOfDay
//        do! Async.Sleep 2000
//        printfn "Finished sleep workflow at %O" DateTime.Now.TimeOfDay
//        }
//
//    Async.RunSynchronously sleepWorkflow
//
//
//    let nestedWorkflow = async{
//
//        printfn "Starting parent"
//        let! childWorkflow = Async.StartChild sleepWorkflow
//
//        do! Async.Sleep 100
//        printfn "Doing something useful while waiting "
//
//        let! result = childWorkflow
//
//        printfn "Finished parent"
//        }
//
//    Async.RunSynchronously nestedWorkflow
//
//    let testLoop = async{
//        for i in [1..100] do
//            printf "%i before.." i
//
//            do! Async.Sleep 10
//            printfn "..after"
//
//        }
//
//    Async.RunSynchronously testLoop
//
//    let cancellationSource = new CancellationTokenSource()
//    
//    Async.Start(testLoop, cancellationSource.Token)
//
//    Thread.Sleep(200)
//
//    cancellationSource.Cancel()

    let adderWithPluggableLogger logger x y =
        logger "x" x
        logger "y" y
        let result = x + y
        logger "x+y" result
        result

    let consoleLogger argName argValue =
        printfn "%s=%A" argName argValue

    let addWithConsoleLogger = adderWithPluggableLogger consoleLogger

//    addWithConsoleLogger 1 2
//    addWithConsoleLogger 42 99

    type Name = {first:string; last:string}
    let bob' = {first="Bob"; last="Smith"}

    let f1 name =
        let {first=f; last=l} = name
        printfn "first=%s; last=%s" f l

    let f2 {first=f; last=l} =
        printfn "first=%s; last=%s" f l

//    f1 bob'
//    f2 bob'


    let addThreeNumbers x y z =
        let add n =
            fun x -> x + n

        x |> add y |> add z

//    addThreeNumbers 2 3 4


    let validateSize max n =

        let printError() =
            printfn "Oops: '%i' is bigger than max: '%i'" n max

        if n > max then printError()

//    validateSize 10 9

    let sumNumbersUpTo max =

        let rec recursiveSum n sumSoFar =
            match n with
            | 0 -> sumSoFar
            | _ -> recursiveSum (n-1) (n+sumSoFar)

        recursiveSum max 0

//    sumNumbersUpTo 10

module Person =
    type T = {First:string; Last:string} with
        member this.FullName =
            this.First + " " + this.Last
            
    let create first last =
        {First=first; Last=last}

module kaas1 =

    let person = Person.create "John" "Doe"
    let fullname = person.FullName


type Product = {SKU:string; Price: float} with
    member this.CurriedTotal qty discount =
        (this.Price * float qty) - discount

    member this.TupleTotal(qty,discount) =
        (this.Price * float qty) - discount

    member this.TupleTotal2(qty,?discount) =
        let extPrice = this.Price * float qty
        match discount with
        | None -> extPrice
        | Some discount -> extPrice - discount


    member this.TupleTotal2'(qty,?discount) =
        let extPrice = this.Price * float qty
        let discount = defaultArg discount 0.0
        extPrice - discount

module test2 =
    let product = {SKU="ABC"; Price=2.0}
    let total1 = product.CurriedTotal 10
    let total2 = product.TupleTotal(10,1.0)

namespace calculator
module calculatorSample =

    open System

    type Stack = StackContents of float list

    let newStack = StackContents [1.0;2.0;3.0]

    let (StackContents contents) = newStack

    let push x (StackContents contents) =
        StackContents (x::contents)

    let pop (StackContents contents) =
        match contents with
        | top::rest ->
            let newStack = StackContents rest
            (top,newStack)
        | [] ->
            failwith "Stack underflow"

    let ONE = push 1.0
    let TWO = push 2.0
    let THREE = push 3.0
    let FOUR = push 4.0
    let FIVE = push 5.0
    let EMPTY = StackContents []

    let binary mathFn stack =
        let y,stack' = pop stack
        let x,stack'' = pop stack'
        let z = mathFn y x
        push z stack''

    let ADD = binary (+)
    let SUB = binary (-)
    let MUL = binary (*)
    let DIV = binary (/)

    let SHOW stack =
        let x,_ = pop stack
        printfn "The answer is %f" x
        stack

    let DUP stack =
        let x,_ = pop stack
        push x stack

    let SWAP stack =
        let x,s = pop stack
        let y,s' = pop s
        push y (push x s')

    let START = EMPTY

    START
        |> ONE |> TWO |> SHOW

    START
        |> ONE |> TWO |> ADD |> SHOW
        |> THREE |> ADD |> SHOW

    
    // Overview of F# expressions

    let x1 = fun () -> 1

    let x2 = match 1 with
             | 1 -> "a"
             | _ -> "b"

    let x3 = if true then "a" else "b"

    let x4 = for i in [1..10]
                do printf "%i" i

    let x5 = try
                let result = 1/0
                printfn "%i" result
             with 
                | e ->
                    printfn "%s" e.Message

    let x6 = let n=1 in n+2

    let test b t f = if b then t else f

    test true (printfn "true") (printfn "false")

    let f = test true (lazy (printfn "true")) (lazy (printfn "false")) 

    f.Force()

    let makeResource name =
        { new System.IDisposable
          with member this.Dispose() = printfn "%s disposed" name }

    let exampleUseBinding name =
        use myResource = makeResource name
        printfn "done"

    exampleUseBinding "hello"

    [2..10]
    |> List.map (fun i ->
            match i with
            | 2 | 3 | 5 | 7 -> sprintf "%i is prime" i
            | _ -> sprintf "%i is not prime" i
            )

    let y =
        match [1;2;3] with
        | [1;x;y] -> printfn "x=%A y=%A" x y
        | 1::tail -> printfn "tail=%A" tail
        | [] -> printfn "empty"


    let rec loopAndPrint aList =
        match aList with
        | [] ->
            printfn "empty"
        | x::xs ->
            printfn "element = %A," x
            loopAndPrint xs

    loopAndPrint [1..5]

    let rec loopAndSum aList sumSoFar =
        match aList with
        | [] ->
            sumSoFar
        | x::xs ->
            let newSumSoFar = sumSoFar + x
            loopAndSum xs newSumSoFar

    loopAndSum [1..5] 0

    let aTuple = (1,2)
    match aTuple with
    | (1,_) -> printfn "first part is 1"
    | (_,2) -> printfn "second part is 2"

    type Person = {First:string; Last:string}
    type Person'' = {First:string; Last:string}
    let person = {First="John"; Last="doe"}
    match person with
    | {First="John"} -> printfn "Matched John"
    | _ -> printfn "Not john :("

    type IntOrBool = I of int | B of bool
    let intOrBool = I 42
    match intOrBool with
    | I i -> printfn "Int=%i" i
    | B b -> printfn "Bool=%b" b

    let makeOrdered aTuple =
        match aTuple with
        | (x,y) when x > y -> (y,x)
        | _ -> aTuple

    makeOrdered (231,21)

    let isAM aDate =
        match aDate:System.DateTime with
        | x when x.Hour <= 12 ->
            printfn "AM"

        | _ ->
            printfn "PM"

    isAM System.DateTime.Now

    open System.Text.RegularExpressions

    let classifyString aString =
        match aString with
        | x when Regex.Match(x,@".+@.+").Success ->
            printfn "%s is an email" aString
        | _ ->
            printfn "%s is something else" aString
        
    classifyString "aliceexamplecom"

    let (|EmailAddress|_|) input =
        let m = Regex.Match(input,@".+@.+")
        if (m.Success) then Some input else None

    let classifyString' aString =
        match aString with
        | EmailAddress x ->
            printfn "%s is an email" x
        | _ ->
            printfn "%s is something else" aString
        
    classifyString' "kaas@bier.com"
    classifyString' "kaas"

    // Function keyword examples

    let f' aValue =
        match aValue with
        | x ->
            match x with
            | _ -> "something"

    let f'' =
        function
        | x ->
            function
            | _ -> "something"

//    [2..10] |> List.map (fun i ->
//            match i with
//            | i ->
//                if (i % 2 = 0) then sprintf "%i is prime" i
//                else sprintf "%i is not prime" i
//            )
// dafuq how does dis work :(

    [2..10] |> List.map(fun i ->
            match i with
            | 2 | 3 | 5 | 7 -> sprintf "%i is prime" i
            | _ -> sprintf "%i is not prime" i
            )

    [2..10] |> List.map(function
            | 2 | 3 | 5 | 7 -> sprintf "prime"
            | _ -> sprintf "not prime"
            )

    try
        failwith "fail"
    with
        | Failure msg -> "caught: " + msg
        | :? System.InvalidOperationException as ex -> "unexpected"
    

    let debugMode = false
    try
        failwith "fail"
    with
        | Failure msg when debugMode ->
            reraise()
        | Failure msg when not debugMode ->
            printfn "silently logged in production: %s" msg


    let times6 x = x * 6

    let isAnswerToEverything x =
        match x with
        | 42 -> (x,true)
        | _ -> (x,false)

    [1..10] |> List.map (times6 >> isAnswerToEverything)

    let rec loopAndSum' aList sumSoFar =
        match aList with
        | [] ->
            sumSoFar
        | x::xs ->
            let newSumSoFar = sumSoFar + x
            loopAndSum xs newSumSoFar

    let loopAndSum'' aList = List.sum aList
    [1..10] |> loopAndSum''

    let loopAndSum''' aList = List.reduce (+) aList
    [1..10] |> loopAndSum'''

    let loopAndSum'''' aList = List.fold (fun sum i -> sum+i) 0 aList
    [1..10] |> loopAndSum''''

    let addOneIfValid optionalInt =
        match optionalInt with
        | Some i -> Some (i + 1)
        | None -> None

    Some 42 |> addOneIfValid

    let addOneIfValid' optionalInt =
        optionalInt |> Option.map (fun i -> i + 1)

    Some 42 |> addOneIfValid'

    type TemperatureType = F of float | C of float

    module Temperature =
        let fold fahrenheitFunction celsiusFunction aTemp =
            match aTemp with
            | F f -> fahrenheitFunction f
            | C c -> celsiusFunction c

    let fFever tempF =
        if tempF > 100.0 then "Fever!" else "OK"

    let cFever tempC =
        if tempC > 38.0 then "Fever!" else "OK"

    let isFever aTemp = Temperature.fold fFever cFever aTemp

    let normalTemp = C 37.0
    let result1 = isFever normalTemp

    Console.ReadKey()

namespace patternmatching
module firstversion =
    
    let OrderByName = "N"
    let OrderBySize = "S"

    type OrderByOption = OrderBySize | OrderByName
    type SubdirectoryOption = IncludeSubdirectories | ExcludeSubdirectories
    type VerboseOption = VerboseOutput | TerseOutput

    type CommandLineOptions' = { 
        verbose: VerboseOption;
        subdirectories: SubdirectoryOption;
        orderby: OrderByOption
        }



    let rec parseCommandLineRec args optionsSoFar =
        match args with
        | [] ->
            optionsSoFar
        | "/v"::xs ->
            let newOptionsSoFar = { optionsSoFar with verbose = VerboseOutput }
            parseCommandLineRec xs newOptionsSoFar
        | "/s"::xs ->
            let newOptionsSoFar = { optionsSoFar with subdirectories = IncludeSubdirectories }
            parseCommandLineRec xs newOptionsSoFar
        | "/o"::xs ->
            match xs with
            | "S"::xss ->
                let newOptionsSoFar = { optionsSoFar with orderby=OrderBySize }
                parseCommandLineRec xss newOptionsSoFar
            | "N"::xss ->
                let newOptionsSoFar = { optionsSoFar with orderby=OrderByName }
                parseCommandLineRec xss newOptionsSoFar
            | _ ->
                eprintfn "OrderBy needs a second argument"
                parseCommandLineRec xs optionsSoFar
        | x::xs ->
            eprintfn "Option '%s' is unrecognized" x
            parseCommandLineRec xs optionsSoFar

    
    let parseCommandLine args =
        let defaultOptions = {
            verbose = TerseOutput;
            subdirectories = ExcludeSubdirectories;
            orderby = OrderByName
            }

        parseCommandLineRec args defaultOptions


    parseCommandLine ["/v"]

module romannumerals =

    type RomanDigit = I | V | X | L | C | D | M
    type RomanNumeral = RomanNumeral of RomanDigit list

    let digitToInt =
        function
        | I -> 1
        | V -> 5
        | X -> 10
        | L -> 50
        | C -> 100
        | D -> 500
        | M -> 1000

    let rec digitsToInt =
        function
        | [] -> 0
        | smaller::larger::ns when smaller < larger ->
            (digitToInt larger - digitToInt smaller) + digitsToInt ns
        | digit::ns ->
            digitToInt digit + digitsToInt ns

    let toInt (RomanNumeral digits) = digitsToInt digits

    type ParsedChar =
        | Digit of RomanDigit
        | BadChar of char

    let charToRomanDigit =
        function
        | 'I' -> Digit I
        | 'V' -> Digit V
        | 'X' -> Digit X
        | 'L' -> Digit L
        | 'C' -> Digit C
        | 'D' -> Digit D
        | 'M' -> Digit M
        | ch -> BadChar ch


    let toRomanDigitList (s:string) =
        s.ToCharArray()
        |> List.ofArray
        |> List.map charToRomanDigit

    let toRomanNumeral s =
        toRomanDigitList s
        |> List.choose (
            function
            | Digit digit ->
                Some digit
            | BadChar ch ->
                eprintfn "%c is not a valid character" ch
                None
            )
        |> RomanNumeral

    let runsAllowed =
        function
        | I | X | C | M -> true
        | V | L | D -> false

    let noRunsAllowed = runsAllowed >> not

    let rec isValidDigitList digitList =
        match digitList with
        | [] -> true

        | d1::d2::d3::d4::d5::_
            when d1=d2 && d1=d2 && d1=d4 && d1=d5 ->
                false

        | d1::d2::_
            when d1=d2 && noRunsAllowed d1 ->
                false

        | d1::d2::d3::d4::higher::ds
            when d1=d2 && d1=d3 && d1=d4
            && runsAllowed d1
            && higher > d1 ->
                false

        | d1::d2::d3::higher::ds
            when d1=d2 && d1=d3
            && runsAllowed d1
            && higher > d1 ->
                false
        | d1::d2::higher::ds
            when d1=d2
            && runsAllowed d1
            && higher > d1 ->
                false

        | d1::d2::d3::_ when d1<d2 && d2<=d3 ->
            false

        |_::ds ->
            isValidDigitList ds

    let isValid (RomanNumeral digitList) =
        isValidDigitList digitList

module romannumeralsv2 =
    type RomanDigit =
    | I | II | III | IIII
    | IV | V
    | IX | X | XX | XXX | XXXX
    | XL | L
    | XC | C | CC | CCC | CCCC
    | CD | D
    | CM | M | MM | MMM | MMMM

    type RomanNumeral = RomanNumeral of RomanDigit list

    let digitToInt =
        function
        | I -> 1 | II -> 2 | III -> 3 | IIII -> 4
        | IV -> 4 | V -> 5
        | IX -> 9 | X -> 10 | XX -> 20 | XXX -> 30 | XXXX -> 40
        | XL -> 40 | L -> 50
        | XC -> 90 | C -> 100 | CC -> 200 | CCC -> 300 | CCCC -> 400
        | CD -> 400 | D -> 500
        | CM -> 900 | M -> 1000 | MM -> 2000 | MMM -> 3000 | MMMM -> 4000

    let digitsToInt list =
        list |> List.sumBy digitToInt

    let toInt (RomanNumeral digits) = digitsToInt digits

    type ParsedChar =
        | Digit of RomanDigit
        | BadChar of char
    
    let rec toRomanDigitListRec charList =
        match charList with
        | 'I'::'I'::'I'::'I'::ns ->
            Digit IIII :: (toRomanDigitListRec ns)
        | 'X'::'X'::'X'::'X'::ns -> 
            Digit XXXX :: (toRomanDigitListRec ns)
        | 'C'::'C'::'C'::'C'::ns ->
            Digit CCCC :: (toRomanDigitListRec ns)
        | 'M'::'M'::'M'::'M'::ns -> 
            Digit MMMM :: (toRomanDigitListRec ns)

        | 'I'::'I'::'I'::ns -> 
            Digit III :: (toRomanDigitListRec ns)
        | 'X'::'X'::'X'::ns -> 
            Digit XXX :: (toRomanDigitListRec ns)
        | 'C'::'C'::'C'::ns -> 
            Digit CCC :: (toRomanDigitListRec ns)
        | 'M'::'M'::'M'::ns -> 
            Digit MMM :: (toRomanDigitListRec ns)

        | 'I'::'I'::ns -> 
            Digit II :: (toRomanDigitListRec ns)
        | 'X'::'X'::ns -> 
            Digit XX :: (toRomanDigitListRec ns)
        | 'C'::'C'::ns -> 
            Digit CC :: (toRomanDigitListRec ns)
        | 'M'::'M'::ns -> 
            Digit MM :: (toRomanDigitListRec ns)

        | 'I'::'V'::ns -> 
            Digit IV :: (toRomanDigitListRec ns)
        | 'I'::'X'::ns -> 
            Digit IX :: (toRomanDigitListRec ns)
        | 'X'::'L'::ns -> 
            Digit XL :: (toRomanDigitListRec ns)
        | 'X'::'C'::ns -> 
            Digit XC :: (toRomanDigitListRec ns)
        | 'C'::'D'::ns -> 
            Digit CD :: (toRomanDigitListRec ns)
        | 'C'::'M'::ns -> 
            Digit CM :: (toRomanDigitListRec ns)

        | 'I'::ns -> 
            Digit I :: (toRomanDigitListRec ns)
        | 'V'::ns -> 
            Digit V :: (toRomanDigitListRec ns)
        | 'X'::ns -> 
            Digit X :: (toRomanDigitListRec ns)
        | 'L'::ns -> 
            Digit L :: (toRomanDigitListRec ns)
        | 'C'::ns -> 
            Digit C :: (toRomanDigitListRec ns)
        | 'D'::ns -> 
            Digit D :: (toRomanDigitListRec ns)
        | 'M'::ns -> 
            Digit M :: (toRomanDigitListRec ns)

        | badChar::ns ->
            BadChar badChar :: (toRomanDigitListRec ns)

        | [] ->
            []

    let toRomanDigitList (s:string) =
        s.ToCharArray()
        |> List.ofArray
        |> toRomanDigitListRec

    let toRomanNumeral s =
        toRomanDigitList s
        |> List.choose (
            function
            | Digit digit ->
                Some digit
            | BadChar ch ->
                eprintfn "%c is not a valid character" ch
                None
            )
        |> RomanNumeral

    let rec isValidDigitList digitList =
        match digitList with

        | [] -> true

        | d1::d2::_
            when d1 <= d2 ->
                false
        | _::ds ->
            isValidDigitList ds

    let isValid (RomanNumeral digitList) =
        isValidDigitList digitList

    "III" |> toRomanNumeral |> isValid

module types =

    type A = int * int
    type B = {FirstName:string; LastName:string}
    type C = Circle of int | Rectangle of int * int
    type D = Day | Month | Year
    type E<'a> = Choice1 of 'a | Choice2 of 'a * 'a

    type MyClass(initX:int) =
        let x = initX
        member this.Method() = printf "x=%i" x
    
    let a = (1,1)
    let (a1,a2) = a
    let b = { FirstName="Bob"; LastName="Smith" }
    let c = Circle 99
    let c' = Rectangle (2,1)
    let d = Month
    let e = Choice1 "a"
    let myVal = MyClass 99
    myVal.Method()
