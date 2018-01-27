using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	// The layer number for the tower
	public static int TOWER_IGNORE_MASK { get; private set; }

    //The bool that detects if this specific tower is being dragged
    private bool MouseIsDragging;
    private float Timer = 0;
    [SerializeField] float FireRate;
    [SerializeField] GameObject Bullet;
    [SerializeField] float BulletSpeed;
    public enum State { FindNextTarget, StartShooting, Disabled }
    public State SwitchStates;
    //Stores the current enemy being shot at
    public GameObject EnemyBeingShot;
    public List<GameObject> DetectedEnemies; 


    // Use this for initialization
    void Start()
    {
		TOWER_IGNORE_MASK = ~(1 << LayerMask.NameToLayer("Tower")) | (1 << LayerMask.NameToLayer("Tower Detector"));
        //Initialize the tower as not being dragged
        MouseIsDragging = false;
        DetectedEnemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        DraggingTower();
        StateMachine();
        //If an enemy gets killed (Becomes Null) when it's being shot remove it from the list and find a new target
        if(EnemyBeingShot == null)
        {
            SwitchStates = State.FindNextTarget;
            DetectedEnemies.Remove(EnemyBeingShot);
        }
    }

    //When the mouse is clicked on the object toggle the dragging bool
    private void OnMouseDown()
    {
		// Start dragging if the player isn't dragging
		if(PlayerManager.instance.towerBeingDragged == null) {
			MouseIsDragging = true;
			PlayerManager.instance.towerBeingDragged = this;
		}

		// If the player is already dragging, check to see if we're dragging this one.
		else if(PlayerManager.instance.towerBeingDragged == this) {
			// Stop dragging
			MouseIsDragging = false;
			PlayerManager.instance.towerBeingDragged = null;

			// Raycast to see where we're trying to put the tower
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, TOWER_IGNORE_MASK);
			if (hit) {
				TowerSendPlatform platform = hit.transform.GetComponent<TowerSendPlatform>();
				if(platform != null) {
					// Send the object to the other player
					Debug.Log("sending to player " + platform.Player);

				}
			}

		}

    }

    private void DraggingTower()
    {
        //While the game object is being dragged set its position to the mouse position
        if (MouseIsDragging)
        {
            //Disable the tower once you're dragging it
            SwitchStates = State.Disabled;
            Vector3 TrueMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 MousePosition = new Vector3(TrueMousePosition.x, TrueMousePosition.y, transform.position.z);
			// bound the tracking only to valid positions
			if(MousePosition.x > -8.8f && MousePosition.x < 8.8f && MousePosition.y < 5f && MousePosition.y > -3.8f) {
				gameObject.transform.position = MousePosition;
			}
        }
    }

    //Contains the switch statement that tells the tower what to do
    private void StateMachine()
    {
        switch(SwitchStates)
        {
            case State.StartShooting:
                StartShooting();
                break;
            case State.FindNextTarget:
                FindNextTarget();
                break;
            case State.Disabled:
                TowerIsDisabled();
                break;
        }
    }

    private void StartShooting()
    {
        Timer += Time.deltaTime;
        if(Timer > FireRate)
        {
            //Instantiate an object and set their velocity to the calculated value of where the enemy they're shooting will be
            GameObject InstantiatedBullet;
            InstantiatedBullet = Instantiate(Bullet, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0f), transform.rotation);
            //Draw a vector from the tower to the enemy
            Vector2 VectorToEnemy = (EnemyBeingShot.transform.position - this.transform.position);
            //Normalize the vector from the tower to the enemy and apply the bullet speed to it
            InstantiatedBullet.GetComponent<Rigidbody2D>().velocity = VectorToEnemy.normalized * BulletSpeed;
            Timer = 0;
        }


    }

    private void FindNextTarget()
    {
        if(DetectedEnemies.Count != 0)
        {
            print(DetectedEnemies[0]);
            for (int i = 0; i < DetectedEnemies.Count; i++)
            {
                //If the enemy found in the list is not null start shooting at that
                if (DetectedEnemies[i] != null)
                {
                    EnemyBeingShot = DetectedEnemies[i];
                    SwitchStates = State.StartShooting;
                }
            }
        }
    }

    private void TowerIsDisabled()
    {
        //Added in case we need certain behavior while the tower is disabled
    }
}
