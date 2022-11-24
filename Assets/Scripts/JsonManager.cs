
 using System;
 using System.Collections;
 using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]
public class RequestConArgumentos : UnityEvent<ListaCarros> { }
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
                ListaCarros listaSim =
                    JsonUtility.FromJson<ListaCarros>(jsonSource);
                print (listaSim);
                for (int step = 0; step < listaSim.steps.Length; step++)
                {
                    for (int car = 0; car < listaSim.steps[step][0].Length; car++)
                    {
                        print(listaSim.steps[step][0][car].position);
                    }
                    for (int tl = 0; listaSim.steps[step][1].Length; tl++)
                    {
                        print(listaSim.steps[step][1][tl].state);
                    }
                }
                _requestRecibidaSinArgumentos?.Invoke();
                _requestConArgumentos?.Invoke(listaSim);
            }
            yield return new WaitForSeconds(_esperaEntreRequests);
        }
    }
}
     
