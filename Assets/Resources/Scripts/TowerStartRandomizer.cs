using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStartRandomizer : Singleton<TowerStartRandomizer> {

    public GameObject Factory1;
    public GameObject Factory2;
    public GameObject Factory3;
    public List<GameObject> FactoryTypes;

	// Use this for initialization
	void Start () {
        GameObject RandomFactory;
        RandomFactory = FactoryTypes[Random.Range(0, FactoryTypes.Count)];
        GameObject PlacedFactory1 = Instantiate(RandomFactory, Factory1.transform.position, Quaternion.identity);
        RandomFactory = FactoryTypes[Random.Range(0, FactoryTypes.Count)];
        GameObject PlacedFactory2 = Instantiate(RandomFactory, Factory2.transform.position, Quaternion.identity);
        RandomFactory = FactoryTypes[Random.Range(0, FactoryTypes.Count)];
        GameObject PlacedFactory3 = Instantiate(RandomFactory, Factory3.transform.position, Quaternion.identity);


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
