using UnityEngine;
using System.Collections;

public class Lose : MonoBehaviour {

    public string loseLevel;
    bool submitted = false;

	// Use this for initialization
	void Start () {
	
	}
	
	//OnGUI is called even if time is frozen
    void OnGUI() {
        if (gameObject.activeSelf && !submitted)
        {
            Time.timeScale = 0;
            if (Input.GetAxisRaw("Submit") != 0)
            {
                submitted = true;
                Time.timeScale = 1;
                Application.LoadLevel(loseLevel);
            }
        }
    }
}
