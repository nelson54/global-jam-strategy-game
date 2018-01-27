using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    //The bool that detects if this specific tower is being dragged
    private bool MouseIsDragging;
    [SerializeField] float FireRate;
    [SerializeField] GameObject Bullet;


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

    private void ShootEnemy(GameObject Enemy)
    {

    }

    private void StopShootingEnemy()
    {

    }
}
