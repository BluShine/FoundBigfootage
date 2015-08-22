﻿using UnityEngine;
using System.Collections;

public class VideoDude : MonoBehaviour {

    public float speed = 5;
    Player player;
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();
        transform.position += dir * Time.deltaTime * speed;

        if (dir.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (dir.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        anim.speed = speed / 2;
	}
}