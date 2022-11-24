using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.Json;


    [Serializable]
   public class Step
   {
 
       public IDictionary<string, Carro>[] cars;
 
       public IDictionary<string, Semaforo>[] traffic_lights;

   }

