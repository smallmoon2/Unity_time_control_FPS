using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    
    private float shot;
    private float h;
    private float v;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.Play("Run", -1, 0);    
           
        }

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        anim.SetFloat("h", h);
        anim.SetFloat("v", v);
    }
}
