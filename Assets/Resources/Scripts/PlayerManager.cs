﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {

	public float initialHealth = 100;
	public float currentHealth;
	public int initialMoney = 500;
	public int currentMoney;

	public NetworkedPlayer localNetworkedPlayer;

	//public PlayerColor color;  //TODO define this

	public HealthChangedEvent healthChanged;
	public MoneyChangedEvent moneyChanged;

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
	[NonSerialized] public Tower towerBeingDragged = null;

	public int money {
		get { return currentMoney; }
		set {
			var oldVal = currentMoney;
			currentMoney = Math.Max (value, 0);

			if (currentMoney != oldVal) {
				OnMoneyChanged ();
			}
		}
	}

	public void StartGame() {
		currentHealth = initialHealth;
		currentMoney = initialMoney;

		OnMoneyChanged ();
		OnHealthChanged ();

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

		if(localNetworkedPlayer != null)
			localNetworkedPlayer.CmdUpdateHealth(currentHealth, initialHealth);

	}

	void OnPlayerHasLost() {
		//TODO player loss management
	}

	void OnMoneyChanged() {
		moneyChanged.Invoke (currentMoney);
	}
}
