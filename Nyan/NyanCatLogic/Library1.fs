module NyanCat.GameState

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input

type Ship =
    {
        Position : Vector2
        Velocity : Vector2
    }

type GameState =
    {
        Ship : Ship
    }

let initialState() =
    {
        Ship = 
        {
            Position = Vector2(320.0f, 400.0f)
            Velocity = Vector2.Zero
        }
    }

type InputEvents = 
    {
        Movement : Vector2
    }

let updateShip (ks:KeyboardState) (ms:MouseState) (dt:float32) (ship:Ship) =
    let speed = 400.0f;
    let ship =
        if ks.IsKeyDown(Keys.Left) then
        { ship with Velocity = ship.Velocity - Vector2.UnitX * speed * dt}
        else
            ship
    let ship =
        if ks.IsKeyDown(Keys.Right) then
        {ship with Velocity = ship.Velocity + Vector2.UnitX * speed * dt}
        else
            ship
    { ship with Position = ship.Position + ship.Velocity * dt;
                Velocity = ship.Velocity * 0.9f }

let updateState (ks:KeyboardState) (ms: MouseState) (dt:float32) (gameState:GameState) =
    { gameState with Ship = updateShip ks ms dt gameState.Ship }

type Drawable =
    {
     Position : Vector2
     Image : string
    }

let drawState (gameState:GameState) : seq<Drawable> =
    { Drawable.Position = gameState.Ship.Position
      Drawable.Image = "image.png"  
     }