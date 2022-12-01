using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using UnityEngine;

public class TLManager : MonoBehaviour
{
    [SerializeField] private Light[] _objetoVerde;

    [SerializeField] private Light[] _objetoRojo;

    [SerializeField] private Light[] _objetoAmarillo;

    //[SerializeField] private int _tamanioDePool;
    // public Light ActivarObjeto(int state)
    // {
    //     print(state);
    //
    //     // revisar si queue tiene objetos disponibles
    //     if (_poolV == null || _poolV.Count == 0)
    //     {
    //         Debug.LogError("SE ACABO EL POOL, YA TRANQUILIZATE");
    //         return null;
    //     }
    //
    //     if (_poolR == null || _poolR.Count == 0)
    //     {
    //         Debug.LogError("SE ACABO EL POOL, YA TRANQUILIZATE");
    //         return null;
    //     }
    //
    //     if (_poolA == null || _poolA.Count == 0)
    //     {
    //         Debug.LogError("SE ACABO EL POOL, YA TRANQUILIZATE");
    //         return null;
    //     }
    //
    //     Light objetoActivadoV = _poolV.Dequeue();
    //     print(objetoActivadoV);
    //     objetoActivadoV.GetComponent<Light>().enabled = true;
    //     Light objetoActivadoR = _poolR.Dequeue();
    //     print(objetoActivadoR);
    //     objetoActivadoR.GetComponent<Light>().enabled = true;
    //     Light objetoActivadoA = _poolA.Dequeue();
    //     print(objetoActivadoA);
    //     objetoActivadoA.GetComponent<Light>().enabled = true;
    //     switch (state)
    //     {
    //         case 0:
    //             _objetoVerde.intensity = 5.0f;
    //             _objetoAmarillo.intensity = 0.0f;
    //             _objetoRojo.intensity = 0.0f;
    //             return objetoActivadoV;
    //             break;
    //         case 1:
    //             _objetoVerde.intensity = 0.0f;
    //             _objetoAmarillo.intensity = 5.0f;
    //             _objetoRojo.intensity = 0.0f;
    //             return objetoActivadoA;
    //             break;
    //         case 2:
    //             _objetoVerde.intensity = 0.0f;
    //             _objetoAmarillo.intensity = 0.0f;
    //             _objetoRojo.intensity = 5.0f;
    //             return objetoActivadoR;
    //             break;
    //         default:
    //             _objetoVerde.intensity = 5.0f;
    //             _objetoAmarillo.intensity = 5.0f;
    //             _objetoRojo.intensity = 5.0f;
    //             break;
    //     }
    //
    //     return null;
    // }
    public static TLManager Instance { get; private set; }

    void Awake()
    {
        //print("AWAKE");
        // mecanismo de singleton correctivo
        // osea, si ya existe pelas
        if (Instance != null)
        {
            // significa que ya fue asignada
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ActualizarEstados(Semaforo[] trafficLights)
    {
        for (int i = 0; i < trafficLights.Length; i++)
        {
            switch (trafficLights[i].state)
            {
                case 0:
                    _objetoVerde[i].intensity = 5.0f;
                    _objetoAmarillo[i].intensity = 0.0f;
                    _objetoRojo[i].intensity = 0.0f;
                    break;
                case 1:
                    _objetoVerde[i].intensity = 0.0f;
                    _objetoAmarillo[i].intensity = 5.0f;
                    _objetoRojo[i].intensity = 0.0f;
                    break;
                case 2:
                    _objetoVerde[i].intensity = 0.0f;
                    _objetoAmarillo[i].intensity = 0.0f;
                    _objetoRojo[i].intensity = 5.0f;
                    break;
                default:
                    _objetoVerde[i].intensity = 5.0f;
                    _objetoAmarillo[i].intensity = 5.0f;
                    _objetoRojo[i].intensity = 5.0f;
                    break;
            }
        }
    }
}