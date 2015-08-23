using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;

public class Player : MonoBehaviour {

    public float speed = 50;

    public GameObject model;
    Animator anim;
    Rigidbody2D body;
    MusicPlayer music;
    public Blur blur;
    public GameObject winText;
    public GameObject loseText;
    [HideInInspector]
    public List<GameObject> videoEnemies;

    static int LOWESTBLUR = 2;
    static int HIGHESTBLUR = 10;

	// Use this for initialization
	void Start () {
        anim = model.GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        music = FindObjectOfType<MusicPlayer>();
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
        int lowestScore = HIGHESTBLUR;
        float filmed = 0;
        foreach(GameObject g in videoEnemies){
            if (g.activeSelf)
            {
                VideoDude d = g.GetComponent<VideoDude>();
                if (d != null)
                {
                    if(d.score < lowestScore)
                        lowestScore = d.score;

                    if (d.filming)
                    {
                        filmed = 1;
                    }
                }
            }
        }
        if (lowestScore == 0)
            lose();
        music.setSpotted(filmed);
        lowestScore = Mathf.Max(LOWESTBLUR, lowestScore);
        blur.iterations = Mathf.Max(LOWESTBLUR, lowestScore);

        //music volume
        float volume = ((float) lowestScore - (float)LOWESTBLUR) / ((float)HIGHESTBLUR - (float)LOWESTBLUR);
        music.setVolume(1 - volume);
	}

    public void lose()
    {
        loseText.SetActive(true);
    }

    public void win()
    {
        winText.SetActive(true);
    }
}
