import pyautogui
import socket

HOST = '127.0.0.1'
PORT = 12345

socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
socket.bind((HOST, PORT))

deg_90 = 108


def rotate_90_clockwise():
    pyautogui.moveRel(deg_90, 0, duration=0.5)


def rotate_90_counter_clockwise():
    pyautogui.moveRel(-deg_90, 0, duration=0.5)

socket.listen()
connection, addr = socket.accept()
print(f"{addr[0]} connected!")

with connection:
    while True:
        data = connection.recv(1024)
        if not data:
            break
        data_string = data.decode('ASCII')
        data_string_list = data_string.split(" ")
        if data_string_list[1].lstrip('-').isdigit():
            if data_string_list[0] == "rotate":
                counter = int(data_string_list[1]) < 0
                for i in range(abs(int(data_string_list[1]))):
                    if not counter:
                        rotate_90_clockwise()
                    else:
                        rotate_90_counter_clockwise()
