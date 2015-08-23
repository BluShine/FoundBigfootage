using UnityEngine;
using System.Collections;

public class VideoDude : MonoBehaviour {

    public float speed = 5;
    public VideoScore videoScore;
    public LayerMask raycastMask;
    public GameObject body;
    Player player;
    Animator anim;
    public GameObject camDotPrefab;
    GameObject[] camDots;
    static int MAXDOTS = 10;

	// Use this for initialization
	void Start () {
        anim = body.GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        //go ahead and make some dots ahead of time.
        camDots = new GameObject[MAXDOTS];
        for (int i = 0; i < MAXDOTS; i++)
        {
            camDots[i] = GameObject.Instantiate(camDotPrefab);
            camDots[i].SetActive(false);
        }
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        //MOVEMENT------------------------------------------------------
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

        //FILMING------------------------------------------------------
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position,
            player.transform.position - transform.position, 
            Vector2.Distance(transform.position, player.transform.position),
            raycastMask);
        videoScore.SetScore(hits.Length);
        for (int i = 0; i < MAXDOTS; i++)
        {
            if(i < hits.Length){
                camDots[i].SetActive(true);
                camDots[i].transform.position = hits[i].point;
            }
            else
            {
                camDots[i].SetActive(false);
            }
        }
	}
}
