using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerSfx
{
    public AudioClip[] fire;
    public AudioClip[] reload;

}
public class Fire_Ctrl : MonoBehaviour
{
    

    public enum WeaponType
    {
        RIFLE = 0,
        SHOTGUN
    }
    public WeaponType currWeapon = WeaponType.RIFLE;

    public GameObject bullet;
    public GameObject bullet_2;
    public Transform firePos;
    public ParticleSystem Shot_Effect;

    public PlayerSfx playerSfx;

    private AudioSource audio_1;

    private Shake shake;
    

    // Start is called before the first frame update
    void Start()
    {
        audio_1 = GetComponent<AudioSource>();

        shake = GameObject.Find("CameraRig").GetComponent<Shake>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Fire();
        }

    }
    void Fire()
    {
        StartCoroutine(shake.ShakeCamera());
        Instantiate(bullet, firePos.position, firePos.rotation);
        Shot_Effect.Play();

        FireSfx();
        Destroy(GameObject.Find(bullet_2.name + "(Clone)"), 3.0f);
    }

    private void FireSfx()
    {
        var _sfx = playerSfx.fire[(int)currWeapon];
        audio_1.PlayOneShot(_sfx, 0.5f);
    }
}
