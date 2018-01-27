using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableTowerSpot : MonoBehaviour {

	[SerializeField] SpriteRenderer SpriteToChange;
	[SerializeField] Color Unhighlighted;
	[SerializeField] Color Highlighted;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Called when the player is hovering over this thing while dragging a tower
	void OnMouseOver() {
		if(PlayerManager.instance.towerBeingDragged != null) {
			SpriteToChange.color = Highlighted;
		}
	}
	void OnMouseExit() {
		SpriteToChange.color = Unhighlighted;
	}
}
