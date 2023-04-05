using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PowerUpSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float spawnRate;
    [SerializeField] private int maxPowerUps;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private Transform[] powerUpsSpawnPoint = new Transform[2];
    [SerializeField] private List<GameObject> allPowerUps;
    [SerializeField] private List<GameObject> activePowerUps;
    private int timeToNextPowerUp;

    void Start()
    {
        PrepNextPowerUp();

    }

    public void SpawnPowerUp()
	{

        if (activePowerUps.Count < allPowerUps.Count - 1)
        {
            GameObject powerUp = allPowerUps[activePowerUps.Count];
            Vector3 offset = new Vector3(0f, 0f, Random.Range(0, 25));
            int direction = Random.value > 0.5 ? 0 : 1;
            powerUp.transform.position = powerUpsSpawnPoint[direction].position+ offset;
            powerUp.SetActive(true);
            powerUp.GetComponent<PowerUp>().Setup(direction);
            activePowerUps.Add(powerUp);
        }
    }

    public void PrepNextPowerUp()
	{
        timeToNextPowerUp = Random.Range(5, 20);
        FunctionTimer.Create(SpawnPowerUp, timeToNextPowerUp);
	}

    public void DecreasePowerUp(GameObject powerUp)
	{
        activePowerUps.Remove(powerUp);
        if (activePowerUps.Count <= 2)
        {
            PrepNextPowerUp();
        }
    }




}
