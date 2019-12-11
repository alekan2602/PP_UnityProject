//Notes
#define NOTE_G3  196
#define NOTE_C3  131
#define NOTE_C5  523
#define NOTE_D5  587
#define NOTE_E5  659
#define NOTE_F5  698

//Buttons pins
const int denyButtonPin = 8;
const int acceptButtonPin = 7;

//Leds RGB pins
const int rgb01_red = 11;
const int rgb01_green = 10;
const int rgb01_blue = 9;
const int rgb02_red = 6;
const int rgb02_green = 5;
const int rgb02_blue = 3;

//Sounds
int wrongMelody[] = {NOTE_G3, NOTE_C3};
int correctMelody[] = {NOTE_C5, NOTE_D5, NOTE_E5};
int lengthOfWrongMelody = sizeof(wrongMelody) / sizeof(wrongMelody[0]);
int lengthOfCorrectMelody = sizeof(correctMelody) / sizeof(correctMelody[0]);
int wrongMelodyNoteDurations[] = {
  4, 4
};
int correctMelodyNoteDurations[] = {
  8, 8, 8
};

int denyState = 0;
int acceptState = 0;
int selectionValue = 0;

bool canEnterDenyStateOnce = true;
bool canEnterAcceptStateOnce = true;
bool canEnterIdleStateOnce = true;

bool canEnterDenyOnce = true;
bool canEnterAcceptOnce = true;
bool canEnterIdleOnce = true;

int unityData = 0;

void setup() 
{
  Serial.begin(9600);
  pinMode(denyButtonPin, INPUT);
  pinMode(acceptButtonPin, INPUT);
  pinMode(rgb01_red, OUTPUT);
  pinMode(rgb01_green, OUTPUT);
  pinMode(rgb01_blue, OUTPUT);
  pinMode(rgb02_red, OUTPUT);
  pinMode(rgb02_green, OUTPUT);
  pinMode(rgb02_blue, OUTPUT);
}

void loop()
{
  denyState = digitalRead(denyButtonPin);
  acceptState = digitalRead(acceptButtonPin);

  if(denyState == HIGH && acceptState == LOW && canEnterDenyStateOnce == true)
  {  
    selectionValue = 2;
    
    canEnterDenyStateOnce = false;
    canEnterAcceptStateOnce = true;
    canEnterIdleStateOnce = true;
  }
  else if(acceptState == HIGH && denyState == LOW  && canEnterAcceptStateOnce == true)
  {
    selectionValue = 1;

    canEnterDenyStateOnce = true;
    canEnterAcceptStateOnce = false;
    canEnterIdleStateOnce = true;
  }
  else if(acceptState == LOW && denyState == LOW && canEnterIdleStateOnce == true)
  {
    selectionValue = 0;

    canEnterDenyStateOnce = true;
    canEnterAcceptStateOnce = true;
    canEnterIdleStateOnce = false;
  }
  
  if(selectionValue >= 0)
  {
    Serial.flush();
    Serial.println(selectionValue);
  }

  if(Serial.available())
  {
    unityData = Serial.read();
    
    if(unityData == 'A' && canEnterAcceptOnce == true) //ACCEPTED
    {
      SetRGBColor(rgb01_red, rgb01_green, rgb01_blue, 0, 255, 0);
      SetRGBColor(rgb02_red, rgb02_green, rgb02_blue, 0, 255, 0);

      for(int i = 0; i < lengthOfCorrectMelody; i++)
      {
        int noteDuration = 1000 / correctMelodyNoteDurations[i];
        tone(12, correctMelody[i], noteDuration);
        int pauseBetweenNotes = noteDuration * 1.30;
        delay(pauseBetweenNotes);
        noTone(12);
      }
      canEnterAcceptOnce = false;
      canEnterDenyOnce = true;
      canEnterIdleOnce = true;
    }
    else if(unityData == 'D'  && canEnterDenyOnce == true) //DENIED
    {     
      SetRGBColor(rgb01_red, rgb01_green, rgb01_blue, 255, 0, 0);
      SetRGBColor(rgb02_red, rgb02_green, rgb02_blue, 255, 0, 0);

      for(int i = 0; i < lengthOfWrongMelody; i++)
      {
        int noteDuration = 1000 / wrongMelodyNoteDurations[i];
        tone(12, wrongMelody[i], noteDuration);
        int pauseBetweenNotes = noteDuration * 1.30;
        delay(pauseBetweenNotes);
        noTone(12);
      }
      canEnterAcceptOnce = true;
      canEnterDenyOnce = false;
      canEnterIdleOnce = true;
    }
    else if(unityData == 'N' && canEnterIdleOnce == true) //IDLE
    {
      SetRGBColor(rgb01_red, rgb01_green, rgb01_blue, 0, 0, 255);
      SetRGBColor(rgb02_red, rgb02_green, rgb02_blue, 0, 0, 255); 

      canEnterAcceptOnce = true;
      canEnterDenyOnce = true;
      canEnterIdleOnce = false;
    }
  }
  
  delay(50);
}

void SetRGBColor(int redPin, int greenPin, int bluePin, int redValue, int greenValue, int blueValue)
{
  analogWrite(redPin, redValue);
  analogWrite(greenPin, greenValue);
  analogWrite(bluePin, blueValue);
}
