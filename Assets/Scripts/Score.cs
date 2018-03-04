using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public static int score = 0;
	public Text scoreText;
	void Start () {
		GameObject.DontDestroyOnLoad(gameObject);
	}
		
	void Update () {
		score = FruitRotator.count;
		scoreText.text = "Score: " + score;
	}
}

