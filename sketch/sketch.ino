int pin = 6;
int mode = 0;
void setup()
{
  pinMode(pin, OUTPUT);
  Serial.begin(9600);
}
int count = 0;
void loop()
{
  int voltage = analogRead(A0);
  Serial.println(voltage);
  if (count == 500)
  {
    count = 0;
    mode++;
    mode = mode % 2;
    if (mode == 0)
    {
      Serial.println(-1);
      int voltage = analogRead(A0);
      Serial.println(voltage);
      digitalWrite(pin, HIGH);
    }
    else
    {
      digitalWrite(pin, LOW);
    }
  }
  count++;
  delay(80);
}
