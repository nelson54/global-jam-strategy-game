using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : Singleton<TowerPlacer> {

    public List<GameObject> Towers;
    GameObject TowerToPlace;
    Transform PlacementPosition;

	// Use this for initialization
	void Start () {
        Towers = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject PlaceATower(TowerType TypeOfTower, Color PlayerColor)
    {
        for(int i = 0; i < Towers.Count; i++)
        {
            if(Towers[i].GetComponent<Tower>().Type == TypeOfTower)
            {
                TowerToPlace = Towers[i];
                break;
            }
        }
        GameObject InstancedTower = Instantiate(TowerToPlace, PlacementPosition);
        InstancedTower.GetComponent<SpriteRenderer>().color = PlayerColor;
        return InstancedTower;
    }


}
