class Empty:
    def __init__(self):
        self.IsEmpty = True

Empty = Empty()

class Node:
    def __init__(self, value, tail):
        self.IsEmpty = False
        self.Value = value
        self.Tail = tail

l = Empty

'''
cnt = int(input("How many elements"))
for i in range(0, cnt):
    v = int(input("Next element?"))
    l = Node(v, l)
    '''

# reverse print of the list



def sum(l):
    if l.IsEmpty:
        return 0
    else:
        return sum(l.Tail) + l.Value

print(sum(Node(1, Node(2, Node(3, Empty)))))

l = Node(1, Node(2, Node(3, Empty)))

print (l.Tail.Value)

'''
def addNum(step, l):
    if not(l.IsEmpty):
        return Node(l.Value + step, addNum(step, l.Tail))
    else:
        return Empty

def printList(l):
    if not(l.IsEmpty):
        print(l.Value)
        printList(l.Tail)
    else:
        print("empty")


l = Node(1, Node(2, Node(3, Empty)))

while not(l.IsEmpty):
    print (l.Value)
    l = l.Tail

printList(addNum(10,l))

def printList(l):
    if(l.IsEmpty):
        return Empty
    else:
        print(l.Value)
        printList(l.Tail)

def map(l, f):
    if(l.IsEmpty):
        return Empty
    else:
        return Node(f(l.Value), map(l.Tail, f))

printList(map(Node(1, Node(2, Node(3, Node(4, Empty)))), lambda x: x + 1))
#kaas



class Empty:
    def Length(self):
        return 0
    def __str__(self):
        return "[]"
    def __rlshift__(l, x):
        return Node(x, l)
    def map(self, f):
        return Empty()
    def filter(self, f):
        return Empty()


class Node:
    def __init__(self, x, xs):
        self.Value = x
        self.Tail = xs
    def Lenght(self):
        return 1 + self.Tail.Length()
    def __str__(self):
        return str(self.Value) + "<<" + str(self.Tail)
    def __rlshift__(l, x):
        return Node(x, l)
    def map(self, f):
        return Node(f(self.Value), self.Tail.map(f))
    def filter(self, f):


# l = 1 << (2 << (3 <<( 4 << Empty())))
# print(l)

l = 1 << (2 << (3 <<( 4 << Empty())))

bier = l.map(lambda x: x + 1).map(lambda x: x * 2)

print (bier)

'''