
//Relacionado con el serialEvent
String inputString = "";
bool stringComplete = false;


float distance = 50;        //Distancia_Actual
float distancia_Des = 50;   //Distancia_Deseada 
float ang = 0;              //Angulo

float error = 0;            //Error
float errorAcum = 0;
float errorAnt = 0;
  


float kp = 1;//1              //Constante proporcional
float ki = 0.05;//0.05
float kd = 0.01;//0.01

float tiempo_Ant = 0;       //Tiempo Anterior
float tiempo_Act = 0;       //Tiempo Actual
float h = 50;//200          //Tiempo de "delay"

int longitud_Base = 30;     //Distancia maxima que puede alcanzar la esfera.

//*******************SETUP*************************//
void setup() { 
  Serial.begin(9600);
  inputString.reserve(200);
}

//*******************LOOP*************************//
void loop() {

  tiempo_Act = millis();

  if(tiempo_Act-tiempo_Ant > h)
  {
    tiempo_Ant = tiempo_Act;
    
    if(stringComplete)
    {         
      distance= inputString.toFloat();
      
      //Condicional que evita valores superiores a los que deberia.
      if(distance < (longitud_Base +1))
      {
        distance = MapFloat(distance,0,longitud_Base,0,100);   
        }
             
      inputString = "";
      stringComplete = false;
    }
    else
    {  
      SendData();     
     } 
   }
}

void SendData()
{ 

  error = distancia_Des - distance;     
  errorAcum += error*(h/1000);
 
  ang = kp * error + ki * errorAcum + kd * ((error - errorAnt) / (h/1000));
  
  ang = MapFloat(ang,-50,50,-15,15);
  ang = constrain(ang,-15,15);

  Serial.print(ang);
  Serial.print('a');
  Serial.println(distance);
  errorAnt = error;
}

void serialEvent(){
  while(Serial.available()){
    char inChar = (char)Serial.read();
    if(inChar == '\n'){
      stringComplete = true;
      }
      else{
        inputString += inChar;      
        }
    }  
  }

float MapFloat(long x,long in_min,long in_max,long out_min,long out_max)
{
  return(float)(x - in_min) * (out_max - out_min) / (float) (in_max - in_min) + out_min;
}
