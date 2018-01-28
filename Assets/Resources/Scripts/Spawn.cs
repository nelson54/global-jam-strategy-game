using System;
using System.Collections;

public interface Spawn {
	IEnumerator Run (EnemyPathNode startNode);
}

