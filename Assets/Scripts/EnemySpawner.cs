using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	//Public Object
	public GameObject enemyPrefab;
	public float width;
	public float height;
	public float speed;
	public float spawnDelay;
	
	private bool movingRight = true;
	private float xmin;
	private float xmax;

	//Custom Methods
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width,height,0));
	}
	
	bool AllMembersDead(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}
	
	Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount == 0){
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	void SpawnEnemyFormation(){
		//Spawn Enemy at Start
		foreach(Transform child in transform){
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			//Keep scene clean
			enemy.transform.parent = child;
		}
	}
	
	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if(freePosition){
			GameObject enemy = Instantiate(enemyPrefab, freePosition.transform.position, Quaternion.identity) as GameObject;
			//Keep scene clean
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition()){
			Invoke("SpawnUntilFull", spawnDelay);
		}
	}
	
	// Use this for initialization
	void Start () {
	//Getting the bounds of the screen
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 RightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		
		xmin = leftMost.x;
		xmax = RightMost.x;
		
		//Spawn Enemy Formation at Start
		SpawnUntilFull();
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
		if(leftEdgeofFormation < xmin || rightEdgeofFormation > xmax){
			movingRight = !movingRight;
		}
		//Checking if all current enemies are destoryed
		if(AllMembersDead()){
			//Debug.Log("Formation Dead");
			//Respawn Enemy Formation
			SpawnUntilFull();
		}
	}
}
