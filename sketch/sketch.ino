#include <SoftwareSerial.h>
#define readCount 300
SoftwareSerial mySerial(10, 11);
int pin = 6;
int mode = 0;
int isStopped = 1;
int interval = 8;

unsigned long start;
struct read
{
  int time;
  int value;
};
read reads[readCount];
void setup()
{
  pinMode(pin, OUTPUT);
  Serial.begin(9600);
  mySerial.begin(9600);
}
int count = 0;
void activateCharge()
{
  Serial.println(-20);
  /*for (int i = 0; i < 10; ++i)
  {
    int voltage = analogRead(A0);
    Serial.println(voltage);
    delay(interval);
  }*/
  digitalWrite(pin, HIGH);
}
void activateDischarge()
{
  Serial.println(-21);
  /*for (int i = 0; i < 10; ++i)
  {
    int voltage = analogRead(A0);
    Serial.println(voltage);
    delay(interval);
  }*/
  digitalWrite(pin, LOW);
}
void loop()
{
  while (Serial.available() > 0)
  {
    start = millis();
    int byte = Serial.read() - 48;
    mySerial.println(byte);
    if (byte == 0)
    {
      isStopped = 0;
      count = 0;
      activateDischarge();
    }
    else if (byte == 1)
    {
      isStopped = 0;
      count = 0;
      activateCharge();
    }
    else if (byte == 2)
    {
      isStopped = 0;
    }
    else if (byte == 3)
    {
      isStopped = 1;
    }
  }
  if (isStopped == 0)
  {
    int voltage = analogRead(A0);
    //    Serial.println(voltage);
    reads[count].time = millis() - start;
    reads[count].value = voltage;
    if (count == readCount)
    {
      isStopped = 1;
      for (int i = 0; i < count; ++i)
      {
        Serial.print(reads[i].time);
        Serial.print("_");
        Serial.println(reads[i].value);
      }
      Serial.println("-end-");
    }
    count++;
    delay(interval);
  }
}
