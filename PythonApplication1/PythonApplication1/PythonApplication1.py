value_input = input("Fill in the size of the cube: ")
value_output = 1
string_out = "x"
string_space = " "
print (value_input * "_")
while value_input > 0:
    print("|" + string_out * value_input + "/" + value_output * string_space + "|")
    value_input = value_input - 1
    value_output = value_output + 1
    
print (value_output * "_")