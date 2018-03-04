using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {
	public static int score = 0;
	public Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText.text = "Score: " + Score.score;
	}
	
	// Update is called once per frame
	void Update () {
		score = FruitRotator.count;
		scoreText.text = "Score: " + score;
	}
}
