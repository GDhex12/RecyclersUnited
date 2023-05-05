using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrateSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private int spawnRate = 6;
    [Header("Crate Objects")]
    [SerializeField] private Transform crateLeft;
    [SerializeField] private Transform crateRight;
    [Header("Spawn Between LeftSide")]
    [SerializeField] private Transform spawnFromLeft;
    [SerializeField] private Transform spawnToLeft;
    [Header("Spawn Between RightSide")]
    [SerializeField] private Transform spawnFromRight;
    [SerializeField] private Transform spawnToRight;
    private float timeToNextPowerUp;
    private MoveDirection moveDirection = MoveDirection.Right;
    private enum MoveDirection
    {
        Right,
        Left
    }
    private void Start()
    {
        PrepNextCrateSpawn();
    }
    private Vector3 RandomPositionBetween(Transform from, Transform to)
    {
        Vector3 vector1 = from.position;
        Vector3 vector2 = to.position;

        // Get a random point within the square
        return new Vector3(Random.Range(vector1.x, vector2.x), Random.Range(vector1.y, vector2.y), Random.Range(vector1.z, vector2.z));
    }

    public void SpawnCrate()
    {
        MoveDirection randomValue = (MoveDirection)Enum.GetValues(typeof(MoveDirection)).GetValue(UnityEngine.Random.Range(0, Enum.GetValues(typeof(MoveDirection)).Length));
        switch(randomValue)
        {
            default:
            case MoveDirection.Right:
                crateRight.position = RandomPositionBetween(spawnFromRight, spawnToRight);
                crateRight.gameObject.SetActive(true);
                break;
            case MoveDirection.Left:
                crateLeft.position = RandomPositionBetween(spawnFromLeft, spawnToLeft);
                crateLeft.gameObject.SetActive(true);
                break;
        }
        PrepNextCrateSpawn();
    }
    public void PrepNextCrateSpawn()
    {
        timeToNextPowerUp = Random.Range(20f, spawnRate);
        FunctionTimer.Create(SpawnCrate, timeToNextPowerUp);
    }
}
