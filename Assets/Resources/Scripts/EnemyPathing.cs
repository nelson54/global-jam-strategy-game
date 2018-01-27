using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPathing : Singleton<EnemyPathing> {

	//public Dictionary<string, List<

	public ReachedEndEvent reachedEnd; //TODO add a type parameter?

	void Start() {
		if (reachedEnd == null)
			reachedEnd = new ReachedEndEvent();
	}
}
