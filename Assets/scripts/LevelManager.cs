using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name) {
		Debug.Log(name);
		SceneManager.LoadScene(name);
	}

	public void QuitRequest() {
		Debug.Log ("Quit");
		Application.Quit();
	}

	public void LoadNextLevel() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void brickDestroyed() {
		/*if(Brick.breakableCount <= 0) {
			LoadNextLevel();
		}*/
	}
}
