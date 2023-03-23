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
    [SerializeField] private List<GameObject> allPowerUps;
    [SerializeField] private List<GameObject> activePowerUps;
    private int timeToNextPowerUp;

    void Start()
    {
        PrepNextPowerUp();

    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void SpawnPowerUp()
	{
        
        //powerUpPrefab.SetActive(true);
        //
        if (activePowerUps.Count < allPowerUps.Count - 1)
        {
            GameObject powerUp = allPowerUps[activePowerUps.Count];
            Vector3 offset = new Vector3(0f, 0f, Random.Range(0, 25));
            powerUp.transform.position = powerUpsSpawnPoint.position+ offset;
            powerUp.SetActive(true);
            powerUp.GetComponent<PowerUp>().Setup();
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
