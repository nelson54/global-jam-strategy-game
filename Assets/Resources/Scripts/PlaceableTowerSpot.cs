using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableTowerSpot : MonoBehaviour {

	public bool SnapToCenter;

	SpriteRenderer SpriteToChange;

	// Colors when highlighted/Unhighlighted
	[SerializeField] Color Unhighlighted;
	[SerializeField] Color Highlighted;

	// Use this for initialization
	void Start () {
		SpriteToChange = GetComponent<SpriteRenderer>();
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

	// Called when the player is hovering over this thing while dragging a tower
	void OnMouseOver() {
		
	}
	void OnMouseExit() {
		
	}
}
