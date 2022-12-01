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