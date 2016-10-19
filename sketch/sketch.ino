#include <SoftwareSerial.h>
#define readCount 300
#define microDelay 1
int interval = 50;
SoftwareSerial mySerial(10, 11);
int pin = 6;
int mode = 0;
int isStopped = 1;


unsigned long start;
struct read
{
  int time;
  int value;
};
read reads[readCount];
void setup()
{
  pinMode(pin, INPUT);
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
  pinMode(pin, OUTPUT);
  start = millis();
  digitalWrite(pin, HIGH);
  mode = 1;
}
void activateDischarge()
{
  pinMode(pin, OUTPUT);
  Serial.println(-21);
  mode = 0;
  /*for (int i = 0; i < 10; ++i)
  {
    int voltage = analogRead(A0);
    Serial.println(voltage);
    delay(interval);
  }*/
  start = millis();
  digitalWrite(pin, LOW);
}
void checkSerial()
{
  while (Serial.available() > 0)
  {
    int byte = Serial.read() - 48;
    mySerial.print("From serial:");
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
}
bool isIntervalOk()
{
  bool isOk = true;
  if (mode == 1)
  {
    int refereceVoltage = reads[count / 3].value;
    mySerial.println(refereceVoltage);
    if (refereceVoltage < 600)
    {
      interval += 10;
      isOk = false;
    }
    else if (refereceVoltage > 700)
    {
      interval -= 10;
      isOk = false;
    }
    if (interval < 1)
      interval = 1;
  }
  return isOk;
}
void loop()
{
  checkSerial();
  if (isStopped == 0)
  {
    int voltage = analogRead(A0);
    //    Serial.println(voltage);
    reads[count].time = millis() - start;
    reads[count].value = voltage;
    if (count == readCount)
    {
      pinMode(pin, INPUT);
      isStopped = 1;
      if (isIntervalOk() == true)
      {
        for (int i = 0; i < count; ++i)
        {
          Serial.print(reads[i].time);
          Serial.print("_");
          Serial.println(reads[i].value);
        }
        Serial.println("-end-");
      }
      else
      {
        count = -1;
        isStopped = 0;
        pinMode(pin, OUTPUT);
        digitalWrite(pin, LOW);
        while (analogRead(A0) > 50);
        pinMode(pin, INPUT);
        activateCharge();
      }
    }
    
  }
  count++;
#if microDelay==1
  delayMicroseconds(interval);
#else
  delay(interval);
#endif

}
