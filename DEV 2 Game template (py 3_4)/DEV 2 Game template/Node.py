class Node:
    def __init__(self, value, tail):
        self.Value = value
        self.Tail = tail
        self.IsEmpty = False
class Empty:
    def __init__(self):
        self.IsEmpty = True

Empty = Empty()

def map(l, f):
    if (l.IsEmpty):
        return Empty
    else:
        return Node(f(l.Value), map(l.Tail, f))

def filter(l, f):
    if (l.IsEmpty):
        return Empty
    else:
        if f(l.Value):
            return Node(l.Value, filter(l.Tail, f))
        else:
            return filter(l.Tail, f)

def iter(l, f):
    if (l.IsEmpty):
        return
    else:
        f(l.Value)
        iter(l.Tail, f)

#class Empty:
#    def IsEmpty(self): return True
#    def __str__(self):
#        return "[]"
#    def __rlshift__(self, v):
#        return Node(v, self)
#    def Map(self, f):
#        return self
#    def Filter(self, f):
#        return self
#    def Fold(self, f, z):
#        return z

#Empty = Empty()


#class Node:
#    def IsEmpty(self): return False
#    def Value(self): return self.Value
#    def Tail(self): return self.Tail
#    def __init__(self, value, tail):
#        self.Value = value
#        self.Tail = tail
#    def Map(self, f):
#        return Node(f(self.Value), self.Tail.Map(f))
#    def Filter(self, f):
#        xs = self.Tail.Filter(f)
#        if f(self.Value):
#            return Node(self.Value, xs)
#        else:
#            return xs

#def iter(l, f):
#    if (l.IsEmpty()):
#        return
#    else:
#        f(l.Value)
#        iter(l.Tail, f)