using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathNode : MonoBehaviour {

	public List<EnemyPathNode> nextNodes;
	public EnemyPathNodeType nodeType;

	void Start () {
		if (nextNodes == null) {
			nextNodes = new List<EnemyPathNode> ();
		}
        
	}
	
	void OnDrawGizmos() {
		Vector3 from = new Vector3 (transform.position.x, transform.position.y, 0f);

		Gizmos.color = Color.white;

		Gizmos.DrawWireSphere (from, 0.1f);

		foreach (var node in nextNodes) {
			Vector3 to = new Vector3 (node.transform.position.x, node.transform.position.y, 0);
			Gizmos.DrawLine (from, to);
		}
	}
}
