using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableTowerSpot : MonoBehaviour {

	public bool SnapToCenter;
	public Tower tower;
	SpriteRenderer SpriteToChange;

	// Colors when highlighted/Unhighlighted
	[System.NonSerialized] public Color Unhighlighted;
	[System.NonSerialized] public Color Highlighted;

	// Can the turret fire while on this spot?
	public bool CanFire;

	// Use this for initialization
	void Start () {
		SpriteToChange = GetComponent<SpriteRenderer>();
		Unhighlighted = SpriteToChange.color;
		Highlighted = SpriteToChange.color * 1.3f;
	}
	
	// Update is called once per frame
	void Update () {
		// raycast to see if we're hovering over this gameobject while dragging a tower
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, Tower.TOWER_IGNORE_MASK);
		if(hit && hit.transform == transform && PlayerManager.instance.towerBeingDragged != null) {
			SpriteToChange.color = Highlighted;
		} else {
			SpriteToChange.color = Unhighlighted;
		}
	}

	virtual public bool isFull() {
		return tower != null;
	}

	// Called when the player is hovering over this thing while dragging a tower
	void OnMouseOver() {
		
	}
	void OnMouseExit() {
		
	}
}
