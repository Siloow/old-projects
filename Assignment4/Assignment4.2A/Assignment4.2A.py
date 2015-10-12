# import random to let the computer function randomly choose between rock, paper or scissors
import random
 
person = raw_input("Choose between Rock, Paper or Scissors")

print person[2]
'''# Function to convert the number to a string
def name_to_number(name):
    if name == "Rock":
        number = 0
    elif name == "Paper":
        number = 1
    elif name == "Scissors":
        number = 2
    else:
        return "incorrect input"
    return number
    
# Function to convert the number to a name 
def number_to_name(number):
    if number == 0:
        name = "Rock"
    elif number == 1:
        name = "Paper"
    elif number == 2:
        name = "Scissors"
    else:
        return "incorrect input"
    return name
 
def rpsls(player_choice):     
    # print a blank line to separate consecutive games
    print
    
    # print out the message for the player's choice
    print "Player chooses"+" "+player_choice
    
    # convert the player's choice to player_number using the function name_to_number()
    player_number = name_to_number(player_choice)
    
    # compute random guess for comp_number using random.randrange()
    comp_number = random.randrange(0, 3)
    
    # convert comp_number to comp_choice using the function number_to_name()
    comp_choice = number_to_name(comp_number)
    
    # print out the message for computer's choice
    print "Computer chooses" + " " +comp_choice
    
    # compute difference of comp_number and player_number modulo five
    gap = player_number - comp_number
    gap_remainder = gap % 3
    
       
    # use if/elif/else to determine winner, print winner message
    if gap_remainder == 0:
        print "Player and computer tie!"
    elif gap_remainder < 1:
        print "You win!"
    elif gap_remainder >= 1:
        print "Computer wins!"
        
    
user_input = raw_input("Choose Rock, Paper or Scissors\n")
rpsls(user_input)

user_input = raw_input("Choose Rock, Paper or Scissors\n")
rpsls(user_input)

user_input = raw_input("Choose Rock, Paper or Scissors\n")
rpsls(user_input)'''