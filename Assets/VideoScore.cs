using UnityEngine;
using System.Collections;

public class VideoScore : MonoBehaviour {

    public TextMesh multiplier;
    public TextMesh qualityName;

    static string[] QNAMES = {"GREAT", "GOOD", "DECENT", "AVERAGE", "NOT GREAT", 
                                 "MEDIOCRE", "POOR", "BAD", "TRASH", "GARBAGE", 
                                 "POTATO"};
    static float QINTERVAL = .2f;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetScore(float s)
    {
        multiplier.text = "X" + s;
        if (s < QNAMES.Length)
        {
            qualityName.text = QNAMES[Mathf.FloorToInt(s)];
        }
        else
        {
            qualityName.text = QNAMES[QNAMES.Length - 1];
        }
    }
}
