using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCooldownUI : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void FastenCar()
    {
        GameManager.Instance.vehicleCooldown.FastenTimer(0.5f);
        animator.Play("Shrink");
    }
}
