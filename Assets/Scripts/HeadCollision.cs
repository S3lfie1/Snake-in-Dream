using UnityEngine;
using System.Collections;

public class HeadCollision : MonoBehaviour {

	private LevelManager levelManager;

	void Start () {
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Rock") || other.gameObject.CompareTag("BodyPart")){
			levelManager.LoadLevel("Lose");
		}
	}

}
