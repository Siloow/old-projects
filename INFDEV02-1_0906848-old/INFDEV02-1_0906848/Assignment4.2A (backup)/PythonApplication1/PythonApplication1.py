#Import random to let the computer function randomly choose between rock, paper or scissors
import random

#Function to let the computer randomly choose between rock, paper or scissors
def computer():
    comp_choice = ["Rock", "Paper", "Scissors"]
    return (random.choice(comp_choice))

#Variable to store computer outcome
computer_choice = computer()

person_choice = raw_input("Choose between Rock, Paper or Scissors\n")

#Function to determine the outcome of the game
def outcome(person_choice, computer_choice):
    if computer_choice == "Rock":
        print "Computer chose Rock"
        if person_choice == "Rock":
            print "Rock on Rock leads to nothing"
            print "Nobody won :("
        elif person_choice == "Paper": 
            print "Paper beats rock"
            print "You win!"
        elif person_choice == "Scissors":
            print "Rock beats scissors"
            print "You lose :("
        else:
            print "But you've probably misspelled Rock, Paper or Scissors" 
    elif computer_choice == "Paper":
        print "Computer chose Paper"
        if person_choice == "Rock":
            print "Paper beats rock"
            print "You lose :("
        elif person_choice == "Paper": 
            print "Paper on paper leads to nothing"
            print "Nobody won"
        elif person_choice == "Scissors":
            print "Scissors beats paper"
        else:
            print "But you've probably misspelled Rock, Paper or Scissors" 
    elif computer_choice == "Scissors":
        print "Computer chose Scissors"
        if person_choice == "Rock":
            print "Rock beats scissors"
            print "You win!"
        elif person_choice == "Paper": 
            print "Scissors beats paper"
            print "You lose :("
        elif person_choice == "Scissors":
            print "Scissors on scissors leads to nothing"
            print "Nobody wins :("
        else:
            print "But you've probably misspelled Rock, Paper or Scissors" 
    else:
        print "You've probably misspelled Rock, Paper or Scissors"        

#Calling the outcome function with the two variables inserted

outcome(person_choice, computer_choice)