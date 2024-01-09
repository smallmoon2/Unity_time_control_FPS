﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Ctrl : MonoBehaviour
{
    public float damage = 10.0f;
    public float speed = 5000.0f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
