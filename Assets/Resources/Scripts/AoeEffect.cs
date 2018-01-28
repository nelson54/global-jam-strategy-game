using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeEffect : MonoBehaviour {
	float reduceScalePerTick = .01f;
	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (1.5f, 1.5f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x > 0) {
			transform.localScale -= new Vector3 (reduceScalePerTick, reduceScalePerTick, 0);
		} else {
			Destroy (this);
		}
	}
}
