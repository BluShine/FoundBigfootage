using UnityEngine;
using System.Collections;

public class VideoDrone : VideoEnemy {

    public float speed = 7;
    public float accel = 2;
    public int spotThreashold = 2;
    public float filmSeconds = 10;
    public VideoScore videoScore;
    public LayerMask raycastMask;
    Rigidbody2D body;
    Player player;
    TotalScore totalScore;
    public GameObject camDotPrefab;
    GameObject[] camDots;
    float filmLeft = 1;
    float runTime = 1;

    static int MAXDOTS = 10;
    static float DISTANCESCORING = 10;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>();
        totalScore = FindObjectOfType<TotalScore>();
        filmLeft = filmSeconds;
        //go ahead and make some dots ahead of time.
        camDots = new GameObject[MAXDOTS];
        for (int i = 0; i < MAXDOTS; i++)
        {
            camDots[i] = GameObject.Instantiate(camDotPrefab);
            camDots[i].SetActive(false);
        }
        videoScore.gameObject.SetActive(false);
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //MOVEMENT------------------------------------------------------
        if (filming && filmLeft > 0)
        {
            //fly towards bigfoot
            Vector3 dir = player.transform.position - transform.position;
            dir.Normalize();
            dir += new Vector3(Random.value * .02f, Random.value * .02f, 0);
            Vector2 accelDir = accel * dir * Time.fixedDeltaTime;
            body.velocity += accelDir;
            if (body.velocity.magnitude > speed)
            {
                body.velocity = body.velocity.normalized * speed;
            }
        }
        else if (filmLeft <= 0)
        {
            body.velocity = Vector2.zero;
            body.isKinematic = true;
            //run away when we're done!
            Vector3 dir = -player.transform.position + transform.position;
            dir.Normalize();
            transform.position += dir * Time.deltaTime * speed * 3;

            runTime -= Time.fixedDeltaTime;
            if (runTime <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            //do nothing
        }

        //FILMING------------------------------------------------------
        //calculate score
        float distance = Vector2.Distance(transform.position, player.transform.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position,
            player.transform.position - transform.position,
            distance,
            raycastMask);
        score = hits.Length + Mathf.FloorToInt(distance / DISTANCESCORING);
        //shoot footage
        if (!filming && score <= spotThreashold)
        {
            filming = true;
            videoScore.gameObject.SetActive(true);
        }
        if (filming && filmLeft > 0)
        {
            //score points
            filmLeft -= Time.fixedDeltaTime;
            videoScore.SetScore(score);
            videoScore.SetFilm(filmLeft / filmSeconds);
            totalScore.score += score * Time.fixedDeltaTime;
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