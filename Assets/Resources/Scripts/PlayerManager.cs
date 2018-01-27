using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {

	public float initialHealth = 100;
	public float currentHealth;
	//public PlayerColor color;  //TODO define this

	public float health {
		get { return currentHealth; }
		set {
			var oldVal = currentHealth;
			currentHealth = Mathf.Max(value, 0f);

			if (currentHealth != oldVal) {
				UpdateHealthDisplay ();
				if (currentHealth <= 0) {
					OnPlayerHasLost ();
				}
			}
		}
	}

	public void StartGame() {
		currentHealth = initialHealth;
		//TODO other stuff!
	}

	//TODO this is happening instantaneously... there's no juice!
	public void OnEnemyReachedEnd(GameObject gameObject) {
		var enemy = gameObject.GetComponent<Enemy> ();
		if (enemy) {
			this.health -= enemy.damage;
		}
	}
		
	void Start () {
		//TODO get a reference to the player health meter	
		//TODO move this into the game flow management somehow!
		StartGame();
	}
	
	void UpdateHealthDisplay() {
		//TODO really updates of a canvas item
		Debug.Log( string.Format("Player's base Has {0} hp", currentHealth) ); 
	}

	void OnPlayerHasLost() {
		//TODO player loss management
	}
}
