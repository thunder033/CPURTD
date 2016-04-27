using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    Camera[] cameras;
    int curCameraIndex;

    // Use this for initialization
    void Start () {
        cameras = FindObjectsOfType<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        //switch camera on space bar press
        if (Input.GetKeyDown("space")) {
            if (curCameraIndex++ >= cameras.Length) {
                curCameraIndex = 0;
            }

            for (int c = 0; c < cameras.Length; c++) {
                cameras[c].enabled = false;
                cameras[c].tag = "Untagged";

                if (c == curCameraIndex) {
                    cameras[c].enabled = true;
                    cameras[c].tag = "MainCamera";
                }
            }
        }
    }
}
