using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]
public class RequestConArgumentos : UnityEvent<ListSim> {
}

public class JsonManager : MonoBehaviour {
    
    [SerializeField] private RequestConArgumentos _requestConArgumentos;
    [SerializeField] private int population = 10;
    [SerializeField] private int seed = 1000;
    [SerializeField] private bool trafficLightsEvaluate = false;
    [SerializeField] private string _url = "http://127.0.0.1:5000/run";

    void Awake(){
        StartCoroutine(HacerRequest());
    }

    IEnumerator HacerRequest(){
        UnityWebRequest www = UnityWebRequest.Get(_url+"?population="+population+"&seed="+seed+"&evaluate_traffic="+trafficLightsEvaluate);

        yield return www.SendWebRequest();
        string jsonSource = null;
        if (www.isNetworkError || www.isHttpError){
            Debug.Log(www.error);
        }

        if (www.result != UnityWebRequest.Result.Success){
            print("ERROR EN REQUEST!");
        } else{
            jsonSource = www.downloadHandler.text;
        }

        print(jsonSource);
        if (jsonSource != null){
            ListSim listSim =
                JsonUtility.FromJson<ListSim>(jsonSource);
            _requestConArgumentos?.Invoke(listSim);
        }
    }

}