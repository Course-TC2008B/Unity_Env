using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(1,0,0), Time.deltaTime * speed);
    }
}
