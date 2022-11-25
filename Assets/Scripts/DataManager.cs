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
    private  Carro[] _cars;
    //private   Semaforo[] _trafic_lights;
    private GameObject[] _carrosGO = null;

    //private GameObject[] _semaforosGO;
    // private JsonManager _jsonManager;

//
//     // Start is called before the first frame update
    void Start()
    {
        //_jsonManager = new JsonManager();
        //_listSims = _jsonManager._listSimJson
        print("DataManager Start _lenght: " + _cars.Length);
        //_carrosGO = new GameObject[_cars.Length];
        //_semaforosGO = new GameObject[_trafic_lights.Length];
        
        //for(int i = 0; i < _carrosGO.Length ; i++)
        //{
        //    _carrosGO[i] = CarPoolManager.Instance.ActivarObjeto(Vector2.zero);
       // }
        
        //print("Carros_Go: " + _carrosGO.Length);

        //PosicionarCarros();
    }

//
    private void PosicionarCarros()
    {
        print("Carros Go: " + _carrosGO);
        // Iniciar Cars
        if (_carrosGO == null || _carrosGO.Length == 0)
        {
            print("Iniciar carros");
            _carrosGO = new GameObject[_cars.Length];
            print("PosicionarCarros carrosGo Lenght: " + _carrosGO.Length);
            
            for(int i = 0; i < _carrosGO.Length ; i++)
            {
                _carrosGO[i] = CarPoolManager.Instance.ActivarObjeto(Vector2.zero);
            }
        }
            // Finalizar cars

            print("PosicionarCarros _lenght: " + _cars.Length);
          for (int cr = 0; cr < _cars.Length; cr++)
            {
                float _x = _cars[cr].position[0];
                float _y = _cars[cr].position[1];
                
                print("Car:" + cr + "  " + _x + " / " + _y);
                _carrosGO[cr].transform.position = new Vector3(
                    _x,
                        0,
                        _y
                    );
            
                // print(_car[cr].position);
            }

            // for (int cr = 0; cr < _step[st].traffic_lights.Length; cr++)
            // {
            //     string key = _step[st].traffic_lights[cr].Keys.First();
            //     _semaforosGO[cr] = CarPoolManagerSM.Instance.ActivarObjeto(
            //
            //         _step[st].traffic_lights[cr][key].state
            //
            //     );
            //
            // }
            
        // for(int i = 0; i < _carros.Length; i++)
        // {
        //     _carrosGO[i].transform.position = new Vector3(
        //         _carros[i].x,
        //         0,
        //         _carros[i].z
        //     );
        // }

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
    public void EscucharRequestSinArgumentos()
    {

        print("HUBO UN REQUEST MUY INTERESANTE!");
    }

    public void EscucharRequestConArgumentos(ListSim datos)
    {
        print("DATOS: " + datos);

        // actualizar arreglo _carros de esta clase con 
        // los carros que recibo de "datos"

        // invocar PosicionarCarros()
        StartCoroutine(ConsumirSteps(datos));
    }
    private IEnumerator ConsumirSteps(ListSim datos) {
        for(int i = 0; i < datos.steps.Length - 1; i++){
            print("Step: " + i);
            _cars = datos.steps[i].cars;
            print("ConsumirSteps cars: " + _cars.Length);
            //_carros = datos.steps[i].carros;
           // _trafic_lights = datos.steps[i].traffic_lights;
            PosicionarCarros();
            yield return new WaitForSeconds(0.5f);
        }
    }
}