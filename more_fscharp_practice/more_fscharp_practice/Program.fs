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


open System

Console.WriteLine("Fill in the amount of passegiers")


let checkPas x =
    let result =
        if x <= 0 then
            "Y u no fill in number above 4 :<"
        elif x <= 4 then
            "Voertuig A"
        elif x > 4 then
            "Voertuig B"
        elif x > 9 then
            "Invalide busje"
        else
            "lolwut"
    result





