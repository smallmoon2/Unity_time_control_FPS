using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove_Bullet : MonoBehaviour
{
    public GameObject sparkEffect;
    private void OnCollisionEnter (Collision Coll)
    {
        if(Coll.collider.tag == "BULLET")
        {
            ShowEffect(Coll);
            Destroy(Coll.gameObject);
        }
    }
    private void ShowEffect(Collision coll)
    {
        //첫번째 충돌한 지점
        ContactPoint contact = coll.contacts[0];
        // 회전각도
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal);
        //스파크 효과 생성
        Instantiate(sparkEffect, contact.point, rot);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
