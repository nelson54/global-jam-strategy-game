using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
	public float defaultSpeed = 1f;

	public float damage = 1f;
	public float speed;

	public bool slowed = false;
	public float slowSpeed = .3f;
	public float slowTime = 0f;
	public float slowLength = .6f;

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
}
