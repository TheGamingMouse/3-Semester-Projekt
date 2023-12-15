'''Bjørn'''
from picamera import PiCamera
from time import sleep
from socket import *

sName = 'localhost'
sPort = 12000
cSock = socket(AF_INET, SOCK_DGRAM)



camera = PiCamera()

camera.start_preview()
sleep(1)
camera.capture('/home/pi/Desktop/image.jpg')
camera.stop_preview()


'''
camera.start_preview()
for i in range(5):
    sleep(2)
    camera.capture('/home/pi/Desktop/image%s.jpg' % i)
camera.stop_preview()'''