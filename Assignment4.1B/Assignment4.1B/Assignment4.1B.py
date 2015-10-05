temparature = input("What temparature (Celsius) do you want to be converted to Kelvin?\n")   # store the user input in a variable

def convert(value):
    if value < -273.15:                                                                      # check if input is lower than -273.15
        print "You cannot go lower than 273.15 fahrenheit, please try again"                 # print error
        value = input("What temparature (Celsius) do you want to be converted to Kelvin?\n") # let user try again
        kelvin = (value + 273.15)                                                            # formula to convert celsius to kelvin
        if value == -273.15:                                                                 # check if value is exactly -273.15 to return 0 and not 0.0
            print int(kelvin)                                                                # print 0
        else:
            print kelvin # print a value with a decimal
    else:       
        kelvin = (value + 273.15)
        if value == -273.15: # check if value is exactly -273.15 to return 0 and not 0.0
            print int(kelvin) # print 0
        else:
            print kelvin # print a value with a decimal
             
convert(temparature) # run the function