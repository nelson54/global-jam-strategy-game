using System;
using System.Collections;

public interface Wave {
	IEnumerator Spawn (EnemyPathNode startNode);
}

