using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CamerasLeft : MonoBehaviour {

    Text text;
    public int camerasLeft = 0;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        camerasLeft = GameObject.FindGameObjectsWithTag("VideoEnemy").Length;
        text.text = camerasLeft + " cameras remaining";
	}
}
