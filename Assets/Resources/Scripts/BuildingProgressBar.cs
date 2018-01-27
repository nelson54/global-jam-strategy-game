using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingProgressBar : MonoBehaviour {

	public Image progressBar;

	void Start () {
		progressBar = GetComponentInChildren<Image> ();
		OnBuildingProgress (0f, 1f);
	}

	public void OnBuildingProgress(float progress, float totalProgress) {
		progressBar.fillAmount = progress / totalProgress;
	}

	public void Show() {
		progressBar.enabled = true;
	}

	public void Hide() {
		progressBar.enabled = false;
	}

	public void Reset() {
		OnBuildingProgress (0f, 1f);
		Hide ();
	}
}
