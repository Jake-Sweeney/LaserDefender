using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {

	public GameObject enemyPrefab;

	public float width = 10f;
	public float height = 5f;
	public float speed = 5.0f;
	public float spawnDelay = 0.5f;
	private bool movingRight = true;
	private float spawnBorderLeft = 0.0f;
	private float spawnBorderRight = 0.0f;

	float xmin;
	float xmax;
	float padding = 0.25f;


	void Start () {
		spawnEnemies ();
		//spawnUntilFull();
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3(0, 0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3(1, 0,distance));
		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding; 
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	// Update is called once per frame
	void Update () {
	//time.delta time accounts for lag in framerate.
		if(movingRight) {
			updateEnemyFormationPosition (speed * Time.deltaTime, 0f);
		} else {
			updateEnemyFormationPosition (-speed * Time.deltaTime, 0f);
		}

		float spawnBorderRight = this.transform.position.x + width/2;
		float spawnBorderLeft = this.transform.position.x - width/2;

		if(spawnBorderLeft < xmin) movingRight = true;
		else if (spawnBorderRight > xmax) movingRight = false;

		if(allMembersDead()) {
			Debug.Log ("Empty formation");
			spawnUntilFull ();
		}
	}

	void updateEnemyFormationPosition(float x, float y) {
		this.transform.position += new Vector3(x, y, 0f);
	}

	bool allMembersDead() {
		//transform is used here for each child in the formation
		foreach(Transform childPositionGameObject in transform) {
			//at least one enemy has a child object i.e. it exists.
			if(childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		//all enemies are dead
		return true;
	}

	Transform nextFreePosition() {
		foreach(Transform childPositionGameObject in transform) {
			//if it doesn't exist
			if(childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;
	}

	void spawnEnemies() {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	void spawnUntilFull() {
		Transform freePosition = nextFreePosition();
		if(freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}

		if(nextFreePosition()) {
			Invoke ("spawnUntilFull", spawnDelay);	
		}	
	}
}
