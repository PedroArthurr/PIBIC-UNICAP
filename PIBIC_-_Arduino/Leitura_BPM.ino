#define USE_ARDUINO_INTERRUPTS true    
#include <PulseSensorPlayground.h> 

const int Sinal = 0;           // A0 
const int LED13 = 13;          // Pisca LED onBoard pra cada batimento detectado
int Limite = 400;              // Mudar esse valor de acordo com cada pessoa que for usar. (a princípio checar valor no primeiro script) 
                               
PulseSensorPlayground pulseSensor;  // Creates an instance of the PulseSensorPlayground object called "pulseSensor"


void setup() {   

  Serial.begin(9600);          // 9600 no unity também !


  pulseSensor.analogInput(Sinal);   
  pulseSensor.blinkOnPulse(LED13);       //LED13 -> OnBoard
  pulseSensor.setThreshold(Limite);   

  
      if (pulseSensor.begin()) {
    Serial.println("Iniciando...");  
  }
}



void loop() {

 int myBPM = pulseSensor.getBeatsPerMinute();  // puxa função de pegar o BPM e declara como int myBPM
                                             
if (pulseSensor.sawStartOfBeat()) {            
  
 Serial.println(myBPM);                        // printa o valor no serial -> pro Unity
}

  delay(30);                

}

  
