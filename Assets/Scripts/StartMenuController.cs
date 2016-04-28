using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMenuController : MonoBehaviour {

	public Button playText;
	public Button exitText;
	
	void Start () {
		playText = playText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
	}

	public void PlayGame() {
		// NOTE : "TowerTestScene" should be replaced with our actual game scene
		Application.LoadLevel("MasterScene");
	}

	public void ExitGame() {
		Application.Quit ();
	}
}
