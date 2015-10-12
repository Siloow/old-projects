import math
# full square
a = int(raw_input("How big do you want the full square to be?\n"))

m, n = a, a

for i in range (m): # in range of (1, m)
    for j in range (n): # in range of (1, n)
        print '*',
    print 


# hollow square
a = int(raw_input("How big do you want the hollow square to be?\n"))

m, n = a, a

for i in range (m):
    for j in range(n):
        print '*' if i in [0, n-1] or j in [0, m-1] else ' ', # only print * if i is in array [0, n-1] so that it only prints the outter lines
    print

# triangle
a = int(raw_input("How big do you want the triangle to be?\n"))

m, n = a, a

for i in range (m):
    for j in range (i + 1): # 1 gets added to i each iteration so that it print an extra *
        print '*',
    print 

# pyramid
a = int(raw_input("How big do you want the pyramid to be?\n"))

n = a

for i in range(1,n+1):
    for j in range(1,n-i+1): # in range of (1, (4, 3, 2, 1))
        print ' ',
    for k in range(1,2*i): # in range of (1, (2, 4, 6, 8, 10)) always an even number is printed 
        print '*',
    print 

# circle

a = int(raw_input("How big do you want the circle to be?\n"))

diameter = a
radius = a / 2  # return an int "because fuck floats" - Python

for i in range(diameter):
    for j in range(diameter):
        if math.sqrt((i - radius)**2 + (j - radius)**2) < radius:
            print "*",
        else:
            print " ",
    print


