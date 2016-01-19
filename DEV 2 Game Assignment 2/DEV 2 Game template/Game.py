import time
from threading import Thread
import os, pygame
import time
from Tile import *
from Node import *
from Boat import *
pygame.init()
size = width, height = 600, 600
white = 255, 255, 255
green = 50, 255, 100
screen = pygame.display.set_mode(size)
offset = 50
size = 10
entry_road, entry_rivers, bridges = build_scene(size, offset)

#faces to the right
boat_texture = pygame.image.load("Content/tanker.png").convert_alpha()

#faces to the right
car_texture = pygame.image.load("Content/car.png").convert_alpha()

class Boat:
    def __init__(self, tile, tex, cm):
        self.Tile = tile
        self.Texture = tex
        self.canRemove = False

    def Draw(self):
        _width = int(offset / 3)
        screen.blit(pygame.transform.scale(self.Texture, (_width, _width)),
                    (_width + self.Tile.Position.X * offset,
                     _width + self.Tile.Position.Y * offset))

    def canRemove(self):
        return self.Tile.Harbor == True

    def Update(self):
        kaas = random.randint(0, 3)
        if kaas == 0 and self.Tile.Right != None and self.Tile.Right.River:
            boatLoc = self.Tile.Right
        elif kaas == 1 and self.Tile.Left != None and self.Tile.Left.River:
            boatLoc = self.Tile.Left
        elif kaas == 2 and self.Tile.Up != None and self.Tile.Up.River:
            boatLoc = self.Tile.Up
        elif kaas == 3 and self.Tile.Down != None and self.Tile.Down.River:
            boatLoc = self.Tile.Down
        else:
            boatLoc = self.Tile

        return Boat(boatLoc, self.Texture, self.canRemove)

class Car:
    def __init__(self, tile, tex, cm):
        self.Tile = tile
        self.Texture = tex
        self.canRemove = cm

    def Draw(self):
        _width = int(offset / 3)
        screen.blit(pygame.transform.scale(self.Texture, (_width, _width)),
                    (_width + self.Tile.Position.X * offset,
                     _width + self.Tile.Position.Y * offset))

    def canRemove(self):
        return self.Tile.Park == True

    def Update(self):
        kaas = random.randint(0, 8)
        if kaas == 0 and self.Tile.Right != None and self.Tile.Right.Traverseable and self.Tile.Right.Harbor == False:
            carLoc = self.Tile.Right
        elif kaas == 1 and self.Tile.Left != None and self.Tile.Left.Traverseable and self.Tile.Left.Harbor == False:
            carLoc = self.Tile.Left
        elif kaas == 2 and self.Tile.Up != None and self.Tile.Up.Traverseable and self.Tile.Up.Harbor == False:
            carLoc = self.Tile.Up
        elif kaas == 3 and self.Tile.Down != None and self.Tile.Down.Traverseable and self.Tile.Down.Harbor == False:
            carLoc = self.Tile.Down
        else:
            carLoc = self.Tile

        return Car(carLoc, self.Texture, self.canRemove)


def Update(entities):


    filteredList = filter(map(entities, lambda x: x.Update()), lambda x: not x.canRemove())
    return filteredList

def Draw(entities):
    entityList = entities
    while not entityList.IsEmpty:
        entityList.Value.Draw()
        entityList = entityList.Tail

def Main():
  start = time.time()

  entities = Node(Car(entry_road.Value, boat_texture, False), Empty)

  while True:    
    pygame.event.wait()
    screen.fill(green)

    #here we draw the board, do not move
    _board = entry_road
    while not _board.IsEmpty:
      _board.Value.Draw(screen, False)
      _board = _board.Tail

    #here we draw the bridges, do not move
    _board = bridges
    while not _board.IsEmpty:
      _board.Value.Draw(screen, True)
      _board = _board.Tail

    
    entities = Update(entities)
    Draw(entities)


    pygame.display.flip()
    time.sleep(0.1)
    
Main()