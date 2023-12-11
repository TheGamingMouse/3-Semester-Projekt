import datetime
import time
from socket import *
import spidev

spi = spidev.SpiDev()
spi.open(0,0)
spi.max_speed_hz = 1000000

date = datetime.datetime.now()
formDate = date.strftime("%d-%m-%Y %H:%M")

sName = 'localhost'
sPort = 12000
cSock = socket(AF_INET, SOCK_DGRAM)

def readChannel(channel):
  value = spi.xfer2([1, (8 + channel) << 4,0])
  data = ((value[1]&3) << 8) + value[2]
  return data

while True:
  v = (readChannel (0) / 1023.0) * 3.3
  dist = 16.2537 * v**4 - 129.893 * v**3 + 382.268 * v**2 - 512.611 * v + 301.439
  print('Distance: %.2f cm' %dist)
  time.sleep(0.05)

while True:
  if (dist < 200):
    if (dist <= 5):
      cSock.sendto('Dør Aktiv'.encode(), (sName,sPort))
      returnMsg, sAddr = cSock.recvfrom(2048)
      print(returnMsg.decode())
      cSock.Close()

    elif (dist < 150):
      cSock.sendto(formDate.encode(), (sName,sPort))
      returnMsg, sAddr = cSock.recvfrom(2048)
      print(returnMsg.decode())
      cSock.close()
      
  time.sleep(.05)
