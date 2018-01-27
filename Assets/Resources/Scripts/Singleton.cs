using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> {

	public static T instance;

	protected virtual void Awake() {
		if (instance == null) {
			instance = (T)this;
		} else {
			Destroy (gameObject);
		}
	}

	// This is just from the boilerplate, we might not need it
	protected void SetDontDestroy() {
		DontDestroyOnLoad (gameObject);
	}
}
