using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

	public float scrollSpeed = 3f;
	public GameObject[] obstacles;
	public float frequency = 0.6f;
	public Transform obstacleSpawnPoint;
	GameObject currentChild;

	private float counter = 0f;
	private bool gameOver = false;
	private Canvas can;

	// Use this for initialization
	void Start () {
		GenerateRandomObstacles ();
	}
	
	// Scrolling obstacles
	void Update () {
		if (gameOver)
			return;
		
		// Generate objects
		if(counter <= 0f){
			GenerateRandomObstacles ();
		}
		else{
			counter -= Time.deltaTime * frequency;
		}

		for(int i = 0; i < transform.childCount; i++){
			currentChild = transform.GetChild (i).gameObject;
			ScrollObstacle (currentChild);

			if(currentChild.transform.position.x <= -25f){
				Destroy (currentChild);
			}
		}
	}
	// Scrolling current obstacle from right
	void ScrollObstacle(GameObject currentObstacle){
		currentObstacle.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
	}
	// Generate random obstacles. newObstacle is a child of the GameObject (ObstacleController).
	void GenerateRandomObstacles(){
		GameObject newObstacle = Instantiate (obstacles [Random.Range (0, obstacles.Length)], obstacleSpawnPoint.position, Quaternion.identity) as GameObject;
		newObstacle.transform.parent = transform;
		counter = 2f;
	}

	public void GameOver(){
		gameOver = true;
		transform.GetComponent<GameController> ().GameOver (); //Access script atteched to same GameObject as Obstacle controller.
	}

}
