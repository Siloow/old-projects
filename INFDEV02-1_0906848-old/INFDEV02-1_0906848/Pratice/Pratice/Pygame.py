import pygame

class Car():
    def __init__(self, image, height, speed):
        self.Speed = speed
        self.Image = image
        self.Position = image.get_rect().move(0, height)
    def move(self):
        self.Position = self.Position.move(0, self.Speed)
        if self.Position.right > 600:
            self.Position.left = 0

screen = pygame.display.set_mode((640, 480))

