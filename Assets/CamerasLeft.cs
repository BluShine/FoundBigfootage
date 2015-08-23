using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CamerasLeft : MonoBehaviour {

    Text text;
    public int camerasLeft = 0;
    Player player;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        camerasLeft = 0;
        foreach(GameObject g in player.videoEnemies) {
            if (g.activeSelf)
            {
                camerasLeft++;
            }
        }
        text.text = camerasLeft + " cameras remaining";
	}
}
