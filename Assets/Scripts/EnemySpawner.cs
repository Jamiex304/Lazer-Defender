using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	//Public Object
	public GameObject enemyPrefab;
	public float width;
	public float height;
	public float speed;
	
	private bool movingRight = true;
	private float xmin;
	private float xmax;

	// Use this for initialization
	void Start () {
	//Getting the bounds of the screen
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 RightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		
		xmin = leftMost.x;
		xmax = RightMost.x;
	//Spawn Enemy at Start
		foreach(Transform child in transform){
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			//Keep scene clean
			enemy.transform.parent = child;
		}
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width,height,0));
	}
	
	// Update is called once per frame
	void Update () {
		if(movingRight){//Move Right
			transform.position += Vector3.right * speed * Time.deltaTime;
		}else{//Move Left
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		// Move back and fourth within the playspace
		float rightEdgeofFormation = transform.position.x + (0.5f*width);
		float leftEdgeofFormation = transform.position.x - (0.5f*width);
		
		if(leftEdgeofFormation < xmin){
			movingRight = true;
		}else if (rightEdgeofFormation > xmax){
			movingRight = false;
		}
	}
}
