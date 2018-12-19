char veri=0;
 
void setup() {
 pinMode(4,OUTPUT);
 pinMode(3,OUTPUT);
 pinMode(2,OUTPUT);
 pinMode(10,OUTPUT);
 pinMode(9,OUTPUT);
 pinMode(8,OUTPUT);
 pinMode(7,OUTPUT);
 pinMode(6,OUTPUT);
 pinMode(5,OUTPUT);
 
 Serial.begin(9600);
}
 
void loop() {
  if (Serial.available()){    // Seri haberleşme varsa
      veri=Serial.read();     // Haberleşmeden gelen bilgiyi veri değişkenine ata
          if (veri=='4'){     // Gelen veri a karakteri ise
              digitalWrite(4,0);  // Ledi yak
          }
          else{               // Gelen veri a değilse
              digitalWrite(4,1);
          } 
          
          if (veri=='3'){     // Gelen veri a karakteri ise
              digitalWrite(3,0);  // Ledi yak
          }
          else{               // Gelen veri a değilse
              digitalWrite(3,1);  // Ledi söndür
              
          } 
          if (veri=='2'){     // Gelen veri a karakteri ise
              digitalWrite(2,0);  // Ledi yak
          }
          else{               // Gelen veri a değilse
              digitalWrite(2,1);
          } 
          
          if (veri=='a'){     // Gelen veri a karakteri ise
              digitalWrite(10,0);  // Ledi yak
          }
          else{               // Gelen veri a değilse
              digitalWrite(10,1);  // Ledi söndür
              
          } 
          if (veri=='9'){     // Gelen veri a karakteri ise
              digitalWrite(9,0);  // Ledi yak
          }
          else{               // Gelen veri a değilse
              digitalWrite(9,1);
          } 
          
          if (veri=='8'){     // Gelen veri a karakteri ise
              digitalWrite(8,0);  // Ledi yak
          }
          else{               // Gelen veri a değilse
              digitalWrite(8,1);  // Ledi söndür
              
          } 
          if (veri=='7'){     // Gelen veri a karakteri ise
              digitalWrite(7,0);  // Ledi yak
          }
          else{               // Gelen veri a değilse
              digitalWrite(7,1);
          } 
          
          if (veri=='6'){     // Gelen veri a karakteri ise
              digitalWrite(6,0);  // Ledi yak
          }
          else{               // Gelen veri a değilse
              digitalWrite(6,1);  // Ledi söndür
              
          } 
          if (veri=='5'){     // Gelen veri a karakteri ise
              digitalWrite(5,0);  // Ledi yak
          }
          else{               // Gelen veri a değilse
              digitalWrite(5,1);
          } 
          
          
  }
}
