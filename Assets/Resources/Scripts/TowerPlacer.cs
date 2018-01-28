using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerPlacer : Singleton<TowerPlacer> {

	[Serializable]
	public class TowerPrefab {
		public TowerType type;
		public GameObject prefab;
	}

	public List<TowerPrefab> towerPrefabs;

	// Use this for initialization
	void Start () {
		if (towerPrefabs == null)
			towerPrefabs = new List<TowerPrefab> ();
	}

    public GameObject PlaceATower(TowerType TypeOfTower, Color PlayerColor)
    {
		GameObject TowerToPlace = null;
		foreach (var towerPrefab in towerPrefabs) {
			if (towerPrefab.type == TypeOfTower) {
				TowerToPlace = towerPrefab.prefab;
				break;
			}
		}

        GameObject InstancedTower = Instantiate(TowerToPlace);
        InstancedTower.GetComponent<SpriteRenderer>().color = PlayerColor;
        return InstancedTower;
    }


}
