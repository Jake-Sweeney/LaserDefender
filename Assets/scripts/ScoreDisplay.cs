using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text scoreField = GetComponent<Text>();
		scoreField.text = ScoreKeeper.score.ToString ();
		ScoreKeeper.reset ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
