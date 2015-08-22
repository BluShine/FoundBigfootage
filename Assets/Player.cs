using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed = 50;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (horiz < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        if (horiz > 0)
            transform.localScale = new Vector3(1, 1, 1);

        transform.position += new Vector3(speed * Time.deltaTime * horiz,
            speed * Time.deltaTime * vert, 0);
	}
}
