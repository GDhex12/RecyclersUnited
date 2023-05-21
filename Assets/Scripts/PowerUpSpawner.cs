using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PowerUpSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int SpawnRateMin = 0;
    [SerializeField] private int SpawnRateMax=0;
    
    [SerializeField] private int maxPowerUps;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private Transform[] powerUpsSpawnPoint = new Transform[2];
    [SerializeField] private List<GameObject> allPowerUps;
    [SerializeField] private List<GameObject> activePowerUps;
    [SerializeField] GameObject helicopter;
    [SerializeField] GameObject plane;
    private float timeToNextPowerUp;

    void Start()
    {
        PrepNextPowerUp();

    }

    public void SpawnPowerUp()
	{
       
        if(Random.value > 0.5)
        {
            helicopter.GetComponent<Animator>().Play("HelicopterComes");
            helicopter.GetComponent<Animator>().ResetTrigger("MoveToExit");
        }
        else
        {
            plane.GetComponent<Animator>().Play("PlaneFlyBy");
        }
        FunctionTimer.Create(PrepNextPowerUp, SpawnRateMax);
    }

    public void PrepNextPowerUp()
	{
        timeToNextPowerUp = Random.Range(SpawnRateMin, SpawnRateMax);
        FunctionTimer.Create(SpawnPowerUp, timeToNextPowerUp);
        
    }

    public void DecreasePowerUp(GameObject powerUp)
	{
      
    }




}
