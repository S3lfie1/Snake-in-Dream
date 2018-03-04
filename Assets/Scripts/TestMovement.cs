using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TestMovement : MonoBehaviour {

	private LevelManager levelManager;

	public List<Transform> bodyParts = new List<Transform>();
	public float minDistance = 0.015f;
	public float speed = 1.0f;
	public float rotationSpeed = 50.0f;
	public int startBodySize = 1;
	public GameObject bodyPartPrefab;

	public GameObject rockPref;
	public GameObject rockPref2;
	public GameObject rockPref3;
	public GameObject grassPrefab;
	public GameObject grassPrefab2;

	public int noOfRocks = 2;
	public int noOfRocks2 = 5;
	public int noOfRocks3 = 3;
	public int noOfGrass = 50;
	public int noOfGrass2 = 50;

	private float distance;
	private Transform currentBodyPart;
	private Transform prevBodyPart;

	private Quaternion startingRotation;
	private float angel = 90;
	private float fTurnRate = 90.0f;

	private int boardMinDistance = -8;
	private int boardMaxDistance = 8;

	public Button RightTouchButton;
	public Button LeftTouchButton;

	public AudioClip moveSound;
	AudioSource audioSource;

	//start function
	void Start(){
		audioSource = GetComponent<AudioSource>();

		if(bodyParts.Count != 0)
			startingRotation = bodyParts[0].rotation;

		for(int i = 0; i < startBodySize - 1; i++){
			addBodyPart();
		}


		for(int j=0; j<noOfRocks; j++){
			Vector3 newPosition = new Vector3(Random.Range(boardMinDistance, boardMaxDistance), 0.3f, Random.Range(boardMinDistance, boardMaxDistance));
			if(rockPref != null) {
				GameObject go = Instantiate(rockPref, newPosition, Quaternion.identity) as GameObject;
			}
		}

		for(int j=0; j<noOfRocks2; j++){
			Vector3 newPosition = new Vector3(Random.Range(boardMinDistance, boardMaxDistance), 0.5f, Random.Range(boardMinDistance, boardMaxDistance));
			if(rockPref2 != null) {
				GameObject go = Instantiate(rockPref2, newPosition, Quaternion.identity) as GameObject;
			}
		}

		for(int j=0; j<noOfRocks2; j++){
			Vector3 newPosition = new Vector3(Random.Range(boardMinDistance, boardMaxDistance), 0.5f, Random.Range(boardMinDistance, boardMaxDistance));
			if(rockPref3 != null) {
				GameObject go = Instantiate(rockPref3, newPosition, Quaternion.identity) as GameObject;
			}
		}


		for(int k=0; k<noOfGrass; k++){
			Vector3 newPosition = new Vector3(Random.Range(boardMinDistance, boardMaxDistance), 0.5f, Random.Range(boardMinDistance, boardMaxDistance));
			if(grassPrefab != null) {
				GameObject go = Instantiate(grassPrefab, newPosition, Quaternion.Euler(new Vector3(-90,0,0))) as GameObject;
			}
		}


		for(int l=0; l<noOfGrass2; l++){
			Vector3 newPosition = new Vector3(Random.Range(boardMinDistance, boardMaxDistance), 0.5f, Random.Range(boardMinDistance, boardMaxDistance));
			if(grassPrefab2 != null) {
				GameObject go = Instantiate(grassPrefab2, newPosition, Quaternion.Euler(new Vector3(-90,0,0))) as GameObject;
			}
		}
			
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

	}

	void Update () {
		move();
	}

	public void RightTouched()
	{

		audioSource.PlayOneShot(moveSound, 0.4F);
		bodyParts [0].position = new Vector3 (bodyParts [0].position.x, 0.5f, bodyParts [0].position.z);

		StopAllCoroutines ();
		StartCoroutine (Rotate (angel));
		angel += 90;
		bodyParts [0].Rotate (-Vector3.forward * fTurnRate * Time.deltaTime);

	}

	public void LeftTouched()
	{
		audioSource.PlayOneShot(moveSound, 0.4F);
		bodyParts[0].position = new Vector3(bodyParts[0].position.x, 0.5f, bodyParts[0].position.z);

		StopAllCoroutines();
		StartCoroutine(Rotate(angel-180));
		angel-=90;
		bodyParts[0].Rotate (Vector3.forward * fTurnRate * Time.deltaTime);

	}


	//move function
	public void move(){

		float currentSpeed = speed;

		if(Input.GetKey(KeyCode.UpArrow)) currentSpeed *= 2;

		if(bodyParts.Count != 0)
			bodyParts[0].Translate(bodyParts[0].forward * currentSpeed * Time.smoothDeltaTime, Space.World);

		if(bodyParts.Count != 0)
			bodyParts[0].position = bodyParts[0].position + bodyParts[0].forward * 2.0f * Time.deltaTime;

		for(int i = 1; i<bodyParts.Count; i++){
			currentBodyPart = bodyParts[i];
			prevBodyPart = bodyParts[i-1];

			distance = Vector3.Distance(prevBodyPart.position, currentBodyPart.position);

			Vector3 newPosition = prevBodyPart.position;

			newPosition.y = bodyParts[0].position.y;

			float dTime = Time.deltaTime * (distance/minDistance) * currentSpeed;

			if(dTime > 0.5f) dTime = 0.5f;

			currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newPosition, dTime);
			currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, prevBodyPart.rotation, dTime);
		}
	}

	public void addBodyPart(){
		if(bodyParts.Count != 0){
			speed += 0.03f;
			Transform newPart = (Instantiate(bodyPartPrefab, bodyParts[bodyParts.Count - 1].position, bodyParts[bodyParts.Count -1].rotation) as GameObject).transform;
			newPart.SetParent(transform);
			bodyParts.Add(newPart);
		}
	}
		
	IEnumerator Rotate(float rotationAmount){
		Quaternion finalRotation = Quaternion.Euler( 0, rotationAmount, 0 ) * startingRotation;
		while(bodyParts[0].rotation != finalRotation){
			bodyParts[0].rotation = Quaternion.Lerp(bodyParts[0].rotation, finalRotation, Time.deltaTime*rotationSpeed);
			yield return null;
		}
	}

}
