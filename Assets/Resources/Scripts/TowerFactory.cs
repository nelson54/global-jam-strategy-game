using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {
	public GameObject towerPrefab;

	public BuildingProgressBar progressBar;
	public TowerPen towerPen;

	public bool isBuilding = false;

	public int buildingCost = 0;
	public float totalWork = 1f;
	public float currentWork = 0f;
	public float workRate = .1f;

	private PlayerManager playerManager;

	// Use this for initialization
	void Start () {
		playerManager = PlayerManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
		if(isBuilding) {
			WorkTick ();
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			Build (80, 1f);
		}
			
	}

	public void Build(int cost, float totalWork) {
		if (playerManager.money >= cost) {
			buildingCost = cost;
			playerManager.money -= cost;
			isBuilding = true;
			currentWork = 0f;
			this.totalWork = totalWork;
			progressBar.Show ();
		}
	}

	public void FinishBuilding() {
		isBuilding = false;
		progressBar.Reset ();

		var instance = GameObject.Instantiate (towerPrefab);
		instance.transform.position = this.transform.position + new Vector3(1f, -.5f, 0f);

		var tower = instance.GetComponent<Tower> ();
		tower.setTowerPen (towerPen);
	}

	public void CancelBuilding() {
		playerManager.money += buildingCost;
		buildingCost = 0;
		isBuilding = false;
		progressBar.Reset ();
	}

	void WorkTick() {
		currentWork += (workRate * Time.deltaTime);
		progressBar.OnBuildingProgress (currentWork, totalWork);
		if (currentWork >= totalWork) {
			FinishBuilding ();
		}
	}
}
