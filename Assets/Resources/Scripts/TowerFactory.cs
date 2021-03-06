using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {
	
	public TowerType towerType;
	public BuildingProgressBar progressBar;
	public TowerPen towerPen;

	public bool isBuilding = false;

	public int buildingCost;
	public float totalWork;
	public float currentWork;
	public float workRate;

	private PlayerManager playerManager;

	// Use this for initialization
	void Start () {
		playerManager = PlayerManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
		if(isBuilding) {
			WorkTick ();
		} else if(Input.GetMouseButtonDown(0) && !PlayerManager.instance.isDead) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Factory"));
			if(hit && hit.transform == transform) {
				Build ();
			}
		}
	}
		
	public void Build() {
		if (playerManager.money >= buildingCost) {
			playerManager.money -= buildingCost;
			isBuilding = true;
			currentWork = 0f;
			progressBar.Show ();
		}
	}

	public void FinishBuilding() {
		isBuilding = false;
		progressBar.Reset ();

		var color = playerManager.localNetworkedPlayer != null ? playerManager.localNetworkedPlayer.playerColor : Color.red;
		var instance = TowerPlacer.instance.PlaceATower (towerType, color);

		instance.transform.position = new Vector3(towerPen.transform.position.x, towerPen.transform.position.y, instance.transform.position.z);
		instance.GetComponent<Tower>().CurrentSpot = towerPen;

		//var tower = instance.GetComponent<Tower> ();
		//tower.setTowerPen (towerPen);
	}

	/*
	public void CancelBuilding() {
		playerManager.money += buildingCost;
		buildingCost = 0;
		isBuilding = false;
		progressBar.Reset ();
	}*/

	void WorkTick() {
		currentWork += Time.deltaTime;
		progressBar.OnBuildingProgress (currentWork, totalWork);
		if (currentWork >= totalWork) {
			FinishBuilding ();
		}
	}
}
