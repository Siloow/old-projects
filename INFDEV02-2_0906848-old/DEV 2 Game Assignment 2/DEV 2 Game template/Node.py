﻿import random
class Node:
  def __init__(self, value, tail):
    self.Tail = tail
    self.Value = value
    self.IsEmpty = False

class Empty: 
  def __init__(self):
    self.IsEmpty = True

Empty = Empty()

def map(l,f):
    if l.IsEmpty:
        return Empty
    else:
        return Node(f(l.Value), map(l.Tail, f))

def filter(l, p):
    if l.IsEmpty:
        return Empty
    else:
        if p(l.Value):
            return Node(l.Value, filter(l.Tail, p))
        else:
            return filter(l.Tail, p)

def fold(l, f, z):
    if l.IsEmpty:
        return z
    else:
        return f(l.Value, fold(l.Tail, f, z))

def iter(l, f):
    if (l.IsEmpty):
        return
    else:
        f(l.Value)
        iter(l.Tail, f)

def AUX_length(l, acc):
  if l.IsEmpty: return acc
  else: return AUX_length(l.Tail, acc + 1)

def length(l):
  return AUX_length(l, 0)

def select_one_random(l):
  _length = length(l)
  rnd_num = int(random.uniform(0, _length ))
  while(rnd_num > 0):
    l = l.Tail
    rnd_num -= 1
  return l.Value
  

