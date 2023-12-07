import datetime
from sense_hat import SenseHat
import time
from socket import *

date = datetime.datetime.now()
formDate = date.strftime("%d-%m-%Y %H:%M")

sName = 'localhost'
sPort = 12000
cSock = socket(AF_INET, SOCK_DGRAM)

s = SenseHat()
s.low_light = True

R = (255, 0, 0)
O = (0,0,0)

def alarm_logo():
    logo = [
    R, R, R, R, R, R, R, R,
    R, R, R, R, R, R, R, R,
    R, R, R, R, R, R, R, R,
    R, R, R, R, R, R, R, R,
    R, R, R, R, R, R, R, R,
    R, R, R, R, R, R, R, R,
    R, R, R, R, R, R, R, R,
    R, R, R, R, R, R, R, R,
    ]
    return logo

def secure_logo():
    logo = [
    O, O, O, O, O, O, O, O, 
    O, O, O, O, O, O, O, O, 
    O, O, O, O, O, O, O, O, 
    O, O, O, O, O, O, O, O, 
    O, O, O, O, O, O, O, O, 
    O, O, O, O, O, O, O, O, 
    O, O, O, O, O, O, O, O, 
    O, O, O, O, O, O, O, O, 
    ]
    return logo

while True:
    if ('Sensor bliver brudt'):
      s.set_pixels(alarm_logo())
      cSock.sendto(formDate.encode(), (sName,sPort))
      returnMsg, sAddr = cSock.recvfrom(2048)
      print(returnMsg.decode())
      cSock.close()
    else:
      s.set_pixels(secure_logo())
    time.sleep(.05)