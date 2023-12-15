from socket import *
from threading import *
import requests
import datetime

sPort = 12000
sSock = socket(AF_INET, SOCK_DGRAM)
sSock.bind(('', sPort))

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
        if ('Person Entered' in rasbMsg):
            date = datetime.datetime.now()
            formDate = date.strftime("%d-%m-%Y %H:%M")
            print(formDate)
            values = {
                "Id" : 1,
                "Tidspunkt" : formDate
            }
            print(values)
            sikkerhedsLogs, resp = Post(values)
            print(resp.status_code)
            print(resp.content)
            print(f'SikkerhedsLog "{sikkerhedsLogs}" has been created')

print('The server is ready to receive')
while True:
    Thread(target=HandleClient).start()
