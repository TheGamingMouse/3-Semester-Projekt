# import cv2

# # Create a video capture object and allocate camera ID 0 (your webcam)
# cap = cv2.VideoCapture(0)

# # Capture a photo and store it in the ‘photo’ variable
# status, photo = cap.read()

# # Release the camera after capturing the photo
# cap.release()

# # Display the captured photo in a window titled “My Photo”
# cv2.imshow("My Photo", photo)

# # Wait for 2.5 seconds before closing the window
# cv2.waitKey(2500)

# # Close all OpenCV windows
# cv2.destroyAllWindows()

import cv2 
import smtplib
from email.mime.multipart import MIMEMultipart
from email.mime.image import MIMEImage
from time import sleep


key = cv2. waitKey(1)
webcam = cv2.VideoCapture(0)
while True:
    try:
        check, frame = webcam.read()
        print(check) 
        print(frame) 
        cv2.imshow("Capturing", frame)
        key = cv2.waitKey(1)
        if key == ord('s'): 
            cv2.imwrite(filename='saved_img.jpg', img=frame)
            webcam.release()
            img_new = cv2.imread('saved_img.jpg', cv2.IMREAD_GRAYSCALE)
            img_new = cv2.imshow("Captured Image", img_new)
            cv2.waitKey(1650)
            cv2.destroyAllWindows()
            print("Processing image...")
            img_ = cv2.imread('saved_img.jpg', cv2.IMREAD_ANYCOLOR)
            print("Image saved!")
        
            sender_address = '3semdata@gmail.com' 
            sender_pass = '3sem2023' 
            receiver_address = 'bbkspil@gmail.com' 

            message = MIMEMultipart()
            message['From'] = sender_address
            message['To'] = receiver_address
            message['Subject'] = 'A picture taken by computer webcam. It has an attachment.'  

            pic='saved_img.jpg'
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




            break
        elif key == ord('q'):
            print("Turning off camera.")
            webcam.release()
            print("Camera off.")
            print("Program ended.")
            cv2.destroyAllWindows()
            break
        
    except(KeyboardInterrupt):
        print("Turning off camera.")
        webcam.release()
        print("Camera off.")
        print("Program ended.")
        cv2.destroyAllWindows()
        break




