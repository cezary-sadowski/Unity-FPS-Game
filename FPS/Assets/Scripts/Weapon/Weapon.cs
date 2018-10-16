using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    private Animator _Animator;
    private AudioSource _AudioSource; //good practice to add _ before name of private field

    private bool _isReloading;

    public float range = 100f;

    public int bulletsPerMag = 30; //bullets pet each magazine
    public int bulletsAll = 200; //total fired bullets.
    public int currentBullets; //current bullets in our magazine

    public Transform gunEnd; //the place on gun from which the missiles fly out :) muzzle!
    public ParticleSystem gunEndFlash; //muzzle flash.
    public AudioClip shootSound;

    public float fireRate = 0.25f; //250 miliseconds between shots. 

    float fireTimer;

	// Use this for initialization
	void Start () 
    {
        _Animator = GetComponent<Animator>();
        _AudioSource = GetComponent<AudioSource>();

        currentBullets = bulletsPerMag;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetButton("Fire1"))
        {
            if (currentBullets > 0)
                Fire();

            else
                DoReload();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentBullets < bulletsPerMag && bulletsAll > 0)
            DoReload();
        }

        if (fireTimer < fireRate)
            fireTimer += Time.deltaTime; //Add into time counter.
	}

    private void FixedUpdate() 
    {
        AnimatorStateInfo info = _Animator.GetCurrentAnimatorStateInfo(0);

        _isReloading = info.IsName("Reload");

        //if (info.IsName("Fire")) _Animator.SetBool("Fire", false); //reset loop with fire animation.
    }

    private void Fire()
    {
        if (fireTimer < fireRate || currentBullets <= 0 || _isReloading) 
            return; //if this scenario - return (exit function).

        RaycastHit hit;

        if (Physics.Raycast(gunEnd.position, gunEnd.transform.forward, out hit, range)) //Raycast shooting convention 
        {
            Debug.Log(hit.transform.name + " found!");
        }

        _Animator.CrossFadeInFixedTime("Fire", 0.01f); //fixed play the fire animation
        //animator.SetBool("Fire", true);

        gunEndFlash.Play();
        PlayShootSound();

        currentBullets--;
        fireTimer = 0.0f; //Reset fire timer before fire.
    }

    public void Reload()
    {
        if (bulletsAll <= 0)
            return;

        int bulletsToLoad = bulletsPerMag - currentBullets; //for exmpl: currentBullets is 24 and perMag is 30 so I have to load 6 bullets.

        int bulletsToDeduct = (bulletsAll >= bulletsToLoad) ? bulletsToLoad : bulletsAll; //if it is enough bullets to load from all its ok otherwise 

        bulletsAll -= bulletsToDeduct;
        currentBullets += bulletsToDeduct;
    }

    private void DoReload()
    {
        AnimatorStateInfo info = _Animator.GetCurrentAnimatorStateInfo(0);

        if (_isReloading)
            return;
        
        _Animator.CrossFadeInFixedTime("Reload", 0.01f);
    }

    private void PlayShootSound()
    {
        _AudioSource.clip = shootSound;
        _AudioSource.Play();
    }
}
