using UnityEngine;
using System.Collections;

public class VideoDude : MonoBehaviour {

    public float speed = 5;
    public int spotThreashold = 2;
    public float filmSeconds = 10;
    public VideoScore videoScore;
    public LayerMask raycastMask;
    public GameObject body;
    Player player;
    Animator anim;
    TotalScore score;
    public GameObject camDotPrefab;
    GameObject[] camDots;
    bool filming = false;
    float filmLeft = 1;

    static int MAXDOTS = 10;
    static float DISTANCESCORING = 10;

	// Use this for initialization
	void Start () {
        anim = body.GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        score = FindObjectOfType<TotalScore>();
        filmLeft = filmSeconds;
        //go ahead and make some dots ahead of time.
        camDots = new GameObject[MAXDOTS];
        for (int i = 0; i < MAXDOTS; i++)
        {
            camDots[i] = GameObject.Instantiate(camDotPrefab);
            camDots[i].SetActive(false);
        }
        videoScore.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        //MOVEMENT------------------------------------------------------
        if (filming && filmLeft > 0)
        {
            //walk towards bigfoot
            Vector3 dir = player.transform.position - transform.position;
            dir.Normalize();
            transform.position += dir * Time.deltaTime * speed;

            if (dir.x > 0)
            {
                body.transform.localScale = new Vector3(1, 1, 1);
            }
            if (dir.x < 0)
            {
                body.transform.localScale = new Vector3(-1, 1, 1);
            }

            anim.speed = speed / 2;
        }
        else if (filmLeft <= 0)
        {
            //run away when we're done!
            Vector3 dir = -player.transform.position + transform.position;
            dir.Normalize();
            transform.position += dir * Time.deltaTime * speed * 8;

            if (dir.x > 0)
            {
                body.transform.localScale = new Vector3(1, 1, 1);
            }
            if (dir.x < 0)
            {
                body.transform.localScale = new Vector3(-1, 1, 1);
            }

            anim.speed = speed * 4;
        }
        else
        {
            //patrol or stand still
            anim.speed = 0;
        }

        //FILMING------------------------------------------------------
        //calculate score
        float distance = Vector2.Distance(transform.position, player.transform.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position,
            player.transform.position - transform.position, 
            distance,
            raycastMask);
        int scoreMulti = hits.Length + Mathf.FloorToInt(distance / DISTANCESCORING);
        //shoot footage
        if (!filming && scoreMulti <= spotThreashold)
        {
            filming = true;
            videoScore.gameObject.SetActive(true);
        }
        if (filming && filmLeft > 0)
        {
            //score points
            filmLeft -= Time.fixedDeltaTime;
            videoScore.SetScore(scoreMulti);
            videoScore.SetFilm(filmLeft / filmSeconds);
            score.score += scoreMulti * Time.fixedDeltaTime;
            //place dots
            for (int i = 0; i < MAXDOTS; i++)
            {
                if (i < hits.Length)
                {
                    camDots[i].SetActive(true);
                    camDots[i].transform.position = hits[i].point;
                }
                else
                {
                    camDots[i].SetActive(false);
                }
            }
        }
        else if (filmLeft <= 0)
        {
            foreach (GameObject g in camDots)
            {
                g.SetActive(false);
                videoScore.gameObject.SetActive(false);
            }
        }
	}
}
