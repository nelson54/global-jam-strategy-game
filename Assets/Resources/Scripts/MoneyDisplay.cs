using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour {

	private Text displayText;

	void Awake () {
		displayText = GetComponent<Text> ();
	}
	
	public void OnMoneyChanged(int value) {
		displayText.text = string.Format ("${0}", value);
	}
}
