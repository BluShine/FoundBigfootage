using UnityEngine;
using System.Collections;

public class Lose : MonoBehaviour {

    public string loseLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	//OnGUI is called even if time is frozen
    void OnGUI() {
        if (gameObject.activeSelf)
        {
            Time.timeScale = 0;
            if (Input.GetAxisRaw("Submit") != 0)
            {
                Time.timeScale = 1;
                Application.LoadLevel(loseLevel);
            }
        }
    }
}
