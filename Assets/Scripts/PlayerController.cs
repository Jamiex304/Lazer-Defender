using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed;
	//Stops the ship from being cut off (adjustable outside of script)
	public float padding;
	
	float xmin;
	float xmax;
	
	void Start(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 RightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		
		xmin = leftMost.x + padding;
		xmax = RightMost.x - padding;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.position += Vector3.left * speed * Time.deltaTime;
		}else if(Input.GetKey(KeyCode.RightArrow)){
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		// Restrict the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y ,transform.position.z );
	}
}
