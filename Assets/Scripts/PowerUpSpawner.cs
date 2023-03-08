using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float spawnRate;
    [SerializeField] private int maxPowerUps;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private Transform powerUpsSpawnPoint;
    [SerializeField] private List<GameObject> activePowerUps;
    private int timeToNextPowerUp;

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void SpawnPowerUp()
	{
        PrepNextPowerUp();


    }

    public void PrepNextPowerUp()
	{
        timeToNextPowerUp = Random.Range(5, 20);
        FunctionTimer.Create(SpawnPowerUp, timeToNextPowerUp);
	}

    public void DecreasePowerUp()
	{

	}




}
