#class Node:
#    def __init__(self, value, tail):
#        self.Value = value
#        self.Tail = tail
#        self.IsEmpty = False

#class Empty:
#    def __init__(self):
#        self.IsEmpty = True
#Empty = Empty()

#l = Node(1, Node(2, Node(3, Node(4, Empty))))

#def printList(l):
#    if l.IsEmpty:
#        return
#    else:
#        print(l.Value)
#        printList(l.Tail)

#def removeOdd(l):
#    if l.IsEmpty:
#        return Empty
#    else:
#        if l.Value % 2 == 0:
#            return Node(l.Value, removeOdd(l.Tail))
#        else:
#            return removeOdd(l.Tail)

#def addList(l):
#    if l.IsEmpty:
#        return 0
#    else:
#        return l.Value + addList(l.Tail)

#def mulList(l):
#    if l.IsEmpty:
#        return 1
#    else:
#        return l.Value * mulList(l.Tail)

#def range(l, u):
#    if l > u:
#        return Empty
#    else:
#        return Node(l, range(l+1, u))

#l = 1
#u = 4
#printList(range(l, u))

#class Empty:
#    def IsEmpty (self):
#        return True
#    def Length (self):
#        return 0

#class Node:
#    def IsEmpty (self): return False
#    def __init__ (self , x, xs):
#        self.head = x
#        self.tail = xs
#    def Length (self):
#        return 1 + self.tail.Length()

#class Counter:
#    def __init__ (self):
#        self.cnt = 0
#    def Tick (self,n):
#        self.cnt = self.cnt + n
#    def __str__(self):
#        return "Ticked " + str(self.cnt) + " times."

#c = Counter()
#for i in range (0, 5):
#    c.Tick (i)
#    print (c)
class Empty:
  def IsEmpty(self): return True
  def __str__(self):
    return "[]"
  def __rlshift__(self, v):
    return Node(v, self)
  def Sum(self):
    return 0
  def Length(self):
    return 0
  def Map(self,f):
    return self
  def Filter(self,p):
    return self
  def Fold(self,f,z):
    return z
Empty = Empty()

class Node:
  def IsEmpty(self): return False
  def Head(self): return self.Head
  def Tail(self): return self.Tail
  def __init__(self, x, xs):
    self.head = x
    self.tail = xs
  def __rlshift__(self, v):
    return Node(v, self)
  def __str__(self):
    return str(self.head) + "<<" + str(self.tail)
  def Sum(self):
    return self.head + self.tail.Sum()
  def Length(self):
    return 1 + self.tail.Length()
  def Map(self,f):
    return Node(f(self.head), self.tail.Map(f))
  def Filter(self,p):
    xs = self.tail.Filter(p)
    if p(self.head):
      return Node(self.head, xs)
    else:
      return xs
  def Fold(self,f,z):
    return f(self.head, self.tail.Fold(f,z))

l = 1 << (2 << (3 << (4 << Empty)))
#l = Node(1, Node(2, Node(3, Node(4, Empty))))
print("length(" + str(l) + ") = " + str(l.Length()))
print("incr(" + str(l) + ") = " + str(l.Map(lambda x: x + 1)))
print("even(" + str(l) + ") = " + str(l.Filter(lambda x: x % 2 == 0)))
print("sum(" + str(l) + ") = " + str(l.Sum()))
print("mul(" + str(l) + ") = " + str(l.Fold(lambda x,y: x * y, 1)))