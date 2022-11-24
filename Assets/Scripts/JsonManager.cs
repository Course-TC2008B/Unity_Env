
 using System;
 using System.Collections;
 using System.Collections.Generic;
 using System.Linq;
 using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]
public class RequestConArgumentos : UnityEvent<ListSim> { }
 public class JsonManager : MonoBehaviour{

    [SerializeField]
    private UnityEvent _requestRecibidaSinArgumentos;

    [SerializeField]
    private RequestConArgumentos _requestConArgumentos;

    [SerializeField]
    private string _url = "http://127.0.0.1:5000/run";

    [SerializeField]
    private float _esperaEntreRequests = 1;

    //public ListSim _listSimJson;

    void Start()
    {
        StartCoroutine(HacerRequest());
    }

    IEnumerator HacerRequest()
    {
        while (true)
        {
            // hacer request al "server"
            // esto va a cambiar mañana
            // string jsonSource = PseudoServer.Instance.JSON;
            UnityWebRequest www = UnityWebRequest.Get(_url);
            yield return www.SendWebRequest();
            string jsonSource = null;
            if (www.result != UnityWebRequest.Result.Success)
            {
                print("ERROR EN REQUEST!");
            }
            else
            {
                jsonSource = www.downloadHandler.text;
            }
            print(jsonSource);
            if (jsonSource != null)
            {
                ListSim listSim=
                    JsonUtility.FromJson<ListSim>(jsonSource);
                // print (listSim);
                // for (int st = 0; st < listSim.steps.Length; st++)
                // {
                //     for (int ls = 0;  ls < listSim.steps[st].cars.Length; ls++)
                //     {
                //         print(listSim.steps[st].cars[ls].Keys.First());
                //     }
                //     for (int tf = 0;  tf < listSim.steps[st].traffic_lights.Length; tf++)
                //     {
                //         print(listSim.steps[st].cars[tf].Keys.First());
                //     }
                // }
                _requestRecibidaSinArgumentos?.Invoke();
                _requestConArgumentos?.Invoke(listSim);
            }
            yield return new WaitForSeconds(_esperaEntreRequests);
        }
    }
}
     
