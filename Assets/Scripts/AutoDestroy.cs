using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {
	//Destorys Player Missles once they go out of range
	float ymax;	
	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 upmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
	
		ymax = upmost.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.transform.position.y > (ymax + 0.5f)){
			Destroy(gameObject);
		}
	
	}
}
