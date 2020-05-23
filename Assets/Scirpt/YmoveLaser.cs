﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YmoveLaser : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lr;
    private float laserSpeed= 3.0f;
    private float coolTime = 4.0f;
    private bool stun = false;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.right, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else lr.SetPosition(1, transform.right * 5000);


        if (stun == false)
        {
            if (hit.collider.tag == "Player" || hit.collider.tag == "Power")
            {
                coolTime = 4.0f;
                GameObject.Find("Player").GetComponent<PlayerController>().stun = true;
                stun = true;
            }
        }
        else
        {
            coolTime -= Time.deltaTime;
            if (coolTime < 0.0f)
                stun = false;
        }

        if (transform.position.y < 11.0f)
        {
            laserSpeed = 3.0f;
        }
        else if (transform.position.y >20.0f)
        {
            laserSpeed = -3.0f;
        }
            transform.position += Vector3.up * laserSpeed* Time.deltaTime;
    }
}
