using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public float health = 150f;
	public GameObject projectile;
	public float laserbeamSpeed = 5;
	public float fireRate = 0.2f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	public AudioClip laserSound;
	public AudioClip destroyedSound;

	private ScoreKeeper scoreKeeper;

	void Start() {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}

	void OnTriggerEnter2D (Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.getDamage();
			missile.hit();
			if(health <= 0) {
				die();
			}
		}
	}

	void die() {
		AudioSource.PlayClipAtPoint (destroyedSound, transform.position);
		scoreKeeper.updateScore(scoreValue);
		Destroy (gameObject);
	}

	void Update() {
		float probabilty = shotsPerSecond * Time.deltaTime;
		//random.value is between zero and 1;
		if(Random.value  < probabilty) {
			fire();
		}
	}

	void fire() {
		//offset no longer needed as we have added layers to the game and removed collision detection for certain layers.
		//Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
		GameObject laserbeamSpawn = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		laserbeamSpawn.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -laserbeamSpeed, 0);
		AudioSource.PlayClipAtPoint (laserSound, transform.position);
	}
}
