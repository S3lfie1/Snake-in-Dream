using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {

	public Text scoreText;

	void Start () {
		scoreText.text = "Score: " + Score.score;
	}
		
	void Update () {

	}
}
