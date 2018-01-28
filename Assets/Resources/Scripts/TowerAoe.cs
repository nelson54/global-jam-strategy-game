using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAoe : Tower {
	public float missRate = .2f;
	public float bulletDamage = 5f;
	public GameObject effect;
	public override void StartShooting()
    {
        Timer += Time.deltaTime;
        if(Timer > FireRate)
        {
			PartyHard ();
            Timer = 0;
        }
    }

    
    private void PartyHard ()
    {
        GameObject EnemyCircleDetector = transform.GetChild(0).gameObject;
        var EnemyList = Physics2D.OverlapCircleAll(transform.position, EnemyCircleDetector.GetComponent<CircleCollider2D>().radius, 1 << LayerMask.NameToLayer("Enemy"));
        if(EnemyList.Length > 0)
        {
			GameObject instantiatedEffect = Instantiate<GameObject> (effect);
			instantiatedEffect.transform.position = transform.position;

			for (int i = 0; i < EnemyList.Length * (1f-missRate); i++)
            {
                //If the enemy found in the list is not null start shooting at that
                if (EnemyList[i] != null)
                {
					FollowPathEnemy enemy = EnemyList [i].GetComponent<FollowPathEnemy> ();
					enemy.LoseHealth(bulletDamage, BulletTyper.Normal);
                }
            }
            SwitchStates = State.StartShooting;
        }
    }
}
