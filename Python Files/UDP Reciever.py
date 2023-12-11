from socket import *
from threading import *
import requests

serverPort = 12000
sSock = socket(AF_INET, SOCK_DGRAM)
sSock.bind(('', serverPort))

URL = 'http://localhost:5165/api/SikkerhedsLogs'

def Post(values):
    response = requests.post(URL, json=values)
    data = response.json()
    return data, response

def HandleClient():
    while True:
        msg, addr = sSock.recvfrom(2048)
        sHatMsg = msg.decode()
        print(f'Message from SenseHat {addr}: "{sHatMsg}"')
        if ('' in sHatMsg):
            values = {
                "Id" : 1,
                "Tidspunkt" : sHatMsg
            }
            print(f'SikkerhedsLog "{msg}" has been created')
            sikkerhedsLogs, resp = Post(values)
            print(resp.status_code)
            print(resp.content)

        sMsg = 'Message recieved.'
        sSock.sendto(sMsg.encode(), addr)

print('The server is ready to receive')
while True:
    Thread(target=HandleClient).start()
