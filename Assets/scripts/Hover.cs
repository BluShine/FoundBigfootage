using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {

    public float hoverAmount = 10f;
    public float hoverSpeed = 1;
    float hovery = 0;
    Vector3 startPos;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        hovery += Time.deltaTime * hoverSpeed;
        hovery %= Mathf.PI * 2;
        transform.position = startPos + new Vector3(0, Mathf.Sin(hovery), 0);
	}
}
