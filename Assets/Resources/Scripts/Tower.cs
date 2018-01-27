using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	// The layer number for the tower
	const int TOWER_LAYER_NUMBER = 11;

    //The bool that detects if this specific tower is being dragged
    private bool MouseIsDragging;
    private float Timer = 0;
    [SerializeField] float FireRate;
    [SerializeField] GameObject Bullet;
    [SerializeField] float BulletSpeed;
    public enum State { FindNextTarget, StartShooting }
    public State SwitchStates;
    //Stores the current enemy being shot at
    public GameObject EnemyBeingShot;
    public List<GameObject> DetectedEnemies; 


    // Use this for initialization
    void Start()
    {
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
        MouseIsDragging = !MouseIsDragging;

		if(!MouseIsDragging) {
			// raycast should ignore the tower
			int mask = 1 << TOWER_LAYER_NUMBER;
			mask = ~mask;
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);
			if (hit) {
				Transform objectHit = hit.transform;
				// Send the object to the other player

			}

		}
    }

    private void DraggingTower()
    {
        //While the game object is being dragged set its position to the mouse position
        if (MouseIsDragging)
        {
            Vector3 TrueMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 MousePosition = new Vector3(TrueMousePosition.x, TrueMousePosition.y, transform.position.z);
            gameObject.transform.position = MousePosition;
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
}
