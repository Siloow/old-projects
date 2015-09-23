temparature = input("What temeparature do you want to be converted to celsius?\n")

def convert(temparature):
    fahrenfeit = (temparature - 32) / 1.800
    return float(format(fahrenfeit, '.2f'))

print convert(temparature)



