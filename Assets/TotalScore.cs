using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour {

    Text text;
    public float score = 0;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Score: " + Mathf.FloorToInt(score);
	}
}
