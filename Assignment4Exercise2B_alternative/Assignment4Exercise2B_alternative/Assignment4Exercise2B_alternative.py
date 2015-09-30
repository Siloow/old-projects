# Import random to let the computer function randomly choose between rock, paper, scissors, lizard or spock
import random
 
# Function to convert the number to a string
def name_to_number(name):
    if name == "Rock":
        number = 0
    elif name == "Spock":
        number = 1
    elif name == "Paper":
        number = 2
    elif name == "Lizard":
        number = 3
    elif name == "Scissors":
        number = 4
    else:
        print "incorrect input"
    return number
    
# Function to convert the number to a name 
def number_to_name(number):
    if number == 0:
        name = "Rock"
    elif number == 1:
        name = "Spock"
    elif number == 2:
        name = "Paper"
    elif number == 3:
        name = "Lizard"
    elif number == 4:
        name = "Scissors"
    else:
        print "incorrect input"
    return name
 
def rpsls(player_choice):     
    # print a blank line to separate consecutive games
    print
    
    # print out the message for the player's choice
    print "Player chooses"+" "+player_choice
    
    # convert the player's choice to player_number using the function name_to_number()
    player_number = name_to_number(player_choice)
    
    # compute random guess for comp_number using random.randrange()
    comp_number = random.randrange(0, 5)
    
    # convert comp_number to comp_choice using the function number_to_name()
    comp_choice = number_to_name(comp_number)
    
    # print out the message for computer's choice
    print "Computer chooses"+" "+comp_choice
    
    # compute difference of comp_number and player_number modulo five
    gap = player_number - comp_number
    gap_remainder = gap % 5
    
       
    # use if/elif/else to determine winner, print winner message
    if gap_remainder == 0:
        print "Player and computer tie!"
    elif gap_remainder < 3:
        if comp_number == 0 and player_number == 1:
            print "Spock vaporizes Rock"
            print "Player wins!"
        elif comp_number == 1 and player_number == 3:
            print "Lizard poisons Spock"
            print "Player wins!"
        elif comp_number == 2 and player_number == 4:
            print "Scissors cuts paper"
            print "Player wins!"
        elif comp_number == 3 and player_number == 0:
            print "Rock crushes Lizard"
            print "Player wins!"
        elif comp_number == 4 and player_number == 1:
            print "Spock smashes Scissors"
            print "Player wins!"
        else:
            print "bir"
    elif gap_remainder >= 3:
        print "Computer wins!"
        
    
user_input = raw_input("Choose Rock, Paper, Scissors, Lizard or Spock\n")
rpsls(user_input)

user_input = raw_input("Choose Rock, Paper, Scissors, Lizard or Spock\n")
rpsls(user_input)

user_input = raw_input("Choose Rock, Paper, Scissors, Lizard or Spock\n")
rpsls(user_input)
