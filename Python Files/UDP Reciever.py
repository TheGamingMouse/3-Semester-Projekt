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
        rasbMsg = msg.decode()
        print(f'Message from SenseHat {addr}: "{rasbMsg}"')
        if ('Dør Aktiv' not in rasbMsg):
            values = {
                "Id" : 1,
                "Tidspunkt" : rasbMsg
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
