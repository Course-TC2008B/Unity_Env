using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
 public class DataManager : MonoBehaviour
{
//     
     [SerializeField]
     private ListSim[] _listSims;
     private GameObject[] _carros;
     private GameObject[] _semaforos;
//
//     // Start is called before the first frame update
    void Start() 
    {
         _carros = new GameObject[_listSims[0].step[0].car.Length];
         _semaforos = new GameObject[_listSims[0].step[0].semf.Length];

         PosicionarCarros();
    }
//
    private void PosicionarCarros() 
     {
         for (int ls = 0; ls < _listSims.Length; ls++)
         {
             for (int st = 0; st < _listSims[ls].step.Length; st++)
             {
                 for (int cr = 0; cr < _listSims[ls].step[st].car.Length; cr++)
                 {
                     _carros[cr] = CarPoolManager.Instance.ActivarObjeto(
                         new Vector3(
                         _listSims[ls].step[st].car[cr].position.x,
                         _listSims[ls].step[st].car[cr].position.y
                         )
                     );
                     // print(_car[cr].position);
                 }
                 // for (int sf = 0; sf <_listSims[ls].step[st].semf.Length; sf++)
                 // {
                 //     _semaforos[sf] = _listSims[ls].step[st].semf[sf].state
                 // }
             }
         }
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
//     public void EscucharRequestSinArgumentos() {
//
//         print("HUBO UN REQUEST MUY INTERESANTE!");
//     }
//
//     public void EscucharRequestConArgumentos(ListaCarros datos){
//         print("DATOS: " + datos);
//
//         // actualizar arreglo _carros de esta clase con 
//         // los carros que recibo de "datos"
//
//         // invocar PosicionarCarros()
//     }
 }
