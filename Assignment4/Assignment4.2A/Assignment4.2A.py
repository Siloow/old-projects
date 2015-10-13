# import random to let the computer function randomly choose between rock, paper or scissors
import random

# function to let the computer randomly choose between rock, paper or scissors
rint = random.randint(1,5)
def computer(rint):
    if rint == 1:
        return "Rock"
    if rint == 2:
        return "Paper"
    if rint == 3:
        return "Scissors"
    else:
        return "incorrect"

computer_choice = computer(rint)


person_choice = raw_input("Choose between Rock, Paper or Scissors\n")

def person(person_choice):
    if person_choice == "Rock" | "Paper" | "Scissors" :
        return person_choice
    else:
        print "incorrect"


# function to determine the outcome of the game
def outcome(person_choice, computer_choice):
    if computer_choice == "Rock":
        print "Computer chose Rock"
        if person_choice == "Rock":
            print "Rock on Rock leads to nothing"
            print "Nobody won :("
        elif person_choice == "Paper": 
            print "Paper covers rock"
            print "You win!"
        elif person_choice == "Scissors":
            print "Rock crushes scissors"
            print "You lose :("
        else:
            print "But you've probably misspelled Rock, Paper or Scissors" 
    elif computer_choice == "Paper":
        print "Computer chose Paper"
        if person_choice == "Rock":
            print "Paper covers rock"
            print "You lose :("
        elif person_choice == "Paper": 
            print "Paper on paper leads to nothing"
            print "Nobody won"
        elif person_choice == "Scissors":
            print "Scissors cuts paper"
            print "You win!"
        else:
            print "But you've probably misspelled Rock, Paper or Scissors" 
    elif computer_choice == "Scissors":
        print "Computer chose Scissors"
        if person_choice == "Rock":
            print "Rock beats scissors"
            print "You win!"
        elif person_choice == "Paper": 
            print "Scissors cuts paper"
            print "You lose :("
        elif person_choice == "Scissors":
            print "Scissors on scissors leads to nothing"
            print "Nobody wins :("
        else:
            print "But you've probably misspelled Rock, Paper or Scissors" 
    else:
        print "You've probably misspelled Rock, Paper or Scissors"        

# calling the outcome function with the two variables inserted
outcome(person_choice, computer_choice)