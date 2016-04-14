using UnityEngine;
using System.Collections;

public class TopDownCam : MonoBehaviour {

    bool forward = false;
    bool backward = false;
    bool left = false;
    bool right = false;

	CameraSettingsVariables me;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		me = GameManager._CamSettings;
		forward = Input.GetKey(KeyCode.W);
        backward = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);

		float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (forward)
        {
			this.transform.position = transform.position + (Vector3.up * Time.deltaTime * me.moveSpeed);
        }
        if (backward)
        {
            this.transform.position = transform.position + (Vector3.down * Time.deltaTime * me.moveSpeed);
        }
        if (left)
        {
            this.transform.position = transform.position + (Vector3.left * Time.deltaTime * me.moveSpeed);
        }
        if (right)
        {
            this.transform.position = transform.position + (Vector3.right * Time.deltaTime * me.moveSpeed);
        }
		float curX = Mathf.Clamp(transform.position.x, -me.xBoundry, me.xBoundry);
		float curY = Mathf.Clamp(transform.position.y, -me.yBoundry, me.yBoundry);
		transform.position = new Vector3(curX, curY, transform.position.z);
		float newSize = Camera.main.orthographicSize + (-scroll * me.zoomSpeed);

		newSize = Mathf.Clamp(newSize, me.lowZoomY, me.highZoomY);
		Camera.main.orthographicSize = newSize;

    }
}
