using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHelicopterRotor : MonoBehaviour
{
    [SerializeField] private Transform pivotPoint; // the pivot point to rotate around
    [SerializeField] private float rotationSpeed = 100f; // adjust the speed of rotation as needed

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Time.time * rotationSpeed;

        // calculate the new position based on the pivot point and rotation angle
        Vector3 pivotToBlade = transform.position - pivotPoint.position;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        //Vector3 newPosition = pivotPoint.position + rotation * pivotToBlade;

        // apply the new position and rotation to the blade object
       // transform.position = newPosition;
        transform.localRotation = rotation;
    }
}
