using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]
public class RequestConArgumentos : UnityEvent<ListaCarros> { }

public class JsonManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _requestRecibidaSinArgumentos;

    [SerializeField]
    private RequestConArgumentos _requestConArgumentos;

    [SerializeField]
    private string _url = "http://127.0.0.1:5000/";

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
                ListaCarros listaCarros =
                    JsonUtility.FromJson<ListaCarros>(jsonSource);
                print (listaCarros);
                for (int t = 0; t < listaCarros.Length; t++) {
                  for (int c = 0; c < listaCarros[t][0].Length; c++) {
                    print(listaCarros[t][0][c].position)
                  } 
                  for (int tl = 0; listaCarros[t][1].Length; tl++) {
                    print(listaCarros[t][1][tl].state)
                  }
                }
                _requestRecibidaSinArgumentos?.Invoke();
                _requestConArgumentos?.Invoke(listaCarros);
            }
            yield return new WaitForSeconds(_esperaEntreRequests);
        }
    }
}
