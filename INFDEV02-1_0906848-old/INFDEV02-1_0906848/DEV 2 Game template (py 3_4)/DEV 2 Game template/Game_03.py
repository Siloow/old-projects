import time
from threading import Thread
import os, pygame
import time
from Tile import *
from Node import *
import random

pygame.init()
size = width, height = 600, 600
white = 255, 255, 255
green = 50, 255, 100
screen = pygame.display.set_mode(size)
offset = 30
board_size = 10
car_texture = pygame.image.load("Content\car.png").convert()
entry_tile = build_square_matrix(board_size, offset)

'''class Boat:
    def __init__ (self, tile):
        self.Tile = tile

    def Draw(self):
        screen.blit(pygame.transform.scale(boat_texture, (_width, _width)), 
                          (_width + self.Tile.Position.X * offset, 
                           _width + self.Tile.Position.Y * offset))

    def IsArrived(self):
        # TODO: adapt to boat
        return self.Tile.Haven == True

    def Update(self):
        # TODO: adapt to boat
        kaas = random.randint(0, 3) 
        if kaas == 0 and self.Tile.Right != None and self.Tile.Right.Traverseable:
            carLoc = self.Tile.Right            
            print ("a")
        elif kaas == 1 and self.Tile.Left != None and self.Tile.Left.Traverseable:
            carLoc = self.Tile.Left
            print ("b")
        elif kaas == 2 and self.Tile.Up != None and self.Tile.Up.Traverseable:
            carLoc = self.Tile.Up
            print ("c")
        elif kaas == 3 and self.Tile.Down != None and self.Tile.Down.Traverseable:
            carLoc = self.Tile.Down
            print ("d")
        else:
            carLoc = self.Tile
        return Boat(carLoc)

;'''


class Car:
    def __init__ (self, tile):
        self.Tile = tile

def Draw(self):
    screen.blit(pygame.transform.scale(car_texture, (_width, _width)), 
                        (_width + self.Tile.Position.X * offset, 
                        _width + self.Tile.Position.Y * offset))

def IsArrived(self):
    return self.Tile.Park == True

def Update(self):
    kaas = random.randint(0, 3) 
    if kaas == 0 and self.Tile.Right != None and self.Tile.Right.Traverseable:
        carLoc = self.Tile.Right            
        print ("a")
    elif kaas == 1 and self.Tile.Left != None and self.Tile.Left.Traverseable:
        carLoc = self.Tile.Left
        print ("b")
    elif kaas == 2 and self.Tile.Up != None and self.Tile.Up.Traverseable:
        carLoc = self.Tile.Up
        print ("c")
    elif kaas == 3 and self.Tile.Down != None and self.Tile.Down.Traverseable:
        carLoc = self.Tile.Down
        print ("d")
    else:
        carLoc = self.Tile

    return Car(carLoc)


def Update(entities):
    # TODO: this loop becomes map
    updatedList = Empty
    entityList = entities
    while not entityList.IsEmpty:
        updatedList = Node(entityList.Value.Update(), updatedList)
        entityList = entityList.Tail

    # TODO: this loop becomes filter
    entityList = updatedList
    while not entityList.IsEmpty:
        if not entityList.Value.IsArrived():
            updatedList = Node(entityList.Value, updatedList)
        entityList = entityList.Tail
      
    return updatedList

def Draw(entities):
    entityList = entities
    while not entityList.IsEmpty:
        _width = int(offset / 3)
        entityList.Value.Draw()
        entityList = entityList.Tail


def Main():
  start = time.time()

  counter = 0

  while counter < 3:
    car_list = Node(Car(entry_tile), car_list)
    counter+=1

  entities = car_list
  while True:
    pygame.event.wait()    
    screen.fill(green)  
    entry_tile.Reset()
    entry_tile.Draw(screen)

    entities = Update(entities)
    Draw(entities)

    pygame.display.flip()
    time.sleep(1)
    
Main(entities)