# full square
a = int(raw_input("How big do you want the full square to be?\n"))

m, n = a, a

for i in range (m):
    for j in range (n):
        print '*',
    print 


# hollow square
a = int(raw_input("How big do you want the hollow square to be?\n"))

m, n = a, a

for i in range (m):
    for j in range(n):
        print '*' if i in [0, n-1] or j in [0, m-1] else ' ',
    print ' '