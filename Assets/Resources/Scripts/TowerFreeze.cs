using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFreeze : Tower {

	public override void StartShooting()
    {
        Timer += Time.deltaTime;
        if(Timer > FireRate)
        {
			DeployFreezeRay ();
            Timer = 0;
        }


    }

    
    private void DeployFreezeRay()
    {
        GameObject EnemyCircleDetector = transform.GetChild(0).gameObject;
        var EnemyList = Physics2D.OverlapCircleAll(transform.position, EnemyCircleDetector.GetComponent<CircleCollider2D>().radius, 1 << LayerMask.NameToLayer("Enemy"));
        if(EnemyList.Length > 0)
        {
            for (int i = 0; i < EnemyList.Length; i++)
            {
                //If the enemy found in the list is not null start shooting at that
                if (EnemyList[i] != null)
                {
					FollowPathEnemy enemy = EnemyList [i].GetComponent<FollowPathEnemy> ();
					enemy.startSlow ();
                }
            }
            SwitchStates = State.StartShooting;
        }
    }
}
