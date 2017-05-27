using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour {



    [SerializeField] private float currentHealthPoints;
    [SerializeField] private float maxHealthPoints = 100f;
	//[SerializeField] private float attackRadius = 2f;
	//[SerializeField] private float pursuitRadius = 20f;
	
	private AICharacterControl AIcontrol;
	private GameObject player;
	
	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		//AIcontrol = GetComponent<AICharacterControl>();
		//AIcontrol.SetTarget(transform);
		currentHealthPoints = maxHealthPoints;
	}

	private void Update()
	{
		
	}
	

	public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }

	public void TakeDamage (float damage) 
	{
		currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage,0f,maxHealthPoints);
	}

	private void OnDrawGizmos()
	{
		//Gizmos.color = Color.red;
		//Gizmos.DrawWireSphere(transform.position, attackRadius);

		//Gizmos.color = Color.blue;
		//Gizmos.DrawWireSphere(transform.position, pursuitRadius);

	}
}