using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	// The layer number for the tower
	public static int TOWER_IGNORE_MASK { get; private set; }

    //The bool that detects if this specific tower is being dragged
    public bool MouseIsDragging;
    private float Timer = 0;
    [SerializeField] float FireRate;
    [SerializeField] GameObject Bullet;
    [SerializeField] float BulletSpeed;
    public enum State { FindNextTarget, StartShooting, Disabled }
    public State SwitchStates;
    //Stores the current enemy being shot at
    public GameObject EnemyBeingShot;
    public TowerType Type;

	public PlaceableTowerSpot CurrentSpot;

	//private TowerPen towerPen;

    // Use this for initialization
    void Start()
    {
		TOWER_IGNORE_MASK = ~LayerMask.GetMask("Tower", "Tower Detector");
        //Initialize the tower as not being dragged
        MouseIsDragging = false;
		SwitchStates = State.Disabled;
    }

    // Update is called once per frame
    void Update()
    {
        DraggingTower();
        StateMachine();
        //If an enemy gets killed (Becomes Null) when it's being shot remove it from the list and find a new target
        if(PlayerManager.instance.isDead)
        {
            SwitchStates = State.Disabled;
        }
        else if(EnemyBeingShot == null && SwitchStates != State.Disabled)
        {
            SwitchStates = State.FindNextTarget;
        }
    }

	//public void setTowerPen(TowerPen towerPen) {
	//	this.towerPen = towerPen;
	//}

    //When the mouse is clicked on the object toggle the dragging bool
    private void OnMouseDown()
    {
        if(PlayerManager.instance.isDead)
        {
            SwitchStates = State.FindNextTarget;
        }
		// Start dragging if the player isn't dragging
		else if(PlayerManager.instance.towerBeingDragged == null) {
			
			MouseIsDragging = true;
            //Disable the tower once you're dragging it
            SwitchStates = State.Disabled;
            PlayerManager.instance.towerBeingDragged = this;
		}

		// If the player is already dragging, check to see if we're dragging this one.
		else if(PlayerManager.instance.towerBeingDragged == this) {
			// Raycast to see where we're trying to put the tower
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, TOWER_IGNORE_MASK);
			if (hit) {
				// Placing on one of the sending platformers
				TowerSendPlatform platform = hit.transform.GetComponent<TowerSendPlatform>();
				if(platform != null && platform.enabled) {

					// Send the object to the other player
					PlayerManager.instance.localNetworkedPlayer.CmdSendTower (
						Type,
						platform.networkedPlayer.netId
					);

					PlayerManager.instance.towerBeingDragged = null;

					Destroy (gameObject);
					return;
				}

				PlaceableTowerSpot placeableSpot = hit.transform.GetComponent<PlaceableTowerSpot>();
				if(placeableSpot != null) {
					// Send the object to the other player
					setPlaceableTowerSpot (placeableSpot);

					// Stop dragging
					MouseIsDragging = false;
					if(CurrentSpot.CanFire)
						SwitchStates = State.FindNextTarget;
					PlayerManager.instance.towerBeingDragged = null;
					return;
				}
			}

			if (CurrentSpot) {
				setPlaceableTowerSpot (CurrentSpot);
			}

			// Stop dragging
			MouseIsDragging = false;
			if(CurrentSpot.CanFire)
				SwitchStates = State.FindNextTarget;
			PlayerManager.instance.towerBeingDragged = null;
		}

    }

    private void DraggingTower()
    {
        //While the game object is being dragged set its position to the mouse position
        if (MouseIsDragging)
        {
            Vector3 TrueMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 MousePosition = new Vector3(TrueMousePosition.x, TrueMousePosition.y, transform.position.z);
			// bound the tracking only to valid positions
			if(MousePosition.x > -8.8f && MousePosition.x < 8.8f && MousePosition.y < 5f && MousePosition.y > -5f) {
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
			Bullet bullet = InstantiatedBullet.GetComponent<Bullet> ();
			Rigidbody2D body = EnemyBeingShot.GetComponent<Rigidbody2D> ();
			//Draw a vector from the tower to the enemy
			Vector2 Position = EnemyBeingShot.transform.position;
			Vector3 FuturePosition = new Vector2 (Position.x, Position.y) + body.velocity * Time.deltaTime;
			Vector2 VectorToEnemy = FuturePosition - transform.position;

            //Normalize the vector from the tower to the enemy and apply the bullet speed to it
            InstantiatedBullet.GetComponent<Rigidbody2D>().velocity = VectorToEnemy.normalized * BulletSpeed;
            Timer = 0;
        }


    }

    //TODO Find next target isn't always been called when it should??? find out why
    private void FindNextTarget()
    {
        GameObject EnemyCircleDetector = transform.GetChild(0).gameObject;
        var EnemyList = Physics2D.OverlapCircleAll(transform.position, EnemyCircleDetector.GetComponent<CircleCollider2D>().radius, 1 << LayerMask.NameToLayer("Enemies"));
        if(EnemyList.Length > 0)
        {
            float CurrentClosestDistanceToNextNode = 10000f;
            for (int i = 0; i < EnemyList.Length; i++)
            {
                //If the enemy found in the list is not null start shooting at that
                if (EnemyList[i] != null)
                {
                    float PossibleNextNode = EnemyList[i].GetComponent<FollowPathEnemy>().DistanceToNextNodeCalculation();
                    if (PossibleNextNode < CurrentClosestDistanceToNextNode)
                    {
                        EnemyBeingShot = EnemyList[i].gameObject;
                    }
                }
            }
            SwitchStates = State.StartShooting;
        }
    }

	public void setPlaceableTowerSpot(PlaceableTowerSpot newSpot) {
		if (newSpot == null || newSpot.isFull()) {
			//spot = towerPen;
			transform.position = new Vector3(CurrentSpot.transform.position.x, CurrentSpot.transform.position.y, transform.position.z);
		}
		else {
			transform.position = new Vector3(newSpot.transform.position.x, newSpot.transform.position.y, transform.position.z);
			newSpot.tower = this;
			CurrentSpot.tower = null;
			CurrentSpot = newSpot;
		}
	}

    private void TowerIsDisabled()
    {
        //Added in case we need certain behavior while the tower is disabled
    }
}
