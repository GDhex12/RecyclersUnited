using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [Header("Assign Every Scene")]
    [SerializeField] public ExperienceStats experienceManager;
    [Header("Assign Game Scene")]
    public Storage storage;
    public VehicleSystem vehicle;
    public VehicleCooldown vehicleCooldown;
    public SpawnObject spawner;
    public Transform volunteerRoot;
    public GameObject VehiclePrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
