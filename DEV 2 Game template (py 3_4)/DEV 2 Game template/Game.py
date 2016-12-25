import time
from threading import Thread
import os, pygame
import time
from Tile import *
from Node import *

pygame.init()
size = width, height = 600, 600
white = 255, 255, 255
green = 50, 255, 100
screen = pygame.display.set_mode(size)
offset = 60
board_size = 10
car_texture = pygame.image.load("Content\car.png").convert()
entry_tile = build_square_matrix(board_size, offset)


class Car:
    def __init__(self,  tile):
        self.Tile = tile

    def Draw(self):
        _width = int(offset / 3)
        screen.blit(pygame.transform.scale(car_texture, (_width, _width)), 
                        (_width + self.Tile.Position.X * offset, 
                        _width + self.Tile.Position.Y * offset))

    def IsArrived(self):
        return self.Tile.Park == True

    def Update(self):
        kaas = random.randint(0,8)
        if kaas == 0 and self.Tile.Right != None and self.Tile.Right.Traverseable:
            carLoc = self.Tile.Right
            print ("A")
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

    #updatedList = Empty
    #entityList = entities
    #while not entityList.IsEmpty:
    #    updatedList = Node(entityList.Value.Update(), updatedList)
    #    entityList = entityList.Tail

    #filteredList = Empty
    #while not updatedList.IsEmpty:
    #    if not updatedList.Value.IsArriverd():
    #        filteredList = Node(updatedList.Value, filteredList)
    #    updatedList = updatedList.Tail

    #return filteredList

    updatedList = Empty
    entityList = entities

    filteredList = filter(map(entityList, lambda x : x.Update()), lambda x: not x.IsArrived())

    return filteredList


def Draw(entities):
    iter(entities, lambda x: x.Draw())

def Main():
  start = time.time()
  entities = Node(Car(entry_tile), Empty)

  while True:
    pygame.event.wait()    
    screen.fill(green)  
    entry_tile.Reset()
    entry_tile.Draw(screen)

    entities = Update(entities)
    Draw(entities)

    pygame.display.flip()
    time.sleep(0.2)
    
Main()