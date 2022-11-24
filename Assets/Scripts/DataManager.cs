using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//
 public class DataManager : MonoBehaviour
{
//     
     [SerializeField]
     private Step[] _step;
     private GameObject[] _carros;
     private GameObject[] _semaforos;
    // private JsonManager _jsonManager;
     
//
//     // Start is called before the first frame update
    void Start()
    {
        //_jsonManager = new JsonManager();
        //_listSims = _jsonManager._listSimJson;
         _carros = new GameObject[10];
         _semaforos = new GameObject[2];

         PosicionarCarros();
    }
//
    private void PosicionarCarros()
    {

        for (int st = 0; st < _step.Length; st++)
        {
            for (int cr = 0; cr < _step[st].cars.Length; cr++)
            {
                string key = _step[st].cars[cr].Keys.First();
                _carros[cr] = CarPoolManager.Instance.ActivarObjeto(
                    new Vector2(
                        _step[st].cars[cr][key].position[0],
                        _step[st].cars[cr][key].position[1]
                    )
                );
                // print(_car[cr].position);
            }

            for (int cr = 0; cr < _step[st].traffic_lights.Length; cr++)
            {
                string key = _step[st].traffic_lights[cr].Keys.First();
                _carros[cr] = CarPoolManagerSM.Instance.ActivarObjeto(

                    _step[st].traffic_lights[cr][key].state

                );

            }

        }

    }

     void Update()
    {
        

    }

    //
//     // Update is called once per frame
//     void Update()
//     {
//         if(Input.GetKeyDown(KeyCode.R)){
//
//             // simulando un update en datos
//             for(int i = 0; i < _carros.Length; i++){
//                 _carros[i].x = Random.Range(0f, 10f);
//                 _carros[i].y = Random.Range(0f, 10f);
//                 _carros[i].z = Random.Range(0f, 10f);
//             }
//
//             PosicionarCarros();
//         }
//     }
//
     public void EscucharRequestSinArgumentos() {

         print("HUBO UN REQUEST MUY INTERESANTE!");
     }

     public void EscucharRequestConArgumentos(ListSim datos){
         print("DATOS: " + datos);

         // actualizar arreglo _carros de esta clase con 
         // los carros que recibo de "datos"

         // invocar PosicionarCarros()
     }
 }
