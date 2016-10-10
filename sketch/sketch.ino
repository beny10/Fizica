int pin=6;
int dischargePin=9;
void setup()
{
  pinMode(pin,INPUT);
  Serial.begin(9600);
  pinMode(dischargePin,INPUT);
}
int count=0;
void loop()
{
  digitalWrite(pin,HIGH);
  int voltage=analogRead(A0);
  Serial.println(voltage);
  delay(1);
  count++;
  if(count==40)
  {
    count=0;
    pinMode(dischargePin,OUTPUT);
    digitalWrite(dischargePin,LOW);
    digitalWrite(pin,LOW);
    delay(200);
    Serial.println(-1);
    pinMode(dischargePin,INPUT);
  }
}
