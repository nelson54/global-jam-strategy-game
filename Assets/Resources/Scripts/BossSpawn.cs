using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : Spawn {

    public GameObject enemyPrefab;
    public float hp;
    public float interval;

    public IEnumerator Run(EnemyPathNode start)
    {
        var instance = GameObject.Instantiate(enemyPrefab);
        hp = instance.GetComponent<Enemy>().Health;
        instance.transform.position = start.transform.position;
        var follow = instance.GetComponent<FollowPathEnemy>();
        follow.nextNode = start;
		yield return null;
        
    }
}
