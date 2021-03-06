﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float health = 250f;
	public float speed;
	//Stops the ship from being cut off (adjustable outside of script)
	public float padding;
	public GameObject projectile;
	public float projectileSpeed;
	public float fireRate;
	
	//Adding Sound Effects 
	public AudioClip mainWeaponSound;
	
	float xmin;
	float xmax;
	
	void Start(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 RightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		
		xmin = leftMost.x + padding;
		xmax = RightMost.x - padding;
	}
	
	void Fire(){
		//Spawn Lazer from player
		GameObject lazerbeam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		lazerbeam.rigidbody2D.velocity = new Vector3(0, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(mainWeaponSound, transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		//Spawn Lazer from player when space is pressed
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire", 0.00001f, fireRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}
	
		//Move left and Right
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.position += Vector3.left * speed * Time.deltaTime;
		}else if(Input.GetKey(KeyCode.RightArrow)){
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		// Restrict the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y ,transform.position.z );
	}
	
	void Die(){
		Destroy(gameObject);
		LevelManager Man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		Man.LoadLevel("EndScreen");
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		//Debug.Log(collider);
		Projectile lazer = collider.gameObject.GetComponent<Projectile>();
		if(lazer){
			//Debug.Log("Hit By A Lazer");
			health -= lazer.GetDamage();
			lazer.Hit();
			if(health <= 0){
				Die();
			}
		}
	}
}
