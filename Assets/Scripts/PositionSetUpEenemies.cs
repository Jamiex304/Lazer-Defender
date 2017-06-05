using UnityEngine;
using System.Collections;

public class PositionSetUpEenemies : MonoBehaviour {

	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position, 1);
	}
}
