from picamera import PiCamera
from time import sleep
import smtplib
from email.mime.multipart import MIMEMultipart
from email.mime.image import MIMEImage

camera = PiCamera()

sender_address = '3semdata@gmail.com'
sender_pass = '3sem2023'
receiver_address = bbkspil@gmail.com 


message = MIMEMultipart()
message['From'] = sender_address
message['To'] = receiver_address
message['Subject'] = 'A picture taken by Pi camera module. It has an attachment.'   

while True:
    
    pic = '/home/pi/Desktop/img.jpg'
    camera.start_preview()
    sleep(5)
    camera.capture(pic)
    camera.stop_preview()
    print('Image Captured')

    
    File = open(pic, 'rb')
    img = MIMEImage(File.read())
    File.close()
    message.attach(img)

  
    session = smtplib.SMTP('smtp.gmail.com', 587)
    session.starttls() 
    session.login(sender_address, sender_pass) 
    text = message.as_string()
    session.sendmail(sender_address, receiver_address, text)
    session.quit()
    print('Mail Sent')








    
import smtplib,ssl  
from picamera import PiCamera  
from time import sleep  
from email.mime.multipart import MIMEMultipart  
from email.mime.base import MIMEBase  
from email.mime.text import MIMEText  
from email.utils import formatdate  
from email import encoders  
   
camera = PiCamera()  
     
camera.start_preview()  
sleep(5)  
camera.capture('/home/pi/image.jpg')     
sleep(5)  
camera.stop_preview()  
def send_an_email():  
    toaddr = '3semdata@gmail.com'      
    me = '3semdata@gmail.com'          
    subject = "Nyt billede"              
      
    msg = MIMEMultipart()  
    msg['Subject'] = subject  
    msg['From'] = me  
    msg['To'] = toaddr  
    msg.preamble = "test "   
      
      
    part = MIMEBase('application', "octet-stream")  
    part.set_payload(open("/home/pi/image.jpg", "rb").read())  
    encoders.encode_base64(part)    
    part.add_header('Content-Disposition', 'attachment; filename="image.jpg"')  
    msg.attach(part)  
      
    try:  
       s = smtplib.SMTP('smtp.gmail.com', 587) 
       s.ehlo()  
       s.starttls()  
       s.ehlo()  
       s.login(user = '3semdata@gmail.com', password = '3sem2023')  
       s.sendmail(me, toaddr, msg.as_string())  
       s.quit()  
           
    except SMTPException as error:  
        print ("Error")                
      
send_an_email()  
