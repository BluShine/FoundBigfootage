using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;

public class Player : MonoBehaviour {

    public float speed = 50;

    public GameObject model;
    Animator anim;
    Rigidbody2D body;
    public Blur blur;

    public List<GameObject> videoEnemies;

    static int LOWESTBLUR = 2;
    static float BLURINTERVAL = 5;
    static float MAXDIST = 50;

	// Use this for initialization
	void Start () {
        anim = model.GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("VideoEnemy")) {
            videoEnemies.Add(g);
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {

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
        body.velocity = new Vector3(speed * moveDir.x, speed * moveDir.y, 0);

        //BIGFOOT BLURRINESS-------------------------------------------------------
        float closetDist = MAXDIST;
        foreach(GameObject g in videoEnemies){
            float d = Vector3.Distance(g.transform.position, transform.position);
            if(d < closetDist) {
                closetDist = d;
            }
        }

        int blurLevel = Mathf.FloorToInt(closetDist / BLURINTERVAL);
        blurLevel = Mathf.Max(LOWESTBLUR, blurLevel);
        blur.iterations = blurLevel;
	}
}
