using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
	public float defaultSpeed = 1f;

	public float damage = 1f;
	public float speed;
    public float Health;
    public bool Armored;

    public bool slowed = false;
	public float slowSpeed = .3f;
	public float slowTime = 0f;
	public float slowLength = .6f;
	public int cashValue = 50;

	protected virtual void Start() {
		speed = defaultSpeed;
	}

	protected virtual void Update() {
		if (slowed) {
			slowTick ();
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (!slowed) {
				startSlow ();
			}
		}
	}

	protected void startSlow() {
		if (!slowed) {
			speed = slowSpeed;
		}
		slowed = true;
		slowTime = 0f;
	}

	protected void slowTick() {
		slowTime += Time.deltaTime;

		if(slowTime >= slowLength) {
			slowed = false;
			speed = defaultSpeed;
		}
	}

    public void LoseHealth(float BulletDamage, BulletTyper BulletType)
    {
        //If the enemy is armored and they are not being hit with armorpiercing bullets, deal half damage
        if(Armored && BulletType != BulletTyper.ArmorPiercing)
        {
            Health -= BulletDamage / 2;
        }
        //Otherwise do normal damage
        else
        {
            Health -= BulletDamage;
        }
        if (Health <= 0)
        {
            PlayerManager.instance.money += cashValue;
            Destroy(gameObject);
        }
    }
}
