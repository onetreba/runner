using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCameraController : MonoBehaviour
{

    public Transform Target;
    public float Smoothing;

    Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - Target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = Target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, Smoothing * Time.deltaTime);
    }
}
