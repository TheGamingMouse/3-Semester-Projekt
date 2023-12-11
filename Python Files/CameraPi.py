from picamera import PiCamera
from time import sleep

sName = 'localhost'
sPort = 12000
cSock = socket(AF_INET, SOCK_DGRAM)



camera = PiCamera()

camera.start_preview()
sleep(5)
camera.capture('/home/pi/Desktop/image.jpg')
camera.stop_preview()