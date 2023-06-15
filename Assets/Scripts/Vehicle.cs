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
    [SerializeField] private string Name;
    [SerializeField] private Sprite image;
    [SerializeField] private int price;
	[SerializeField] private int levelRequired;
    [SerializeField] private int capacity;
    [SerializeField] private int speed;
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
    public string GetName()
    {
        return Name;
    }

	public Sprite GetImage()
	{
		return image;
	}

	public int GetCapacity()
	{
		return capacity;
	}
}