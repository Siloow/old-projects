// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

module DataAccess

module Coroutines =
    open Microsoft.FSharp
    open Microsoft.FSharp.Core
    open System

    type Coroutine<'a> = Unit -> CoroutineStep<'a>
    and CoroutineStep<'a> = 
        Return of 'a
        | Yield of Coroutine<'a>
        | ArrowYield of Coroutine<'a>

    type CoroutineBuilder() =
        member this.Return(x:'a) : Coroutine<'a> =
            fun () -> Return x

        member this.Bind(p : Coroutine<'a>,
                         k : 'a -> Coroutine<'b>) : Coroutine<'b> =
            fun () ->
                match p () with
                | Return x -> k x ()
                | Yield p' -> Yield(this.Bind(p',k))
                | ArrowYield p' -> ArrowYield(this.Bind(p', k))

        member this.Combine(p1:Coroutine<'a>,
                            p2:Coroutine<'b>) : Coroutine<'b> =
            this.Bind(p1, fun _ -> p2)

        member this.Zero() : Coroutine<Unit> = this.Return()

        member this.ReturnFrom(s:Coroutine<'a>) = s

        member this.Delay s = s()
        member this.Run s = s
           
    let co = CoroutineBuilder()

    let yield_ : Coroutine<Unit> =
        fun s -> Yield(fun s -> Return())
    let arrow_yield_ : Coroutine<Unit> = 
        fun s -> ArrowYield(fun s -> Return())
    let ignore_ (s:Coroutine<'a>) : Coroutine<Unit> = 
        co {
            let! _ = s
            return ()
        }

    let rec (.||) (s1:Coroutine<'a>) (s2:Coroutine<'b>) :
        Coroutine<Choice<'a, 'b>> = 
        fun s ->
            match s1 s, s2 s with
            | Return x, _ -> Return(Choice1Of2 x)
            | _, Return y -> Return(Choice2Of2 y)
            | ArrowYield k1,_ ->
                co {
                    let! res = k1
                    return Choice1Of2 res
                } |> Yield
            | _, ArrowYield k2 ->
                co {
                    let! res = k2
                    return Choice2Of2 res
                } |> Yield
            | Yield k1, Yield k2 -> (.||) k1 k2 |> Yield

    let (.||>) s1 s2 = ignore_ (s1 .|| s2)

    let rec (=>) (c:Coroutine<bool>) (s:Coroutine<'a>) :
        Coroutine<'a> =
        co {
            let! x = c
            if x then
                do! arrow_yield_
                let! res = s
                return res
            else
                do! yield_
                return! (=>) c s
        }

    let rec (==>) (c: Coroutine<bool>) (s:Coroutine<'a>) :
        Coroutine<'a> =
        co {
            let! x = c
            if x then
                do! yield_
                let! res = s
                return res
            else
                do! yield_
                return! (==>) c s
        }

    let rec repeat_ (s:Coroutine<Unit>) : Coroutine<Unit> =
        co {
            do! s
            return! repeat_ s
        }

    let wait_doing (action:float -> Coroutine<Unit>)
        (interval:float) : Coroutine<Unit> =
        let time : Coroutine<DateTime> =
            fun _ -> Return(DateTime.Now)
        co {
            let! t0 = time
            let rec wait() =
                co {
                    let! t = time
                    let dt = (t-t0).TotalSeconds
                    if dt < interval then
                        do! yield_
                        do! action dt
                        return! wait()
                }
            do! wait()
        }

    let wait = wait_doing (fun (dt:float) -> co { return () })

module sqllogic = 
    open System
    open System.Linq
    open System.Data
    open FSharp.Data.TypeProviders
    open System.Text.RegularExpressions    
    open Coroutines
    open System.Threading

    [<Literal>]
    let connectionString = "Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=assignment01;Integrated Security=True;"

    // Initialize connection
    type dbSchema = SqlDataConnection<connectionString>

    // Get the database info
    let db = dbSchema.GetDataContext()

    // For debuggin : log the db info
    db.DataContext.Log <- System.Console.Out


    let actions() =
        co{
         do! yield_
         let db = dbSchema.GetDataContext()

         return ()
        }

    

    type AllTables (address, degree, employee) =
        let address = address
        let degree = degree
        let employee = employee
        member this.sendAddress() = address

    let a = AllTables(db.Address, db.Degree, db.Employee)

    let b = a.sendAddress()


    let table1 = db.Address

    let query  = 
        query {
            for row in db.Address do
            select row
        }


    let newRecord = new dbSchema.ServiceTypes.Address(  Country = "The Netherlands",
                                                        City = "Rottedam",
                                                        Street = "Random st",
                                                        Number = new Nullable<int>15,
                                                        Postal_code = "1234ab" )

    db.Address.InsertOnSubmit(newRecord)

    try
        db.DataContext.SubmitChanges()
        printfn "SUCCES"
    with
        | exn -> printfn "Exception: %s" exn.Message


    query |> Seq.iter (fun row -> printfn "%s %s" row.City row.Country)



    // type DbContext = dbSchema.ServiceTypes.SimpleDataContextTypes.Assignment01



//type Script<'a, 's> = 's -> Step<'a, 's>
//    and Step<'a, 's>  = Done of 'a | Next of Script<'a, 's>
//
//
//type ScriptBuilder() =
//    member this.Bind(p:Script<'a, 's>, k:'a->Script<'b, 's>)
//        : Script<'b, 's> =
//        fun s ->
//            match p s with
//            | Done x -> k x s
//            | Next p' -> Next(this.Bind(p', k))
//
//    member this.Return(x:'a) : Script<'a, 's> = fun s -> Done x
//
//let script = ScriptBuilder()
//
//let get_state : Script<'s, 's> = fun s -> Done s
//let suspend : Script<Unit, 's> = fun s -> Next(fun s -> Done())
//
//let rec fibonacci n : Script<int, Unit> =
//    script{
//        match n with
//        | 0 -> return 0
//        | 1 -> return 1
//        | n ->
//            do! suspend
//            let! n1 = fibonacci (n-1)
//            let! n2 = fibonacci (n-2)
//            return n1+n2
//    }
//
//let main_loop() =
//    let rec main_loop (f:Script<int, Unit>) =
//        do printf "I am alive \n"
//        match f () with
//        | Done result ->
//          do printf "The result is %d \n" result
//        | Next f' ->
//          main_loop f'
//    do main_loop (fibonacci 5)
//
//main_loop()
//
//Console.ReadKey()
//
//
//type LoggingBuilder() =
//    let log p = printfn "experssion is %A" p
//
//    member this.Bind(x, f) =
//        log x
//        f x
//
//    member this.Return(x) = 
//        x
//
//let logger = new LoggingBuilder()
//
//let loggedWorkflow =
//    logger
//        {
//        let! x = 42
//        let! y = 43
//        let! z = x + y
//        return z
//        }
//
//
//type MaybeBuilder() = 
//    
//    member this.Bind(x, f) =
//        match x with
//        | None -> None
//        | Some a -> f a
//
//    member this.Return(x) =
//        Some x
//
//let maybe = new MaybeBuilder()
//
//let divideBy bottom top =
//    if bottom = 0
//    then None
//    else Some(top/bottom)
//
//let divideByWorkflow' init x y z =
//    maybe
//        {
//        let! a = init |> divideBy x
//        let! b = a |> divideBy y
//        let! c = b |> divideBy z
//        return c
//        }
//
//let divideByWorkflow init x y z =
//    let a = init |> divideBy x
//    match a with
//    | None -> None
//    | Some a' ->
//        let b = a' |> divideBy y
//        match b with
//        | None -> None
//        | Some b' ->
//            let c = b' |> divideBy z
//            match c with
//            | None -> None
//            | Some c' ->
//                Some c'
//
//let good = divideByWorkflow' 12 3 2 1
//let bad = divideByWorkflow' 12 3 0 1
//
//open System.Net
//let req2 = HttpWebRequest.Create("http://google.com")
//let req3 = HttpWebRequest.Create("http://bing.com")
//
//async {
//    use! resp2 = req2.AsyncGetResponse()  
//    printfn "Downloaded %O" resp2.ResponseUri
//
//    use! resp3 = req3.AsyncGetResponse()  
//    printfn "Downloaded %O" resp3.ResponseUri
//
//    } |> Async.RunSynchronously
//
//
//let (>>=) m f = 
//    printfn "expression is %A" m
//    f m
//
//let loggingWorkflow = 
//    1 >>= (+) 2 >>= (*) 42 >>= id

module Math2 =
    (*
    We define a new type with the "type" keyword. The simplest types we can define are types with no definition, which are just placeholders.
    Such types are defined as units of measure, and are preceded by the [<Measure>] attribute.

    We define m (meters), kilograms (kg) and seconds (s).
    *)
    [<Measure>]
    type m

    [<Measure>]
    type kg

    [<Measure>]
    type s

    (*
    Units of measure may be composite. For example, we could define newtons (N) as the unit of measure for force, that is mass times acceleration or kg * m / s^2
    *)
    [<Measure>]
    type N = kg * m / s^2

    (*
    Units of measure are very simple types, but they do not serve much porpose apart from classifying values of the same type into different groups; for example, we could
    try and sum 2.0<m> + 1.0<s> and the F# compiler would give us an error.

    A more interesting type definition is that of records. Records are very similar to tuples, but with a main difference: the elements (fields) of a record have a name.
    We could, of course, define a vector as float * float. Unfortunately, it would be very easy to make the mistake of losing track of which element is the x and which is the y.
    A record does exactly this: it keeps track of "which element of the tuple is which".

    Any definition of a type can contain a unit of measure, right after its name, between angle brackets. This means that such a type will need to be handled by also giving its values a unit of measure.
    We could avoid doing so, but for a complex physics simulation the advantage of having the compiler track what is in meters and what is in meters per second is quite important.
    
    A simple record definition would be:

    type Vector2 = { X : float; Y : float }

    where a record with units of measure would be defined as follows. Notice that the fields of the record are not simple floating point numbers, but rather they are floating point numbers with the same unit of measure of the record itself:
    *)
    type Vector2<[<Measure>] 'a> =
      {
        X : float<'a>
        Y : float<'a>
      }
      (* 
      A record may have members, which are functions that perform computations on values of type record.
      The simplest members of a record are static.
      Static members are defined with the syntax:
      static member Name : TYPE = BODY
      
      A very useful static member is the zero vector, which returns us a vector with all components initialized to zero.
      Notice that this static member must return a vector with a generic unit of measure 'a ('a may be any valid unit of measure: m, kg, s, m / s, etc.).
      For this reason the fields of the returned record must have the same unit of measure of the vector, as per the above definition.
     
      We invoke a static member by writing, for example:
      let v = Vector2<m/s>.Zero

      where v : Vector2<m/s>      
      *)     
      static member Zero : Vector2<'a> = { X = 0.0<_>; Y = 0.0<_> }

      (*
      We may sum two vectors, as long as they have the same units of measure. To sum them, we build a new record where each field is the sum of the two records.
      We may also sum a vector and a scalar.

      Given two vectors v1 : Vector2<m> and v2 : Vector2<m> we sum them by writing v1 + v2. The result will have type Vector2<m>.
      *)
      static member ( + ) (v1:Vector2<'a>,v2:Vector2<'a>):Vector2<'a> = { X = v1.X + v2.X; Y = v1.Y + v2.Y }
      static member ( + ) (v:Vector2<'a>,k:float<'a>):Vector2<'a> = { X = v.X + k; Y = v.Y + k }
      static member ( + ) (k:float<'a>,v:Vector2<'a>):Vector2<'a> = v + k
      
      (*
      We may negate a vector v by writing -v; this operation is called ~-; in general, an operator prefixed with ~ is a unary operator, that is it takes just one parameter.
      Negating a vector with unit of measure 'a returns another vector with the same unit of measure:
      *)
      static member ( ~- ) (v:Vector2<'a>):Vector2<'a> = { X = -v.X; Y = -v.Y }
      
      (*
      We may subtract vectors and scalars:
      *)
      static member ( - ) (v1:Vector2<'a>,v2:Vector2<'a>):Vector2<'a> = v1 + (-v2)
      static member ( - ) (v:Vector2<'a>,k:float<'a>):Vector2<'a> = v + (-k)
      static member ( - ) (k:float<'a>,v:Vector2<'a>):Vector2<'a> = k + (-v)
 
      (*
      We may multiply vectors and scalars. Multiplying a vector with unit of measure 'a with another vector (or scalar) with another unit of measure 'b produces a vector with unit of measure <'a * 'b>.
      For example, if v1 : Vector2<m> and v2 : Vector2<kg> then v1 * v2 : Vector2<m * kg>:
      *)
      static member ( * ) (v1:Vector2<'a>,v2:Vector2<'b>):Vector2<'a * 'b> = { X = v1.X * v2.X; Y = v1.Y * v2.Y }
      static member ( * ) (v:Vector2<'a>,f:float<'b>):Vector2<'a * 'b> = { X = v.X * f; Y = v.Y * f }
      static member ( * ) (f:float<'b>,v:Vector2<'a>):Vector2<'b * 'a> = { X = f * v.X; Y = f * v.Y }
      (*
      We may divide a vector by a scalar. The resulting vector will have a unit of measure that is the ratio of the units of measure of the vector and the scalar.
      For example, if v : Vector2<m> and t : float<s> then v / s : Vector2<m/s>:
      *)
      static member ( / ) (v:Vector2<'a>,f:float<'b>):Vector2<'a / 'b> = v * (1.0 / f)

      (*
      We can define members that can be applied on a particular vector. These members are called instance members, because they require a valid instance of the type to be invoked.
      Instance members are defined with the syntax:
      member SELF.NAME : TYPE = BODY

      Programmers with an object-oriented background may be used to using the name "this" for SELF.
      A very useful member for our vector computes the length of a vector. Notice that the result is a floating point number with the same unit of measure of the original vector.
      To compute the length of a vector v, we simply write v.Length:
      *)
      member this.Length : float<'a> = sqrt((this.X * this.X + this.Y * this.Y))

      (*
      We use vector subtraction and the length member to define the static member that computes the distance between two vectors and the normalization of a vector:
      *)
      static member Distance(v1:Vector2<'a>,v2:Vector2<'a>) = (v1-v2).Length
      static member Normalize(v:Vector2<'a>):Vector2<1> = v / v.Length

      member this.Normalized = this / this.Length

      static member Dot(v1:Vector2<'a>,v2:Vector2<'a>) = v1.X * v2.X + v1.Y * v2.Y

module PoliceChase =
    open System
    open System.Threading
    open Coroutines
    open Math2

     (*
    We define a unit of measure for the ships integrity, called Life:
    *)
    [<Measure>]
    type Life

    (*
    A Ship is defined in terms of:
    - position, velocity and dry (empty) mass
    - fuel, max fuel capacity, engine thrust and speed of fuel burn when engine is firing
    - current forces the ship is subject to
    - integrity and maximum integrity of the ship
    - damage and range of the ship's weapons
    - current state of the AI
    *)
    type Ship =
      {
        mutable Position      : Vector2<m>
        mutable Velocity      : Vector2<m/s>
        DryMass               : float<kg>
        mutable Fuel          : float<kg>
        MaxFuel               : float<kg>
        Thrust                : float<N/s>
        FuelBurn              : float<kg/s>
        mutable Force         : Vector2<N>
        mutable Integrity     : float<Life>
        MaxIntegrity          : float<Life>
        Damage                : float<Life/s>
        WeaponsRange          : float<m>
        mutable AI            : Coroutine<Unit>
      }
      (*
      We also define a method that computes the current mass of the ship as the sum of the dry mass of the ship and the amount of fuel it contains:
      *)
      member this.Mass = this.DryMass + this.Fuel

    (*
    The police station only has contains its position:
    *)
    type Station =
      {
        Position      : Vector2<m>
      }

    (*
    The state of the simulation is comprised of:
    - the police station
    - the police ship
    - the pirate ship
    - the cargo ship
    *)
    type PoliceChase =
      {
        PoliceStation : Station
        Patrol        : Ship
        Pirate        : Ship
        Cargo         : Ship
      }

    (*
    The simulation uses two constants: the delta time of the simulation and the size of the playing field: 
    *)
    let dt = 180.0<s>
    let field_size = 3.8e7<m>

    (*
    An engine impulse on a ship in a certain direction checks to see if there is enough fuel left in the tank;
    if so, then it adds to the current forces of the ship the engine thrust and removes some fuel form the tank:
    *)
    let impulse (self:Ship) (dir:Vector2<1>) (engine_power:float) =
      if self.Fuel > self.FuelBurn * engine_power * dt then
        do self.Force <- self.Thrust * dir * engine_power * dt
        do self.Fuel <- self.Fuel - self.FuelBurn * engine_power * dt

    (*
    An attack AI of a ship against another ship keeps the ship in range of the target, since it:
    - suspends itself for one simulation step
    - if it is not in weapons range, then it pushes the engines towards the target and it waits for a second before activating the engines again (to avoid using too much fuel)
    The various engine impulses use the dot product between the current ship velocity and the desired velocity to determine if the ship is moving in the right direction,
    to decide if it is better to accelerate or to brake.
    *)
    let attack (self:Ship) (target:Ship) =
      co{
        do! yield_
        let dir = Vector2<_>.Normalize(target.Position - self.Position)
        let dist = (target.Position - self.Position).Length
        if dist > self.WeaponsRange * 0.8 then
          if self.Velocity.Length > 0.01<_> then
            let v_norm = self.Velocity.Normalized
            let dot = Vector2.Dot(dir,v_norm)
            if dot <= 0.0 then
              do impulse self (-self.Velocity.Normalized) 1.0
            elif dot < 0.5 then
              do impulse self (Vector2<_>.Normalize((-(self.Velocity.Normalized - dir * dot)))) 0.3
            else
              do impulse self dir 0.1
            do! wait 1.0
          else
            do impulse self dir 1.0
            do! wait 1.0
        return ()
      }

    (*
    A useful AI coroutine takes a ship to the police station, by using the engines to maneuver close to the station;
    when the ship is close enough to the station, then it waits still for 5 seconds and it gets refueled:
    *)
    let reach_station (self:Ship) (s:PoliceChase) =
      co{
        do! yield_
        let dir = Vector2<_>.Normalize(s.PoliceStation.Position - self.Position)
        if Vector2<_>.Distance(s.PoliceStation.Position, self.Position) <= field_size * 1.0e-1 then
          let zero_velocity =
            co{
              do! yield_
              return self.Velocity <- Vector2<_>.Zero
            }
          do! wait_doing (fun _ -> zero_velocity) 5.0
          do self.Integrity <- self.MaxIntegrity
          do self.Fuel <- self.MaxFuel
        elif self.Velocity.Length > 0.01<_> then
          let dot = Vector2<1>.Dot(self.Velocity.Normalized,dir)
          if dot <= 0.0 then
            do impulse self (-self.Velocity.Normalized) 1.0
          elif dot < 0.5 then
            do impulse self (Vector2<_>.Normalize((-(self.Velocity.Normalized - dir * dot)))) 0.3
          else
            do impulse self dir 0.2
          do! wait 1.0
        else
          do impulse self dir 1.0
          do! wait 1.0
        return ()
      }

    (*
    The police patrol AI continuously repeats the following choice:
    - if the ship is healthy and fueled, then it attacks the pirates
    - if the ship is not healthy and fueled, then it goes back to the station for assistance
    *)
    let patrol_ai (s:PoliceChase) =
      let self = s.Patrol
      let healthy_and_fueled =
        co{
          do! yield_
          return self.Integrity > self.MaxIntegrity * 0.4 && self.Fuel > self.MaxFuel * 0.4 && s.Pirate.Integrity > 0.0<_>
        }
      let need_docking =
        co{
          do! yield_
          let! h = healthy_and_fueled
          return not h
        }
      repeat_ ((healthy_and_fueled => attack self (s.Pirate)) .||>
               (need_docking       => reach_station self s))

    (*
    The pirate AI, similarly to the police AI, decides between two behaviors:
    - if the police is too close, it attacks it
    - if the police is not too close, it attacks the cargo ship
    *)
    let pirate_ai (s:PoliceChase) =
      let self = s.Pirate
      let patrol_near =
        co{
          do! yield_
          return Vector2<_>.Distance(self.Position,s.Patrol.Position) < s.Patrol.WeaponsRange
        }
      let patrol_far =
        co{
          let! n = patrol_near
          return not n
        }
      repeat_ ((patrol_near => (attack (s.Pirate) (s.Patrol))) .||>
               (patrol_far  => (attack (s.Pirate) (s.Cargo))))

    (*
    The cargo ship keeps going towards the police station:
    *)
    let cargo_ai (s:PoliceChase) =
      let self = s.Cargo
      co{
        do! yield_
        do! reach_station self s
      } |> repeat_

    (*
    The initial state positions the various ships and the police station; 
    the police station and the patrol ship start close by, the cargo frigate starts far from the station while the pirate ship starts even further:
    *)
    let s0() =
      let s =
        {
          PoliceStation = { Position = { X = field_size; Y = field_size } * 0.25 }
          Patrol        =
            {
              Position        = { X = field_size; Y = field_size } * 0.25
              Velocity        = Vector2<_>.Zero
              DryMass         = 4.5e4<_>
              Fuel            = 2.2e6<_>
              MaxFuel         = 2.2e6<_>
              FuelBurn        = 2.2e6<_> / (50.0 * 180.0)
              Thrust          = 5.0e6<_> / 180.0
              Force           = Vector2<_>.Zero
              Integrity       = 100.0<_>
              MaxIntegrity    = 100.0<_>
              Damage          = 1.0e-1<_> / 180.0
              WeaponsRange    = field_size * 0.1
              AI              = co{ return () }
            }
          Pirate        =
            {
              Position        = { X = field_size; Y = field_size } * 0.75
              Velocity        = Vector2<_>.Zero
              DryMass         = 3.0e4<_>
              Fuel            = 2.2e6<_>
              MaxFuel         = 2.2e6<_>
              FuelBurn        = 2.2e6<_> / (30.0 * 180.0)
              Thrust          = 5.0e5<_> / 180.0
              Force           = Vector2<_>.Zero
              Integrity       = 75.0<_>
              MaxIntegrity    = 75.0<_>
              Damage          = 2.0e-1<_> / 180.0
              WeaponsRange    = field_size * 0.15
              AI              = co{ return () }
            }
          Cargo        =
            {
              Position        = { X = field_size; Y = field_size  * 0.7 } * 0.7
              Velocity        = Vector2<_>.Zero
              DryMass         = 2.3e6<_>
              Fuel            = 3.5e8<_> * 0.3
              MaxFuel         = 3.5e8<_>
              FuelBurn        = 3.5e6<_> / 180.0
              Thrust          = 3.4e6<_> / 180.0
              Force           = Vector2<_>.Zero
              Integrity       = 300.0<_>
              MaxIntegrity    = 300.0<_>
              Damage          = 1.0e-3<_> / 180.0
              WeaponsRange    = field_size * 0.1
              AI              = co{ return () }
            }
        }
      (*
      Each ship starts with the appropriate AI:
      *)
      do s.Patrol.AI <- patrol_ai s
      do s.Pirate.AI <- pirate_ai s
      do s.Cargo.AI  <- cargo_ai s
      s

    (*
    A coroutine step simply checks:
    - if the coroutine is completed then it returns a fake coroutine that propagates the same result again
    - if the coroutine is suspended then it returns the suspension
    *)
    let co_step =
      function
      | Return x          -> co{ return x }
      | Yield k           -> k
      | ArrowYield k      -> k

    (*
    An update of a ship updates its position, its velocity and zeroes its current force; then it updates the ship's AI.
    Notice that the ship is updated in place, thanks to its mutability.
    This way coroutines may keep a reference to the current state, without having to continuously access the new state.
    This considerably eases state accesses, but it would make it harder to make the application concurrent.
    *)
    let ship_step (s:Ship) =
        do s.Position <- s.Position + s.Velocity * dt
        do s.Velocity <- s.Velocity + dt * s.Force / s.Mass
        do s.Force    <- Vector2<_>.Zero
        do s.AI       <- co_step (s.AI())

    (*
    A step of the entire simulation updates the various ships, and then adds the damage between the various weapons when they are in range:
    *)
    let simulation_step (s:PoliceChase) =
      do ship_step s.Patrol
      do ship_step s.Pirate
      do ship_step s.Cargo
      if Vector2<_>.Distance(s.Patrol.Position, s.Pirate.Position) < s.Patrol.WeaponsRange then
        do s.Pirate.Integrity <- s.Pirate.Integrity - s.Patrol.Damage * dt
      if Vector2<_>.Distance(s.Cargo.Position, s.Pirate.Position) < s.Cargo.WeaponsRange then
        do s.Pirate.Integrity <- s.Pirate.Integrity - s.Cargo.Damage * dt
      if Vector2<_>.Distance(s.Patrol.Position, s.Pirate.Position) < s.Pirate.WeaponsRange then
        do s.Patrol.Integrity <- s.Patrol.Integrity - s.Pirate.Damage * dt
      elif Vector2<_>.Distance(s.Cargo.Position, s.Pirate.Position) < s.Pirate.WeaponsRange then
        do s.Cargo.Integrity <- s.Cargo.Integrity - s.Pirate.Damage * dt

    (*
    The printing and simulation function are essentially the same we have seen in the previous chapters;
    the simulation ends when one of the ships is destroyed:
    *)
    let print(s:PoliceChase) =
      do Console.Clear()
      let set_cursor (v:Vector2<_>) =
        Console.SetCursorPosition((((v.X / field_size) * 79.0) |> int) - 1 |> max 0 |> min 79, ((v.Y / field_size) * 23.0) |> int |> max 0 |> min 23)
      let set_cursor_on_ship (s:Ship) = set_cursor (s.Position)
      let set_cursor_on_station (s:Station) = set_cursor (s.Position)
      do set_cursor_on_station (s.PoliceStation)
      do Console.Write("¤")
      do set_cursor_on_ship (s.Patrol)
      let ship_fuel (s:Ship) =
        (9.0 * s.Fuel / s.MaxFuel).ToString("#.")
      let ship_integrity (s:Ship) =
        (9.0 * s.Integrity / s.MaxIntegrity).ToString("#.")
      do Console.Write((ship_fuel s.Patrol) + "∆" + (ship_integrity s.Patrol))
      do set_cursor_on_ship (s.Pirate)
      do Console.Write((ship_fuel s.Pirate) + "†" + (ship_integrity s.Pirate))
      do set_cursor_on_ship (s.Cargo)
      do Console.Write((ship_fuel s.Cargo) + "•" + (ship_integrity s.Cargo))
      do Console.SetCursorPosition(0,0)
      do Thread.Sleep(10)

    let simulation() =
      let s = s0()
      let rec simulation() =
        do print s
        if s.Patrol.Integrity > 0.0<_> && s.Pirate.Integrity > 0.0<_> && s.Cargo.Integrity > 0.0<_> then
          do simulation (simulation_step s)
      do simulation()


    simulation()

//let isprime n =
//    match n with
//    | _ when n > 3 && (n % 2 = 0 || n % 3 = 0) -> false
//    | _ -> 
//        let maxdiv = int(System.Math.Sqrt(float n)) + 1
//        let rec f d i =
//            if d > maxdiv then
//                true
//            else
//                if (n % d = 0) then
//                    false
//                else
//                    f (d + i) (6 - i)
//        f 5 2
//let sequence = seq { for n in 1 .. 100 do if isprime n then yield n }


//module ConsoleUi =
//    
//    type UserAction<'a> =
//        | Continue of 'a
//        | Exit
//
//    let displayCurrentOptions options = 
//        options
//        |> List.iteri ( fun i move ->
//            printfn "%i) %A" i move )
//
//    let getInput actionIndex input = 
//        let input = input
//        Some input
//
//    let processInputIndex inputStr programState availableOptions processAction processInputAgain =
//        match Int32.TryParse inputStr with
//        | true,actionIndex ->
//            match getInput actionIndex availableOptions with
//            | Some action ->
//                let actionResult = processAction programState action
//                Continue actionResult
//            | None ->
//                printfn "No actions found for actionIndex %i" actionIndex
//                processInputAgain()
//        | false, _ ->
//            printfn "Please enter a valid int"
//            processInputAgain()
//             
//
//    let rec processInput programState availableOptions processAction =
//        let processInputAgain() =
//            processInput programState availableOptions processAction
//
//        let inputStr = Console.ReadLine()
//        if inputStr = "q" then
//            Exit
//        else
//            processInputIndex inputStr programState availableOptions processAction processInputAgain

            