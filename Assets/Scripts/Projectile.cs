using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float Damage = 100f;
	
	public float GetDamage(){
		return Damage;
	}
	//Destory Lazer if it hits an object
	public void Hit(){
		Destroy(gameObject);
	}
}
