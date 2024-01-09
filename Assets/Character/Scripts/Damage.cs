using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    private const string bulletTag = "BULLET";
    private float initHp= 100.0f;

    private Color currColor;
    private readonly Color initColor = new Vector4(0, 1.0f, 0.0f, 1.0f);
    public float currHp;

    public Image bloodScreen;

    public Image hpBar;
    // Start is called before the first frame update
    void Start()
    {
        bloodScreen.color = Color.clear;
        currHp = initHp;
        hpBar.color = initColor;
        currColor = initColor;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == bulletTag)
        {
            Destroy(coll.gameObject);
            StartCoroutine(ShowBloodScreen());
            currHp -= 5.0f;
            Debug.Log("PlayerHP = " + currHp.ToString());
            DisplayHpbar();

            if(currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }
    IEnumerator ShowBloodScreen()
    {
        bloodScreen.color = new Color(1, 0, 0, UnityEngine.Random.Range(0.2f, 0.3f));
        yield return new WaitForSeconds(0.1f);
        bloodScreen.color = Color.clear;
    }

    private void PlayerDie()
    {
        Debug.Log("PlayerDie!!");
    }
    void DisplayHpbar()
    {
        if ((currHp / initHp) > 0.5f)
            currColor.r = (1 - (currHp / initHp)) * 2.0f;
        else
            currColor.g = (currHp / initHp) * 2.0f;
        hpBar.color = currColor;
        hpBar.fillAmount = (currHp / initHp);
    }
}
