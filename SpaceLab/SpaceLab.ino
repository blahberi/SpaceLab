#include <Servo.h> 
bool activated = false;
Servo myservo;
void setup() 
{
  myservo.attach(9);
  Serial.begin(9600); 

}
void loop() {
  if (activated){
    return;
  }
  if (Serial.available() <= 0) {
    return;
  } 
  myservo.write(45);
  activated = true; 
}
