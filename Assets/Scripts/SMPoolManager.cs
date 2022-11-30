using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SMPoolManager : MonoBehaviour
{
    // LO PRIMERO
    // lo vamos a hacer un pseudo singleton
    // voy a usar una propiedad
    // mecanismo que divide el acceso de lectura / escritura a una variable
    // la variable puede ser anónima (como aquí)
    public static SMPoolManager Instance { get; private set; }

    // la variable puede estar definida
    private float _dummy;

    public float Dummy
    {
        get { return _dummy; }
        set { _dummy = value; }
    }

    [SerializeField] private Light _objetoVerde;
    [SerializeField] private Light _objetoRojo;
    [SerializeField] private Light _objetoAmarillo;

    [SerializeField] private int _tamanioDePool;

    private Queue<Light> _poolV, _poolA, _poolR;

    // sucede una vez al inicio, siempre corre no importa
    // si el objeto está deshabilitado
    // TODOS Los awakes de los objetos ya creados corren al principio
    // PERO no sabemos cuál corre antes que los demás
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

        //print("START");
        // vamos a crear el pool
        _poolV = new Queue<Light>();
        _poolA = new Queue<Light>();
        _poolR = new Queue<Light>();
        for (int i = 0; i < _tamanioDePool; i++)
        {
            Light nuevoVerde = Instantiate<Light>(_objetoVerde);
            Light nuevoRojo = Instantiate<Light>(_objetoRojo);
            Light nuevoAmarillo = Instantiate<Light>(_objetoAmarillo);
            _poolV.Enqueue(nuevoVerde);
            _poolA.Enqueue(nuevoRojo);
            _poolR.Enqueue(nuevoAmarillo);
            nuevoVerde.GetComponent<Light>().enabled = false;
            nuevoRojo.GetComponent<Light>().enabled = false;
            nuevoAmarillo.GetComponent<Light>().enabled = false;
        }
    }

    // Start is called before the first frame update
    // corre para todos después de terminar TODOS los awakes
    void Start()
    {
    }

    // Update is called once per frame
    // frame?
    // corre en intervalos irregulares
    void Update()
    {
        //print("UPDATE");
    }

    // corre también 1 vez por frame
    // se ejecuta al terminar TODOS los updates
    // de TODOS los componentes de TODOS los objetos
    void LateUpdate()
    {
        //print("LATE UPDATE");
    }

    // corre en intervalos regulares especificados por engine
    // tiene que ser menos frames que update
    void FixedUpdate()
    {
        //print("FIXED UPDATE");
    }

    public Light ActivarObjeto(int state)
    {
        print(state);

        // revisar si queue tiene objetos disponibles
        if (_poolV == null || _poolV.Count == 0)
        {
            Debug.LogError("SE ACABO EL POOL, YA TRANQUILIZATE");
            return null;
        }

        if (_poolR == null || _poolR.Count == 0)
        {
            Debug.LogError("SE ACABO EL POOL, YA TRANQUILIZATE");
            return null;
        }

        if (_poolA == null || _poolA.Count == 0)
        {
            Debug.LogError("SE ACABO EL POOL, YA TRANQUILIZATE");
            return null;
        }

        Light objetoActivadoV = _poolV.Dequeue();
        print(objetoActivadoV);
        objetoActivadoV.GetComponent<Light>().enabled = true;
        Light objetoActivadoR = _poolR.Dequeue();
        print(objetoActivadoR);
        objetoActivadoR.GetComponent<Light>().enabled = true;
        Light objetoActivadoA = _poolA.Dequeue();
        print(objetoActivadoA);
        objetoActivadoA.GetComponent<Light>().enabled = true;
        switch (state)
        {
            case 0:
                _objetoVerde.intensity = 5.0f;
                _objetoAmarillo.intensity = 0.0f;
                _objetoRojo.intensity = 0.0f;
                return objetoActivadoV;
                break;
            case 1:
                _objetoVerde.intensity = 0.0f;
                _objetoAmarillo.intensity = 5.0f;
                _objetoRojo.intensity = 0.0f;
                return objetoActivadoA;
                break;
            case 2:
                _objetoVerde.intensity = 0.0f;
                _objetoAmarillo.intensity = 0.0f;
                _objetoRojo.intensity = 5.0f;
                return objetoActivadoR;
                break;
            default:
                _objetoVerde.intensity = 5.0f;
                _objetoAmarillo.intensity = 5.0f;
                _objetoRojo.intensity = 5.0f;
                break;
        }

        return null;
    }
}


// public void DesactivarObjeto(Light objetoADesactivar)
    //{
      //  objetoADesactivarV.GetComponent<Light>().enabled = false;
    //    _poolV.Enqueue (objetoADesactivar);
   // }

