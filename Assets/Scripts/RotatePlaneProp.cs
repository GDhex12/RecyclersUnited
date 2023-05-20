using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlaneProp : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 500f; // adjust the speed of rotation as needed

    private void Update()
    {
        // Calculate the rotation amount based on time and speed
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Rotate the propeller around its local forward axis
        transform.Rotate(Vector3.forward, rotationAmount);
    }
}
