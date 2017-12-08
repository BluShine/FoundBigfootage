using UnityEngine;
using System.Collections;

public class VideoScore : MonoBehaviour {

    public TextMesh multiplier;
    public TextMesh qualityName;
    public GameObject filmBar;
    Vector3 filmBarSize;

    static string[] QNAMES = {"GREAT", "GOOD", "DECENT", "AVERAGE", "NOT GREAT", 
                                 "MEDIOCRE", "POOR", "BAD", "TRASH", "GARBAGE", 
                                 "POTATO"};
    static float QINTERVAL = .2f;

	// Use this for initialization
	void Start () {
        filmBarSize = Vector3.one;
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

    public void SetFilm(float f)
    {
        Debug.Log(filmBarSize.x);
        filmBar.transform.localScale = new Vector3(filmBarSize.x * f, filmBarSize.y, filmBarSize.z);
    }
}
