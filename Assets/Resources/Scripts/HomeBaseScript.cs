using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBaseScript : MonoBehaviour {
	// Called when the player loses health
	public void OnPlayerLoseHealth(float currentHealth, float initialHealth) {
		if(currentHealth <= 0) {
			Destroy(gameObject);
		}
	}
	
}
