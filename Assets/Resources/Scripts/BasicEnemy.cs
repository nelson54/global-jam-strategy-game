using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy {

	public Vector2 target = new Vector3(10, 10);
	public float speed = 1f;
	public float inRangeDist = 0.5f;

	private Rigidbody2D body;

	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		Vector2 currPos = new Vector2 (transform.position.x, transform.position.y);
		Vector2 dir = target - currPos;
		if (dir.magnitude > inRangeDist) {
			dir.Normalize ();
			body.velocity = dir * speed;
		} else {
			body.velocity = Vector2.zero;
		}
	}
}
