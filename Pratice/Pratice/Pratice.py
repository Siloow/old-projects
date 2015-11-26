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
cnt = int(input("How many elements"))
for i in range(0, cnt):
    v = int(input("Next element?"))
    l = Node(v, l)


# reverse print of the list
x = l
while not(x.IsEmpty):
    print (x.Value)
    x = x.Tail

