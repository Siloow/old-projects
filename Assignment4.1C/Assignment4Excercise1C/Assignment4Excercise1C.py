number = input("What number do you want to be converted to an absolute value?\n")

def convert(number):
    absolute = abs(number)
    print "%s has an absolute value of %s" % (number, absolute)

convert(number)
