using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerSpawn : MonoBehaviour {

	public int normalCost;
	public int slowCost;
	public Text normalCostText;
	public Text slowCostText;

	private Color startColor;
	private bool highlightTower;
	private bool upgradesShown;

	void Start() {
		startColor = GetComponent<SpriteRenderer>().color;
		highlightTower = false;
		upgradesShown = false;
		SetCosts ();
	}

	void Update() {
		if (highlightTower) {
			GetComponent<SpriteRenderer> ().color = Color.cyan;
		} else {
			GetComponent<SpriteRenderer>().color = startColor;
		}
	}

	void OnMouseDown() {
		ToggleTowerButtons();
	}

	void OnMouseEnter() {
		highlightTower = true;
	}

	void OnMouseExit() {
		if (!upgradesShown) {
			highlightTower = false;
		}
	}
	
	void SetCosts() {
		normalCostText.text = normalCost.ToString ();
		slowCostText.text = slowCost.ToString ();
	}


	void ToggleTowerButtons() {

		GameObject upgrades = transform.GetChild (0).gameObject;

		if (upgrades != null) {
			if (upgrades.activeSelf) {
				upgrades.SetActive(false);
				highlightTower = false;
				upgradesShown = false;
			} else {
				upgrades.SetActive(true);
				highlightTower = true;
				upgradesShown = true;
			}
		}
	}

	public void CreateNormalTower() {
		Debug.Log ("Create Normal Tower");
		// Functionality for normal tower construction here
		ToggleTowerButtons ();
	}

	public void CreateSlowTower() {
		Debug.Log ("Create Slow Tower");
		// Functionality for slow tower construction here
		ToggleTowerButtons ();
	}


}
