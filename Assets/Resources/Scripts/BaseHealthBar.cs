using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealthBar : MonoBehaviour {

	private Image healthBar;

	void Start () {
		healthBar = GetComponent<Image> ();
	}

	public void OnHealthChanged(float health, float totalHealth) {
		healthBar.fillAmount = health / totalHealth;
	}
}
