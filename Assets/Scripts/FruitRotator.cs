using UnityEngine;
using System.Collections;

public class FruitRotator : MonoBehaviour {
	public GameObject snakeObject;
	public Transform particles;
	private int minDistance = -8;
	private int maxDistance = 8;
	private AudioSource audioSrc;
	private Vector3 newPosition;
	public static int count;

	void Start () {
		count = 0;
		audioSrc = GetComponent<AudioSource>();
		particles.GetComponent<ParticleSystem>().enableEmission = false;
		newPosition = new Vector3(Random.Range(minDistance, maxDistance), 0.5f, Random.Range(minDistance, maxDistance));
	}
	

	void Update () {
		transform.Rotate(new Vector3(0, 120, 0) * Time.deltaTime);
	}


	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Head")){
			snakeObject.GetComponent<TestMovement>().addBodyPart();
			particles.GetComponent<ParticleSystem>().enableEmission = false;

			StartCoroutine(stopParticles());

			audioSrc.Play();

			StartCoroutine(getNewPosition());

			this.gameObject.SetActive(false);
			Debug.Log("Triggred");

			this.gameObject.transform.position = newPosition;
			this.gameObject.SetActive(true);
			count++;
		}
	}
		
	IEnumerator getNewPosition(){
		while(Physics.CheckSphere(newPosition, 1.0f)){
			newPosition = new Vector3(Random.Range(minDistance, maxDistance), 0.8f, Random.Range(minDistance, maxDistance));
			yield return null;
		}
	}
		
	IEnumerator stopParticles(){
		yield return new WaitForSeconds(0.4f);
		particles.GetComponent<ParticleSystem>().enableEmission = false;
	}
}
