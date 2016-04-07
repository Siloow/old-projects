// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

//namespace lollygaggin
//module testing = 
//    open System
//    type Person = { Name : string; Surname : string; Age : int}
//
//    type Employee =
//        {
//            Person : Person
//            Job : string
//        }
//        member self.ChangeJob new_job = {self with Job = new_job}
//
//    type Department = 
//        {
//        Boss : Person
//        NumEmployees : int
//        Name : string
//        }
//        member this.Description = "Department "+this.Name+" is managed by " + this.Boss.Name
//
//    type Job =
//        {
//            Department : string
//            HoursPerWeek : int
//            SalaryPerWeek : int
//        }
//        member job.Sucks = job.SalaryPerWeek/job.HoursPerWeek < 4
//        static member CreateDefaultJob() = 
//            {
//                Department = "Accounting";
//                HoursPerWeek = 40;
//                SalaryPerWeek = 400
//            }
//
//    type Traveler = 
//        {
//            Person : Person
//            mutable Location : string
//            CurrentActivity : Ref<string>
//        }
//
//    let john_doe = { Name = "John"; Surname = "Doe"; Age = 37 }
//
//    let john_traveler = {Person = john_doe; Location = "Combodia"; CurrentActivity = ref "Fishing"}
//    do john_traveler.Location <- "California";
//    do john_traveler.CurrentActivity := "Surfing"
//
//    type Pair<'a, 'b> = { First : 'a; Second : 'b }
//
//    let my_pair : Pair<int, string> = 
//        { First = 10; Second = "hello" }
//
//    let my_pair' = 
//        { 
//            First = { First = 10; Second = 5.0 };
//            Second = { First = System.DateTime.Now; Second = "Hello" }
//        }
//
//    type Number<'a> =
//        {
//            Zero : 'a;
//            One : 'a;
//            Sum : 'a -> 'a -> 'a;
//            Mul : 'a -> 'a -> 'a;
//            Neg : 'a -> 'a }
//
//    let line num q m x =
//        let (+) = num.Sum
//        let (*) = num.Mul
//        q+m*x
//
//    let float_num = 
//        { Zero = 0.0; One = 1.0; Sum = (+);
//            Mul = (*); Neg = ( ~- ) }
//    let int_num =
//        { Zero = 0; One = 1; Sum = (+);
//            Mul = (*); Neg = ( ~- ) }
//
//    let float_line = line float_num
//    let int_line = line int_num
//
//    let line_3x2 = int_line 2 3
//    let line_4x2 = float_line 2.0 4.0
//
//    let complex_num num =
//        let (+) = num.Sum
//        let (*) = num.Mul
//        let ( ~- ) = num.Neg
//        {
//            Zero = (num.Zero, num.Zero)
//            One = (num.One, num.Zero)
//            Sum = fun (a,b) (c,d) -> (a+c, b+d)
//            Mul = fun (a,b) (c,d) -> (a*c+(-(b*d)), b*c+a*d)
//            Neg = fun (a,b) -> (-a, -b)
//        }
//
//    Console.ReadLine();


namespace chapter2
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


module SmallSimulation =  
    open System
    open System.Threading

    open lollygaggin.Math

        type Asteroid = 
        {
            Position : Vector2<m>
            Velocity : Vector2<m/s>
            Mass : float<kg>
            Name : string
        }

        let dt = 60.0<s>
