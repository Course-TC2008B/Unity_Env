using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//
public class DataManager : MonoBehaviour {
    //     
    [SerializeField] private Carro[] _cars;

    //private   Semaforo[] _trafic_lights;
    private GameObject[] _carrosGO = null;

    //private GameObject[] _semaforosGO;

    //
    //     // Start is called before the first frame update

    //
    private void PosicionarCarros(){
        // Iniciar Cars
        if (_carrosGO == null || _carrosGO.Length == 0){
            print("Iniciar carros");
            _carrosGO = new GameObject[_cars.Length];
            print("PosicionarCarros carrosGo Lenght: " + _carrosGO.Length);
            for (int i = 0; i < _carrosGO.Length; i++){
                _carrosGO[i] = CarPoolManager.Instance.ActivarObjeto(Vector2.zero);
            }
        }
        // Finalizar cars

        print("Carros Go: " + _carrosGO.Length);
        print("PosicionarCarros _lenght: " + _cars.Length);
        for (int cr = 0; cr < _cars.Length; cr++){
            float _x = _cars[cr].position[0];
            float _y = _cars[cr].position[1];
            _carrosGO[cr].transform.position = new Vector3(_x,
                                                           0,
                                                           _y);
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

    public void EscucharRequestConArgumentos(ListSim datos){
        print("DATOS: " + datos);
        
        StartCoroutine(ConsumirSteps(datos));
    }

    private IEnumerator ConsumirSteps(ListSim datos){
        for (int i = 0; i < datos.steps.Length - 1; i++){
            print("Step: " + i);
            _cars = datos.steps[i].cars;
            print("ConsumirSteps cars: " + _cars.Length);
            //_carros = datos.steps[i].carros;
            // _trafic_lights = datos.steps[i].traffic_lights;
            PosicionarCarros();

            yield return new WaitForSeconds(0.25f);
        }
    }
}