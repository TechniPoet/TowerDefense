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

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
			
			if(hit.collider != null && hit.collider.transform == transform)
			{
				if (upgradesShown) {
					DisableTowerButtons();
				} else {
					EnableTowerButtons();
				}
			} else {
				DisableTowerButtons ();
			}
		}
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

	void EnableTowerButtons() {
		GameObject upgrades = transform.GetChild (0).gameObject;
		if (upgrades != null) {
			if (!upgrades.activeSelf) {
				upgrades.SetActive(true);
				highlightTower = true;
				upgradesShown = true;
			}
		}
	}

	void DisableTowerButtons() {
		GameObject upgrades = transform.GetChild (0).gameObject;
		if (upgrades != null) {
			if (upgrades.activeSelf) {
				upgrades.SetActive (false);
				highlightTower = false;
				upgradesShown = false;
			}
		}
	}

	public void CreateNormalTower() {
		Debug.Log ("Create Normal Tower");
		// Functionality for normal tower construction here
		DisableTowerButtons ();
	}

	public void CreateSlowTower() {
		Debug.Log ("Create Slow Tower");
		// Functionality for slow tower construction here
		DisableTowerButtons ();
	}


}
