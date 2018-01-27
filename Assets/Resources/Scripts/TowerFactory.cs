using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {
	public GameObject towerPrefab;
	public BuildingProgressBar progressBar;

	public bool isBuilding = false;

	public float totalWork = 1f;
	public float currentWork = 0f;
	public float workRate = .1f;

	// Use this for initialization
	void Start () {
		//progressBar = GetComponent<BuildingProgressBar> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(isBuilding) {
			WorkTick ();
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			Build (1f);
		}


	}

	public void Build(float totalWork) {
		isBuilding = true;
		currentWork = 0f;
		this.totalWork = totalWork;
		progressBar.Show ();
	}

	public void FinishBuilding() {
		isBuilding = false;
		progressBar.Reset ();

		var instance = GameObject.Instantiate (towerPrefab);
		instance.transform.position = this.transform.position + new Vector3(1f, -.5f, 0f);
	}

	public void CancelBuilding() {
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
