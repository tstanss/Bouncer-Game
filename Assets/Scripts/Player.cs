using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.Audio;

public class Player : MonoBehaviour {

	public float jumpForce = 10f; 
	public bool grounded = false;
	public AudioClip point;
	public AudioClip gameEnd;

	Rigidbody2D myRigidbody;
	float posx = -5.71f;
	bool gameOver = false;
	AudioSource gameAudio;
	ObstacleController obstacleController;
	GameController gameController;

	// Use this for initialization
	void Start () {
		myRigidbody = transform.GetComponent<Rigidbody2D> ();
		obstacleController = GameObject.FindObjectOfType<ObstacleController> ();
		gameController = GameObject.FindObjectOfType<GameController> ();
		gameAudio = GameObject.FindObjectOfType<AudioSource> ();
	}

	void FixedUpdate () {
		if(Input.GetKey(KeyCode.Space) && grounded && ! gameOver){
			myRigidbody.AddForce (Vector3.up * (jumpForce * myRigidbody.mass * myRigidbody.gravityScale * 20f));

		}
		// Player hits obstacles and the game ends.
		if(transform.position.x < posx && ! gameOver){
			GameOver ();
		}
	}

	void GameOver(){
		gameOver = true;
		obstacleController.GameOver ();
		gameAudio.PlayOneShot (gameEnd);
	}

	// Player hits the ground with tag "Ground"
	void OnCollisionEnter2D (Collision2D other){
		if(other.collider.tag == "Ground"){
			grounded = true;
		}
		if(other.collider.tag == "Enemy"){
			GameOver ();
		}
	}
	// Player stays on the ground
	void OnCollisionStay2D (Collision2D other){
		if(other.collider.tag == "Ground"){
			grounded = true;
		}
	}

	// Player collides with points or enemys
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Point"){
			gameController.ScoreCount ();
			gameAudio.PlayOneShot (point, .5f);
			Destroy (other.gameObject);
		}

		if(other.tag == "Enemy"){
			GameOver ();
			Destroy (other.gameObject);
		}
	}
}
