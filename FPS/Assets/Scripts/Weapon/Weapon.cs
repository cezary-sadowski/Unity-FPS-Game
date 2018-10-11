﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float range = 100f;

    public int bulletsPerMag = 30; //bullets pet each magazine
    public int bulletsLeft; //total fired bullets.
    public int currentBullets; //current bullets in our magazine

    public Transform gunEnd; //the place on gun from which the missiles fly out :) muzzle!

    public float fireRate = 0.25f; //250 miliseconds between shots. 

    float fireTimer;

	// Use this for initialization
	void Start () 
    {
        currentBullets = bulletsPerMag;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }

        if (fireTimer < fireRate)
            fireTimer += Time.deltaTime; //Add into time counter.
	}

    private void Fire()
    {
        if (fireTimer < fireRate) return;

        RaycastHit hit;

        if (Physics.Raycast(gunEnd.position, gunEnd.transform.forward, out hit, range)) //Raycast shooting convention 
        {
            Debug.Log(hit.transform.name + " found!");
        }

        currentBullets--;
        fireTimer = 0.0f; //Reset fire timer before fire.
    }
}
