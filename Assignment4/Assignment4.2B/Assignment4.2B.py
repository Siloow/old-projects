# import random to let the computer function randomly choose between rock, paper or scissors
import random

# function to let the computer randomly choose between rock, paper or scissors
computer_choice = random.choice(["Rock", "Paper", "Scissors", "Lizard", "Spock"])

person_choice = raw_input("Choose between Rock, Paper, Scissors, Lizard or Spock\n")

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
        elif person_choice == "Lizard":
            print "Rock crushes Lizard"
            print "You lose :("
        elif person_choice == "Spock":
            print "Spock vaporisez Rock"
            print "You win!"
        else:
            print "But you've probably misspelled Rock, Paper, Scissors, Lizard or Spock" 
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
        elif person_choice == "Lizard":
            print "Lizard eats Paper"
            print "You win!"
        elif person_choice == "Spock":
            print "Paper disaproves Spock"
            print "You lose :("
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
    elif computer_choice == "Lizard":
        print "Computer chose Lizard"
        if person_choice == "Rock":
            print "Rock crushes lizard"
            print "You win!"
        elif person_choice == "Paper": 
            print "Lizard eats paper"
            print "You lose :("
        elif person_choice == "Scissors":
            print "Scissors decapitates Lizard"
            print "You win!"
        elif person_choice == "Lizard":
            print "Lizard against lizard leads to nothing"
            print "Nobody wins :("
        elif person_choice == "Spock":
            print "Lizard poisons Spock"
            print "You lose :("
        else:
            print "But you've probably misspelled Rock, Paper or Scissors"
    else:
        print "You've probably misspelled Rock, Paper or Scissors"        

# calling the outcome function with the two variables inserted
outcome(person_choice, computer_choice)


        
