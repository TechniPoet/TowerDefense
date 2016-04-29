using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class TowerChoice
{
	public TowerSpawn.TowerType type;
	public GameObject prefab;
	public int cost;
	public List<int> upgradeOptions;
}


public class TowerSpawn : MonoBehaviour {

	public enum TowerType
	{
		regular,
		multiRegular,
		slow,
		multiSlow,
		unit,
	}
	
	public List<TowerChoice> choices;
	public UIManager manager;
	public GameObject upgrade1;
	public GameObject upgrade2;
	public GameObject upgrade3;
	public Text upgrade1Cost;
	public Text upgrade2Cost;
	public Text upgrade3Cost;

	private Color startColor;
	private bool highlightTower;
	private bool upgradesShown;
	private bool init = false;
	private TowerChoice currTower;
	private GameObject tower;

	void Start() {
		startColor = GetComponent<SpriteRenderer>().color;
		highlightTower = false;
		upgradesShown = false;
		SetCosts ();
	}

	void Update() {
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		highlightTower = (mousePos.x < transform.position.x + .5f && mousePos.x > transform.position.x - .5f &&
				mousePos.y < transform.position.y + .5f && mousePos.y > transform.position.y - .5f);
		if (highlightTower) {
			GetComponent<SpriteRenderer> ().color = Color.cyan;
		} else {
			GetComponent<SpriteRenderer>().color = startColor;
		}

		if (!init && upgradesShown)
		{
			upgrade1.GetComponent<Button>().interactable = choices[0].cost <= manager.goldCount;
			upgrade2.GetComponent<Button>().interactable = choices[1].cost <= manager.goldCount;
			upgrade3.GetComponent<Button>().interactable = choices[2].cost <= manager.goldCount;
		}
		else if (init && upgradesShown && currTower != null)
		{
			switch (currTower.type)
			{
				case TowerType.slow:
					upgrade1.SetActive(false);
					upgrade3.SetActive(false);
					if (currTower.upgradeOptions.Count > 0) 
						upgrade2.GetComponent<Button>().interactable = choices[currTower.upgradeOptions[0]].cost <= manager.goldCount;
					break;
				case TowerType.regular:
					upgrade2.SetActive(false);
					upgrade3.SetActive(false);
					if (currTower.upgradeOptions.Count > 0)
						upgrade1.GetComponent<Button>().interactable = choices[currTower.upgradeOptions[0]].cost <= manager.goldCount;
					break;
				default:
					upgrade1.SetActive(false);
					upgrade2.SetActive(false);
					upgrade3.SetActive(false);
					break;
			}
		}

		if (Input.GetMouseButtonDown(0))
		{
			if (highlightTower)
			{
				if (upgradesShown)
				{
					DisableTowerButtons();
				}
				else
				{
					SetCosts();
					EnableTowerButtons();
				}
			}
			else if (upgradesShown)
			{
				StartCoroutine(DisableTowersOnOutClick());
			}
		}
	}

	IEnumerator DisableTowersOnOutClick()
	{
		StopCoroutine(DisableTowersOnOutClick());
		yield return new WaitForSeconds(.1f);
		DisableTowerButtons();
	}
	
	void SetCosts() {
		if(!init)
		{
			upgrade1Cost.text = choices[0].cost.ToString();
			upgrade2Cost.text = choices[1].cost.ToString();
			upgrade3Cost.text = choices[2].cost.ToString();
		}
		else
		{
			if (currTower != null && currTower.upgradeOptions.Count > 0)
			{
				upgrade1Cost.text = choices[currTower.upgradeOptions[0]].cost.ToString();
				upgrade2Cost.text = choices[currTower.upgradeOptions[0]].cost.ToString();
			}
		}
	}

	void EnableTowerButtons()
	{
		GameObject upgrades = transform.GetChild (0).gameObject;
		if (upgrades != null)
		{
			if (!upgrades.activeSelf)
			{
				upgrades.SetActive(true);
				highlightTower = true;
				upgradesShown = true;
			}
		}
	}

	void DisableTowerButtons() {
		GameObject upgrades = transform.GetChild (0).gameObject;
		if (upgrades != null) {
			if (upgrades.activeSelf)
			{
				upgrades.SetActive (false);
				upgradesShown = false;
			}
		}
	}

	public void CreateNormalTower() {
		Debug.Log ("Create Normal Tower");
		if (!init)
		{
			tower = Instantiate(choices[0].prefab, transform.position, Quaternion.identity) as GameObject;
			manager.AddGold(-1 * choices[0].cost);
			currTower = choices[0];
			tower.transform.parent = this.transform;
			init = true;
		}
		else if (currTower.type == TowerType.regular)
		{
			Destroy(tower);
			tower = Instantiate(choices[currTower.upgradeOptions[0]].prefab, transform.position, Quaternion.identity) as GameObject;
			manager.AddGold(-1 * choices[currTower.upgradeOptions[0]].cost);
			currTower = choices[currTower.upgradeOptions[0]];
			tower.transform.parent = this.transform;
		}
		// Functionality for normal tower construction here
		DisableTowerButtons ();
	}

	public void CreateSlowTower() {
		Debug.Log ("Create Slow Tower");
		if (!init)
		{
			tower = Instantiate(choices[1].prefab, transform.position, Quaternion.identity) as GameObject;
			manager.AddGold(-1 * choices[1].cost);
			currTower = choices[1];
			tower.transform.parent = this.transform;
			init = true;
		}
		else if (currTower.type == TowerType.slow)
		{
			Destroy(tower);
			tower = Instantiate(choices[currTower.upgradeOptions[0]].prefab, transform.position, Quaternion.identity) as GameObject;
			manager.AddGold(-1 * choices[currTower.upgradeOptions[0]].cost);
			currTower = choices[currTower.upgradeOptions[0]];
			tower.transform.parent = this.transform;
		}
		// Functionality for slow tower construction here
		DisableTowerButtons ();
	}

	public void CreateUnitTower()
	{
		Debug.Log("Create Unit Tower");
		if (!init)
		{
			tower = Instantiate(choices[2].prefab, transform.position, Quaternion.identity) as GameObject;
			manager.AddGold(-1 * choices[2].cost);
			currTower = choices[2];
			tower.transform.parent = this.transform;
			init = true;
		}
		// Functionality for slow tower construction here
		DisableTowerButtons();
	}


}
