from math import sqrt


def display_menu():
    print("Welcome to the Twitter Towers!")
    print("1. Add a rectangular tower ")
    print("2. Add a triangle tower")
    print("3. Exit")


def rectangular_tower():
    length = 0
    while length < 2:
        length = int(input("Enter tower's length (minimum 2): "))
    width = int(input("Enter tower's width: "))
    if width == length or abs(width - length) > 5:
        print("The tower area is: " + str(width * length))
    else:
        print("The tower perimeter is: " + str((width + length) * 2))


def triangle_tower():
    length = 0
    while length < 2:
        length = int(input("Enter tower's length (minimum 2): "))
    width = int(input("Enter tower's width: "))
    print("You Have 2 options:")
    print("1. Print tower's perimeter")
    print("2. Print tower")
    second_choice = input("Please enter your choice (1/2): ")
    if second_choice == '1':
        equal_side_length = sqrt((width / 2) ** 2 + length ** 2)
        print("The tower perimeter is: " + str(width + equal_side_length * 2))
    elif second_choice == '2':
        print_triangle_tower(length, width)
    else:
        print("Invalid choice. Please enter 1 or 2.")


def print_triangle_tower(length, width):
    if (width % 2) == 0 or width > (length * 2):
        print("Cannot print the tower:(")
        return
    lines_length = [3]
    n = 3
    # creates an array with the line's lengthes (without the first and last lines)
    while (n + 2) < width and len(lines_length) < (length - 2):
        n = n + 2
        lines_length += [n]
    # duplicates the line length as needed
    if len(lines_length) < (length - 2):
        count = (length - 2) // len(lines_length)
        first_line_addition = (length - 2) % len(lines_length)
        for i in range(0, len(lines_length)):
            for j in range(1, count):
                lines_length.insert(i*count+j, lines_length[count*i])
        # if needed, duplicates the first line length
        for j in range(0, first_line_addition):
            lines_length.insert(0, lines_length[0])
    # adds the first and last lines length
    lines_length.insert(0, 1)
    lines_length += [width]
    # prints each line
    for l in lines_length:
        print(" "*((width-l)//2)+"*"*l)


def main():
    choice = 0
    while choice != '3':
        display_menu()
        choice = input("Please enter your choice (1/2/3): ")

        if choice == '1':
            rectangular_tower()
        elif choice == '2':
            triangle_tower()
        elif choice == '3':
            print("Exiting the program.")
            break
        else:
            print("Invalid choice. Please enter 1, 2, or 3.")


if __name__ == "__main__":
    main()
