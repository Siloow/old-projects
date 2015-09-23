temparature = input("What temeparature (Celsius) do you want to be converted to Kelvin?\n")

def convert(temparature):
    kelvin = (temparature + 273.15)
    if temparature == -273.15:
        print int(kelvin)
    else:
        print kelvin
    return

convert(temparature)
