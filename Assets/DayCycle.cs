using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private bool simulateDayTime = true;

    // Update is called once per frame
    void Update()
    {
        if (simulateDayTime){
        transform.Rotate(new Vector3(1,0,0), Time.deltaTime * speed);
        }
    }

    public void setSimulateDayTime()
    {
        simulateDayTime = !simulateDayTime;
    }
}
