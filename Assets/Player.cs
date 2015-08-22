using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Player : MonoBehaviour {

    public float speed = 50;

    public GameObject model;
    Animator anim;
    Blur blur;

	// Use this for initialization
	void Start () {
        anim = model.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        //BIGFOOT MOVEMENT-------------------------------------------------------
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        Vector2 moveDir = new Vector2(horiz, vert);
        if (moveDir.magnitude > 1)
            moveDir = moveDir.normalized;
        //set animation speed
        anim.speed = moveDir.magnitude;
        //flip left or right
        if (horiz < 0)
            model.transform.localScale = new Vector3(-1, 1, 1);
        if (horiz > 0)
            model.transform.localScale = new Vector3(1, 1, 1);
        //move position
        transform.position += new Vector3(speed * Time.deltaTime * moveDir.x,
            speed * Time.deltaTime * moveDir.y, 0);

        //BIGFOOT BLURRINESS-------------------------------------------------------
        
	}
}
