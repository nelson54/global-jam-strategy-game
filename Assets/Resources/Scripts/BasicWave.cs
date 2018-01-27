using System.Collections;
using System;
using UnityEngine;

public class BasicWave : Wave
{
	public GameObject enemyPrefab;
	public int size;
	public float interval;

	public IEnumerator Spawn(EnemyPathNode start) {
		for (var i = 0; i < size; i++) {
			var instance = GameObject.Instantiate (enemyPrefab);
			instance.transform.position = start.transform.position;

			var follow = instance.GetComponent<FollowPathEnemy> ();
			follow.nextNode = start;
				
			if (i < size - 1) {
				yield return new WaitForSeconds (interval);
			}
		}
	}
}
