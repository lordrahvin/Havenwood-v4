using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


	[SerializeField] private float maxHealthPoints = 100f;
	[SerializeField] private float maxEssencePoints = 100f;
	private float currentHealthPoints;
	private float currentEssencePoints;

	public float HealthAsPercentage 
	{ 
		get {	return currentHealthPoints / maxHealthPoints; } 
	}

	public float EssenceAsPercentage
	{
		get { return currentEssencePoints / maxEssencePoints; }
	}

	private void Start()
	{
		currentHealthPoints = maxHealthPoints;
		currentEssencePoints = maxEssencePoints;
	}
}
