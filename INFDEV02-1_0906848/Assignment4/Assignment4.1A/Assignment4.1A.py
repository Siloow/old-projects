# store user input in a variable
temparature = input("What temparature from fahrenheit do you want to be converted to celsius?\n")


def convert(value):
    if value < -459.67: # check if value is below -459.67
        print "You cannot go lower than 459.67 fahrenheit, please try again" # error message
        value = input("What temparature from fahrenheit do you want to be converted to celsius?\n") # ask for the user input again
        fahrenheit = (value - 32) / 1.8000 # convert the temparature to celsius
        return float(format(fahrenheit, '.2f')) # return the value and convert it to a float with 2 decimals
    else:
        fahrenheit = (value - 32) / 1.8000 # convert the temparature to celsius
        return float(format(fahrenheit, '.2f')) # return the value and convert it to a float with 2 decimals

print convert(temparature) # call the function and print it out to the console