// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

(*
In this chapter we will see some basic notions about F# programs.
We will study how to write simple applications with recursive functions, loops and other control flow operators.
We will see how to manipulate values and print to the console.

F# files must begin with a "module" or "namespace" declaration.
Modules and namespaces are a handy way of defining a block where every name has a certain prefix.
For example, when we write

namespace X
  type T = { i : int; s : string }

in reality we have defined only a type with name X.T.

Modules are identical to namespaces, with the addition that they may also contain values, where namespaces may only contain type declarations.
*)
namespace Chapter1

  (*
  Modules and namespaces may also be nested, for all those cases when we need to organize the contents of a module.
  *)

  (*
  The first sample we see is a simple ball simulation.
  We simulate the motion of a ball of unit mass, which bounces on a floor.
  We wish to achieve two objectives: on one hand, we wish to correctly simulate the motion of the ball. On the other hand, we wish to visualize this motion by painting a simple ASCII-art picture on the command line.

  We start by discussing what we know about the solution, since no good has ever come from starting to write code without first reasoning a bit on the problem at hand:  
  - the ball is represented by its current position y and its current velocity v
  - the ball position changes according to the ball velocity, and the ball velocity changes according to gravity
  - when the ball reaches the floor its velocity is negated; the impact with the floor reduces the magnitude of the velocity a bit

  A pseudo-code solution could be the following:
  simulation-step (y,v) =
  | (y+dt*v,v+dt*g) when y > 0
  | (0,-v * 0.7)    otherwise

  simulation (y,v) =
  | simulation(simulation-step (y,v)) when v > 0.1
  | nothing                           otherwise

  We will now proceed with an implementation of the above pseudo-code. Notice that we will dive right into the code, instead of discussing at length all the details.
  The point is that you try and get an intuitive feeling for this code, and, if you can write it at a computer, also run it and modify it. Right after the presentation of the code we will go through all its aspects, do not worry!
  Unfortunately there is no silver bullet for learning a programming language, and experimenting is one of the best ways to become a great developer.
  *)
  module BallSimulation =
    (*
    To access a set of namespace or module declarations by their local name, rather than their full name, we use the "open" directive.
    This way rather than writing, for example, System.Console.Write we can write simply Console.Write.
    *)
    open System

    (*
    When we wish to use the same value in various places, we can "bind" that value to a name (the "let" keyword performs a so called "let-binding")..
    Giving names to values is fundamental, and it is even more important to give clear, expressive names that help the programmer remember what a value represents.
    In a sense, the names of a program are the first and foremost means of documentation of the program.
    *)
    let dt = 0.1
    let g = -9.81

    (*
    We can also give names to operations, or functions, with the same "let" syntax.
    A function takes as input one or more values (in this case the position and velocity of our ball) and computes some result.
    The last value found in a function is its result.
    *)
    let simulation_step (y,v) =
       (*
       A function may define some intermediate values that it needs to compute its final result, and even invoke additional functions.
       In this case we evaluate the results of computing a uniformly accelerated motion to our ball, without taking the floor into consideration.
       Notice that we may "let-bind" more values at the same time, separating them with a comma: 
let a = 1
let b = 2 
Is equivalent to 
let a,b = (1, 2)
       *)
       let y',v' = (y + v * dt, v + g * dt)
       (*
       Sometimes we may need a computation to be split in two, according to a certain condition.
       When the condition is true, the first computation is performed; when the condition is false, then the second computation is performed.
       This construct is called "if-then-else", and we use it because if the ball falls through the floor we put it back over the floor and we revert the velocity (reducing it by one third so that at each impact the ball will slow down a bit). When the ball is over the floor, we simply use the intermediate results since they are perfectly valid.
       *)
       if y' < 0.0 then
         (0.0,-v' * 0.7)
       else
         (y',v')

    (*
    F# is an imperative language.
    Being imperative means that we can give commands to the system, such as "print this" or "create a file" or "change this memory cell".
    When we write an imperative command, we precede it with the "do" keyword.
    The print_scene function is an imperative function which prints the current state of the application showing the ball inside a playing field.
    *)
    let print_scene (y,v) =
      do Console.Clear()
      let y,v = int y, int v
      (*
      Whenever we need to perform the same operation a given number of times, then we can use the for-loop.
      We write "for i = min to max do BODY", and the code contained in BODY will be executed for each value of i between min and max (min and max included).
      To iterate decreasing values from max to min we write "for i = max downto min do BODY".
      Here we iterate all the cells of our playing field, writing a frame of asterisks around the scene.
      When we are drawing the cell that contains the ball we will simply write a 'b'.
      *)
      for j = 10 downto 0 do
        for i = 0 to 30 do
          if (y+1) = j && i = 15 then
            Console.Write("b")
          elif j = 0 || i = 0 || j = 10 || i = 30 then
            Console.Write("*")
          else
            Console.Write(" ")
        Console.Write("\n")
      ignore(Console.ReadKey())

    (*
    The simulation is a function that takes as input a special value, called "unit" and written ().
    When we pass the () value to the simulation function, writing "simulation()", then we will compute the contents of the function and execute its commands.
    *)
    let simulation() =
      (*
      Inside a function we are allowed to define another function. We can even use the same name: the last use of a name hides all the previous ones, so there is no ambiguity.
      We define a recursive function which will print the current state of the ball, then it will perform a step of the simulation and finally it will call itself again (to perform the next step of the simulation) unless the velocity has become too little.
      *)
      let rec simulation (y,v) =
        do print_scene (y,v)
        let y',v' = simulation_step (y,v)
        if abs v' > 0.1 then
          do simulation (y',v')
      (*
      Up until now we have defined, but not yet run, the simulation function. We have to launch it with its initial state.
      *)
      do simulation (5.0,-2.0)
    
    simulation();   

  



