
 using System;
 using System.Collections;
 using System.Collections.Generic;
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
            if (jsonSource != null)
            {
                ListSim listSim =
                    JsonUtility.FromJson<ListSim>(jsonSource);
                print (listSim);
                for (int st = 0; st < listSim.step.Length; st++)
                {
                    for (int cr = 0; cr < listSim.step[st].car.Length; cr++)
                    {
                        print(listSim.step[st].car[cr].position);
                    }
                    for (int sf = 0; sf < listSim.step[st].semf.Length; sf++)
                    {
                        print(listSim.step[st].semf[sf].state);
                    }
                }
                _requestRecibidaSinArgumentos?.Invoke();
                _requestConArgumentos?.Invoke(listSim);
            }
            yield return new WaitForSeconds(_esperaEntreRequests);
        }
    }
}
     
