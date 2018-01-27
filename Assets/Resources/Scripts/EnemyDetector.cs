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
        //Check if the thing entering the trigger is on the enemy layer
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //When an enemy enters the collider add them to the list
            Tower.DetectedEnemies.Add(collider.gameObject);
            //If the tower is not shooting, set it to start shooting, this will stop it from exiting the shooting state when we don't want it to
            if (Tower.SwitchStates != Tower.State.StartShooting)
            {
                Tower.SwitchStates = Tower.State.StartShooting;
                Tower.EnemyBeingShot = collider.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //When an enemy leaves the collider remove them from the list
            Tower.DetectedEnemies.Remove(collider.gameObject);
            //If the enemy leaving the trigger is the enemy being shot by the tower change who the tower is shooting
            if (collider.gameObject == Tower.EnemyBeingShot)
            {
                Tower.SwitchStates = Tower.State.FindNextTarget;
            }
        }
    }

}
