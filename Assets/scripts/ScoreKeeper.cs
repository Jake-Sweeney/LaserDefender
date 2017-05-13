using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public static int score = 0;
	private Text scoreText;
	public AudioClip scoreUpSound;

	void Start() {
		scoreText = GetComponent<Text>();
	}

	public static void reset() {
		score = 0;
	}

	public void updateScore(int points) {
		Debug.Log ("Got some points");
		score += points;
		scoreText.text = score.ToString ();
		AudioSource.PlayClipAtPoint (scoreUpSound, GameObject.FindObjectOfType<PlayerController>().transform.position);
	}
}
