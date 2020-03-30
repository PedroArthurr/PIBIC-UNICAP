
/*  

Conexões   
[-----------------] 
|A0 - Branco      |
|GND - Cinza      |
|(5V)3.3V - Preto | (A recomendação do fabricante do sensor é 5V, mas 3.3v saiu melhor.)
|LED13 - OnBoard  |
[-----------------]

Comentários de desenvolvimento
[=============================================================================================================================================================]
|Valores próximos do 300 em 3.3v e próximos de 500 no 5V, obtive melhores leituras no pino 3.3V,os batimentos cardíacos ficaram mais legíveis e nítidos.      |
|Porém esse valor é variável dependendo da corrente de energia que está passando pelo Arduino/Sensor no momento, melhor testar antes de mudar o "int Limite". |
|O Unity lê a COMX e cria um arquivo .txt com o Output do Arduino                                                                                             |
[=============================================================================================================================================================]


*/


int PortaAnalogicaSinal = 0;       //A0 na placa
int LED13 = 13;                    //LED OnBoard

int ContagemBatimentos;            //<<Ainda não to usando isso>>

int Sinal;                         //Sinal dado pelo sensor
int Limite = 375;                  //Se chegar em nesse número conta como uma batida 

void setup() {
  pinMode(LED13,OUTPUT);        
   Serial.begin(9600); 

    

}
void loop() {

  Sinal = analogRead(PortaAnalogicaSinal);   // Leitura do sensor
                                             
   Serial.println(Sinal);                    // Manda pro Serial Plotter

   if(Sinal > Limite){     
     digitalWrite(LED13,HIGH);
   } else {
     digitalWrite(LED13,LOW);            
   }

delay(10);

}
