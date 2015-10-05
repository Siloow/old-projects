temparature = input("What temeparature from fahrenheit do you want to be converted to celsius?\n")

def convert(temparature):
    if temparature < -459.67:
        print "You cannot go lower than 459.67 fahrenheit, please try again"
        temparature = input("What temeparature from fahrenheit do you want to be converted to celsius?\n")
        fahrenheit = (temparature - 32) / 1.8000
        return float(format(fahrenheit, '.2f'))
    else:
        fahrenheit = (temparature - 32) / 1.8000
        return float(format(fahrenheit, '.2f'))

print convert(temparature)



