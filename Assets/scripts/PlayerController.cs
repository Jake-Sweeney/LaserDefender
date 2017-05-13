using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public float padding = 0.5f;
	public GameObject laserbeam;
	public float laserbeamSpeed = 5;
	public float fireRate = 0.2f;
	public float health = 250f;
	public AudioClip laserSound;

	float xmin;
	float xmax;

	// Use this for initialization
	void Start () {
		//distance from camera to object i.e. player
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3(0, 0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3(1, 0,distance));
		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding; 
	}
	
	// Update is called once per frame
	void Update () {
		//Time.deltaTime compensates for slow rendering
		if(Input.GetKey(KeyCode.LeftArrow)) {
			updatePlayerPosition (-speed * Time.deltaTime, 0f);
			//similarly transform.position += Vector3.left * speed * Time.deltaTime;
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			updatePlayerPosition (speed * Time.deltaTime, 0f);
		}
		if(Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating ("fire", 0.0001f, fireRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("fire");
		}

		//keep player in gamespace
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);

		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}

	void updatePlayerPosition(float x, float y) {
		this.transform.position += new Vector3(x, y, 0f);
	}

	void fire() {
		Vector3 offset = new Vector3(0,1,0);
		GameObject laserbeamSpawn = Instantiate(laserbeam, transform.position + offset, Quaternion.identity) as GameObject;
		laserbeamSpawn.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserbeamSpeed, 0);
		AudioSource.PlayClipAtPoint (laserSound, transform.position);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Projectile projectile = collider.gameObject.GetComponent<Projectile> ();
		if (projectile) {
			health -= projectile.getDamage();
			projectile.hit();
			if(health <= 0) {
				die();
			}
		}
	}

	void die() {
		LevelManager levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		levelManager.LoadLevel ("Win");
		Destroy (gameObject);
	}
}
