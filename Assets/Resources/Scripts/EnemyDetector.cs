using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour {

    Tower Tower;

	// Use this for initialization
	void Start () {
        Tower = transform.parent.gameObject.GetComponent<Tower>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //If the tower is not shooting, set it to start shooting
            if(Tower.SwitchStates != Tower.State.StartShooting)
            {
                Tower.SwitchStates = Tower.State.StartShooting;
            }
        }
    }



    private void DetectEnemies()
    {

    }
}
