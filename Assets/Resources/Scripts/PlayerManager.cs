using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {

	public float initialHealth = 100;
	public float currentHealth;
	//public PlayerColor color;  //TODO define this

	public HealthChangedEvent healthChanged;

	public float health {
		get { return currentHealth; }
		set {
			var oldVal = currentHealth;
			currentHealth = Mathf.Clamp(value, 0f, initialHealth);

			if (currentHealth != oldVal) {
				OnHealthChanged ();
				if (currentHealth <= 0) {
					OnPlayerHasLost ();
				}
			}
		}
	}

	// Is the player dead?
	public bool isDead { get { return health <= 0; } }

	// If the player is dragging a tower, it's stored here. Otherwise, this is null
	public Tower towerBeingDragged = null;

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

		if (healthChanged == null) {
			healthChanged = new HealthChangedEvent ();
		}

		//TODO move this into the game flow management somehow!
		StartGame();
	}
	
	void OnHealthChanged() {
		healthChanged.Invoke (currentHealth, initialHealth); //TODO add in the player identifier (modify event)
		Debug.Log( string.Format("Player's base Has {0} hp", currentHealth) ); 
	}

	void OnPlayerHasLost() {
		//TODO player loss management
	}
}
