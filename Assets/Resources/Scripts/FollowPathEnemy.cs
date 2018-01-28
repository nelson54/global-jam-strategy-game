using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FollowPathEnemy : Enemy {

	public EnemyPathNode nextNode;
	public float epsilon = 0.1f;

	private Rigidbody2D body;

	protected override void Start() {
		body = GetComponent<Rigidbody2D> ();
	}

	//TODO need to change this to be a more traditional parametric path with t values and reckoning
	protected override void Update () {
		base.Update ();
		UpdateVelocityForPath ();
	}

	protected void UpdateVelocityForPath() {
		if (nextNode == null) {
			body.velocity = Vector2.zero;
			OnReachedEnd ();
			return;
		}

		var delta = nextNode.transform.position - transform.position;
		if (delta.magnitude <= epsilon) {
			nextNode = GetNextNode ();  // this should choose based on the path its on?
			UpdateVelocityForPath();
		} else {
			delta.Normalize ();
			body.velocity = speed * delta;
		}
	}

	//TODO this is a terrible name
	protected EnemyPathNode GetNextNode() {
		if (nextNode != null && nextNode.nextNodes.Count > 0) {
            int chooseNextNode = Random.Range(0, nextNode.nextNodes.Count);
			return nextNode.nextNodes [chooseNextNode];
		}

		return null;
	}

	protected virtual void OnReachedEnd() {
		EnemyPathing.instance.reachedEnd.Invoke (gameObject);
		Destroy (gameObject); //TODO PERF - maybe a pool eventually?  nah...
	}
}
