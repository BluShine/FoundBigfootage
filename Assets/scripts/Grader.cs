using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Grader : MonoBehaviour {

    MusicPlayer p;

	// Use this for initialization
	void Start () {
        string gradeString = "";
        p = FindObjectOfType<MusicPlayer>();
        foreach (string s in p.letterGrades)
        {
            gradeString += s + "\n";
        }
        GetComponent<TextMesh>().text = gradeString;
	}
	
	// Update is called once per frame
	void Update () {
        p.setVolume(1);
        p.setSpotted(1);
	}
}
