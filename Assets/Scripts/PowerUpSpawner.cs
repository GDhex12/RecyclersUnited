using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PowerUpSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int spawnRate=0;
    [SerializeField] private int maxPowerUps;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private Transform[] powerUpsSpawnPoint = new Transform[2];
    [SerializeField] private List<GameObject> allPowerUps;
    [SerializeField] private List<GameObject> activePowerUps;
    [SerializeField] GameObject helicopter;
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

        }
        FunctionTimer.Create(PrepNextPowerUp, spawnRate);
    }

    public void PrepNextPowerUp()
	{
        timeToNextPowerUp = Random.Range(5f, spawnRate);
        FunctionTimer.Create(SpawnPowerUp, timeToNextPowerUp);
        
    }

    public void DecreasePowerUp(GameObject powerUp)
	{
      
    }




}
