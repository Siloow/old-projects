user_input = raw_input("Type in your password\n")

def strongness(user_input):
    strenght = ['very weak','weak','medium','strong', 'super strong']
    conditions = 0

    # split the input in different k eys in a list to acces them easily
    input_seperated = user_input.split()

    # store the amount of seperate strings entered
    amount = len(input_seperated)
            
    # go through user_input to count each string in the list then return each value
    for val in input_seperated:
        lenght = len(val)     
         
    if len(user_input) >= 6:
        # check if the each key in the list has 6 or more characters.
        if all(lenght >= 3 for i in input_seperated):
            conditions += 1
        if user_input.lower() != user_input:
            conditions += 1
        if len([i for i in user_input if i.isdigit()]) > 0:
            conditions += 1
        if len([i for i in user_input if i.isalnum()]) > 0:
            conditions += 1
    else:
        conditions = 0

    if conditions == 0:
        print "Your password is " + strenght[0]
    elif conditions == 1:
        print "Your password is " + strenght[1]
    elif conditions == 2:
        print "Your password is " + strenght[2]
    elif conditions == 3:
        print "Your password is " + strenght[3]
    elif conditions == 4:
        print "Your password is " + strenght[4]
               
strongness(user_input)