temparature = input("What temparature (Celsius) do you want to be converted to Kelvin?\n")

def convert(temparature):
    if temparature < -273.15:
        print "You cannot go lower than 273.15 fahrenheit, please try again"
        temparature = input("What temparature (Celsius) do you want to be converted to Kelvin?\n")
        kelvin = (temparature + 273.15)
        if temparature == -273.15:
            print int(kelvin)
        else:
            print kelvin
    else:       
        kelvin = (temparature + 273.15)
        if temparature == -273.15:
            print int(kelvin)
        else:
            print kelvin

convert(temparature)