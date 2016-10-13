int pin = 6;
int mode = 0;
int isStopped = 0;
int interval=80;
int reads=800;
void setup()
{
  pinMode(pin, OUTPUT);
  Serial.begin(9600);
}
int count = 0;
void activateCharge()
{
  Serial.println(-20);
  for (int i = 0; i < 10; ++i)
  {
    int voltage = analogRead(A0);
    Serial.println(voltage);
    delay(interval);
  }
  int voltage = analogRead(A0);
  Serial.println(voltage);
  digitalWrite(pin, HIGH);
}
void activateDischarge()
{
  Serial.println(-21);
  for (int i = 0; i < 10; ++i)
  {
    int voltage = analogRead(A0);
    Serial.println(voltage);
    delay(interval);
  }
  digitalWrite(pin, LOW);
}
void loop()
{
  while (Serial.available() > 0)
  {
    int byte = Serial.read();
    if (byte == 1)
    {
      isStopped=0;
      count = 0;
      activateCharge();
    }
    else
    {
      isStopped=1;
    }
  }
  if (isStopped == 0)
  {
    int voltage = analogRead(A0);
    Serial.println(voltage);
    if (count == reads)
    {
      count = 0;
      mode++;
      mode = mode % 2;
      if (mode == 0)
      {
        activateCharge();
      }
      else
      {
        activateDischarge();
      }
    }
    count++;
    delay(interval);
  }
}
