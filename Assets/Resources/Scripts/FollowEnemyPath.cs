using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemyPath : MonoBehaviour {

	public EnemyPathNode nextNode;
	public float epsilon = 0.1f;
	public float speed = 1f;

	private Rigidbody2D body;

	void Start() {
		body = GetComponent<Rigidbody2D> ();
	}

	//TODO need to change this to be a more traditional parametric path with t values and reckoning
	void Update () {
		UpdateVelocity ();
	}

	protected void UpdateVelocity() {
		if (nextNode == null) {
			body.velocity = Vector2.zero;
			return;
		}

		var delta = nextNode.transform.position - transform.position;
		if (delta.magnitude <= epsilon) {
			nextNode = GetNextNode ();  // this should choose based on the path its on?
			UpdateVelocity();
		} else {
			delta.Normalize ();
			body.velocity = speed * delta;
		}
	}

	//TODO this is a terrible name
	protected EnemyPathNode GetNextNode() {
		if (nextNode != null && nextNode.nextNodes.Count > 0) {
			return nextNode.nextNodes [0];
		}

		return null;
	}
}
