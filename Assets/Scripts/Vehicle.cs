using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
[CreateAssetMenu(fileName = "New Vehice", menuName = "Vehice")]
public class Vehicle : ScriptableObject
{
	[SerializeField]private GameObject VehiclePrefab;

	[SerializeField] private int price;



	public int GetPrice()
	{
		return price;
	}
}