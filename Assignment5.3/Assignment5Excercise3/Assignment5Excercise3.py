# get user string
def input():
	print("What input do you want to encrypt?")
	return raw_input()

# get the amount to shift
def input_key():
	key = 0
	while True:
		print("How much do you want to shift? (1-26)")
		key = int(raw_input())
		if (key >= 1 and key <= 26):
			return key

# function to shift the user input
def encrypt_message(message, key):
	store = ""

	for i in message:
		if i.isalpha():
			num = ord(i)
			num += key
			if i.isupper():
				if num > ord('Z'):
					num -= 26
				elif num < ord('A'):
					num += 26
			elif i.islower():
				if num > ord('z'):
					num -= 26
				elif num < ord('a'):
					num += 26
			store += chr(num)
		else:
			store += i
	return store

message = input()
key = input_key()

print('Your encrypted message:')
print(encrypt_message(message, key))