using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    //The bool that detects if this specific tower is being dragged
    private bool MouseIsDragging;
    private float Timer = 0;
    [SerializeField] float FireRate;
    [SerializeField] GameObject Bullet;
    public enum State { FindNextTarget, StartShooting }
    public State SwitchStates;


    // Use this for initialization
    void Start()
    {
        //Initialize the tower as not being dragged
        MouseIsDragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        DraggingTower();
        StateMachine();
    }

    //When the mouse is clicked on the object toggle the dragging bool
    private void OnMouseDown()
    {
        MouseIsDragging = !MouseIsDragging;
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
            InstantiatedBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 0f);
            Timer = 0;
        }


    }

    private void FindNextTarget()
    {

    }

    public void StopShooting()
    {
        SwitchStates = State.FindNextTarget;
    }
}
