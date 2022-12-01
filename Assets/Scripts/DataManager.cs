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
    private Carro[] _cars;

    [SerializeField]
    private Semaforo[] _trafic_lights;

    [SerializeField]
    private float _escalaTiempo = 1;

    private GameObject[] _carrosGO = null;

    //private Light[] _semaforosGO = null;
    private Vector3[] _direcciones;

    private void Update()
    {
        // actualizar posición basado en intervalos regulares
        // RECUERDA QUE MOVEMOS LOS GAME OBJECTS
        if (_direcciones != null && _direcciones.Length > 0)
        {
            for (int i = 0; i < _carrosGO.Length; i++)
            {
                // reorientar
                _carrosGO[i].transform.forward = _direcciones[i].normalized;

                // aplicar desplazamiento
                _carrosGO[i]
                    .transform
                    .Translate(_direcciones[i] * Time.deltaTime * _escalaTiempo,
                    Space.World);
            }
        }
    }

    private void PosicionarCarros()
    {
        // Iniciar Cars
        if (_carrosGO == null || _carrosGO.Length == 0)
        {
            print("Iniciar carros");
            _carrosGO = new GameObject[_cars.Length];
            print("PosicionarCarros carrosGo Lenght: " + _carrosGO.Length);
            for (int i = 0; i < _carrosGO.Length; i++)
            {
                _carrosGO[i] =
                    CarPoolManager.Instance.ActivarObjeto(Vector2.zero);
            }
        }

        // Finalizar cars
        print("Carros Go: " + _carrosGO.Length);
        print("PosicionarCarros _lenght: " + _cars.Length);
        for (int cr = 0; cr < _cars.Length; cr++)
        {
            CarProperties carComponent =
                _carrosGO[cr].GetComponent<CarProperties>();
            float _x = _cars[cr].position[0];
            float _z = _cars[cr].position[1];
            _carrosGO[cr].transform.position = new Vector3(_x, 0, _z);
        }
    }

    public void EscucharRequestConArgumentos(ListSim datos)
    {
        print("DATOS: " + datos);
        StartCoroutine(ConsumirSteps(datos));
    }

    private IEnumerator ConsumirSteps(ListSim datos)
    {
        float _difXAnt = 0.0f;
        float _difZAnt = 0.0f;
        for (int i = 0; i < datos.steps.Length - 1; i++)
        {
            print("Step: " + i);
            _cars = datos.steps[i].cars;
            _direcciones = new Vector3[_cars.Length];
            _trafic_lights = datos.steps[i].traffic_lights;
            print("ConsumirSteps cars: " + _cars.Length);

            // print("ConsumirSteps traffic lights: " + _trafic_lights.Lenght);
            PosicionarCarros();
            TLManager.Instance.ActualizarEstados (_trafic_lights);
            for (int j = 0; j < _cars.Length; j++)
            {
                // en cada paso calcular vector dirección para cada carro
                if (i < datos.steps.Length - 1)
                {
                    float _difX =
                        datos.steps[i + 1].cars[j].position[0] -
                        datos.steps[i].cars[j].position[0];
                    float _difZ =
                        datos.steps[i + 1].cars[j].position[1] -
                        datos.steps[i].cars[j].position[1];
                    if (_difX == 0 && _difZ == 0)
                    {
                        _direcciones[j] = new Vector3(_difXAnt, 0, _difZAnt);
                    }
                    else
                    {
                        _direcciones[j] = new Vector3(_difX, 0, _difZ);
                        _difXAnt = _difX;
                        _difZAnt = _difZ;
                    }
                }
                else
                {
                    _direcciones[j] = Vector3.zero;
                }
            }

            //En cada paso calcular vector direccion de cada carro
            print("Sali del for");
            yield return new WaitForSeconds(1 / _escalaTiempo);
        }
    }
}
