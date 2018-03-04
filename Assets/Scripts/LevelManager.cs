using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	AsyncOperation ao;
	public Text loadingText;
	public Button startButton;
	public string levelName;

	public void LoadLevel (string levelName){
		Debug.Log("Loaded Level: "+levelName);
		SceneManager.LoadScene(levelName);
	}

	public void QuitLevel (){
		Debug.Log("Quit");
		Application.Quit();
	}

	public void LoadNextLevel(){
		Application.LoadLevel(levelName);
	}
		
	public void loadingGame(){
		startButton.gameObject.SetActive(false);

		loadingText.gameObject.SetActive(true);
		StartCoroutine(loadGameWithProgress());
	}


	IEnumerator loadGameWithProgress(){
		yield return new WaitForSeconds(1);

		ao = SceneManager.LoadSceneAsync(1);

		ao.allowSceneActivation = false;

		while(!ao.isDone){
			if(ao.progress == 0.9f){ 
				ao.allowSceneActivation = true;
			}

			Debug.Log(ao.progress);
			yield return null;
		}
	}
}
