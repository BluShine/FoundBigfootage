using UnityEngine;
using System.Collections;

public class Titles : MonoBehaviour {

    public GameObject[] showObjects;
    public float showtime = 2f;
    public string nextScene;

    float timer = 0;
    int count = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw("Submit") != 0)
        {
            Application.LoadLevel(nextScene);
        }

        timer += Time.deltaTime;
        if (timer >= showtime)
        {
            timer = 0;
            if (count < showObjects.Length)
            {
                showObjects[count].SetActive(true);
            }
            count++;
        }
	}
}
