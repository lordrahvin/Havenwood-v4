using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbDisplay : MonoBehaviour {


	private Image orbFillAmount;
	private Player player;
	private enum Orb { Essence, Health}
	[SerializeField] private Orb orbType;

	private void Start()
	{
		player = FindObjectOfType<Player>();
		orbFillAmount = GetComponent<Image>();
	}

	private void Update()
	{
		if (orbType == Orb.Health) { orbFillAmount.fillAmount = player.HealthAsPercentage; }
		else if (orbType == Orb.Essence) { orbFillAmount.fillAmount = player.EssenceAsPercentage; }
		else { orbFillAmount.fillAmount = 0f; }
	}



}