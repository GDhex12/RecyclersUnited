using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
[CreateAssetMenu(fileName = "New Vehice", menuName = "Vehice")]
public class Vehicle : ScriptableObject
{
	[SerializeField]private GameObject VehiclePrefab;
	[SerializeField] private int id;
	[SerializeField] private int price;
	[SerializeField] private int levelRequired;
	[SerializeField] private bool isDefault;



	public int GetPrice()
	{
		return price;
	}
	public int GetLevelRequired()
	{
		return levelRequired;
	}

	public bool IsDefault()
	{
		return isDefault;
	}
	public int GetId()
	{
		return id;
	}
}