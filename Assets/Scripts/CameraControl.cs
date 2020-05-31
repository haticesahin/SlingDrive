using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject car;
    Vector3 distance;

    void Start()
    {
        distance = transform.position - car.transform.position;
    }

    void LateUpdate()
    {
        transform.position = car.transform.position + distance;
    }
}
