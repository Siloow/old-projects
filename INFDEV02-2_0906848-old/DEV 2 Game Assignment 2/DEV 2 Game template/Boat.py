import random
from Node import *
from Common import *
from Game import *
#BOAT CODE HERE

#class Boat:
#    def __init__(self, tile, tex, cm):
#        self.Tile = tile
#        self.Texture = tex
#        self.canRemove = False


#    def Draw(self):
#        _width = int(offset / 3)
#        screen.blit(pygame.transform.scale(self.Texture, (_width, _width)),
#                    (_width + self.Tile.Position.X * offset,
#                     _width + self.Tile.Position.Y * offset))

#    def canRemove(self):
#        return True    

#    def Update(self):
#        kaas = random.randint(0, 3)
#        if kaas == 0 and self.Tile.Right != None and self.Tile.Right.River:
#            boatLoc = self.Tile.Right
#        elif kaas == 1 and self.Tile.Left != None and self.Tile.Left.River:
#            boatLoc = self.Tile.Left
#        elif kaas == 2 and self.Tile.Up != None and self.Tile.Up.River:
#            boatLoc = self.Tile.Up
#        elif kaas == 3 and self.Tile.Down != None and self.Tile.Down.River:
#            boatLoc = self.Tile.Down
#        else:
#            boatLoc = self.Tile

#        return Boat(boatLoc, self.Texture, self.canRemove)

#NOTE: TO DRAW USE THE CODE AS IN ASSIGNMENT 1