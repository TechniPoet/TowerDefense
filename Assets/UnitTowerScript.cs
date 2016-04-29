using System;
using UnityEngine;
using System.Collections;
using UnityEngineInternal;

public class UnitTowerScript : TowerScript {

    public Vector3 RallyPoint;
    public GameObject UnitPrefab;
    public GameObject SquadPrefab;
    public int SpawnTime;
    private Squad squad;
    private GameManager gameManager;
	public GameObject text;

	void Start () {
	    var squadTransform = Instantiate(SquadPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		squadTransform.SetActive(true);
		squad = squadTransform.GetComponent<Squad>();
		StartCoroutine(WaitForNewRally());
	}
	
	void Update () {
	}

    void OnMouseDown() {
        StartCoroutine(WaitForNewRally());
    }

    IEnumerator WaitForNewRally() {
		StopCoroutine(WaitForNewRally());
		text.SetActive(true);
        Time.timeScale = 0;
        var breakBool = false;
        while (!breakBool) {
            var endTime = Time.realtimeSinceStartup + .1f;
            if (Input.GetMouseButton(0)) {
                var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hits = Physics2D.RaycastAll(ray, Vector2.zero);
                foreach (var hit in hits) {
                    if (hit.collider != null && hit.transform.gameObject.tag == "Path") {
                        RallyPoint = ray;
                        squad.rallyPoint = RallyPoint;
                        breakBool = true;
                        break;
                    }
                }
            }
            while (Time.realtimeSinceStartup < endTime) { //Ensures we don't overuse CPU. can't use WaitForSeconds because timescale=0
                yield return 0;
            }
        }
		text.SetActive(false);
		Time.timeScale = 1;
    }

}
