using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public float health = 150f;
	public GameObject projectile;
	public float projectileSpeed;
	public float fireRate;
	public int scoreValue = 150;
	
	private ScoreKeeper scoreKeeper;
	
	//Adding Sound Effects 
	public AudioClip emainWeaponSound;
	public AudioClip deathSound;
	
	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	void OnTriggerEnter2D(Collider2D collider){
		//Debug.Log(collider);
		Projectile lazer = collider.gameObject.GetComponent<Projectile>();
		if(lazer){
			//Debug.Log("Hit By A Lazer");
			health -= lazer.GetDamage();
			lazer.Hit();
			if(health <= 0){
				Death();
			}
		}
	}
	
	void Death(){
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Destroy(gameObject);
		scoreKeeper.Score(scoreValue);
	}
	
	void Fire(){
		//Spawn Lazer from Enemy
		GameObject lazerbeam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		lazerbeam.rigidbody2D.velocity = new Vector3(0, -projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(emainWeaponSound, transform.position);
	}
	
	void Update () {
		float probability = Time.deltaTime * fireRate;
		if(Random.value < probability){
			Fire();
		}
	}
}
