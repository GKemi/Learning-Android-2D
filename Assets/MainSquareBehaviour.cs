using UnityEngine;
using System.Collections.Generic;

public class MainSquareBehaviour : MonoBehaviour {

	public Transform player;
	Vector2 screenSize;
	EdgeCollider2D screenEdges;
	Vector3 cameraDimensions, camDim;
	List<Vector2> vertices = new List<Vector2>();
	public float rotationSpeed = 0;
	public float acceleration = 1;

	void Start(){
		cameraDimensions = new Vector3 (Screen.width, Screen.height, Camera.main.nearClipPlane);
		camDim = Camera.main.ScreenToWorldPoint (cameraDimensions);
		setVertices ();
	}

	void FixedUpdate () {
		Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
		Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint (mousePosition);

		Vector2 mouseDirection = (mousePositionInWorld - player.position);

		if (Input.GetMouseButton (0)) {
			rb.velocity = new Vector3 (mouseDirection.x * 5, mouseDirection.y * 5, 0);

			player.Rotate(new Vector3 (1, 0, -1) * (mouseDirection.x * 5));

			if (rotationSpeed < 40) {
				rotationSpeed += acceleration * Time.deltaTime * 5;
			}


		} else {

			if(rotationSpeed > 0){
				rotationSpeed -= acceleration * Time.deltaTime * 5;
			}

		}

		if (Input.GetMouseButtonUp (0)) {
			rb.AddForce (mouseDirection * (rotationSpeed * 40));
		}

		if (rotationSpeed <= 0) {
			player.Rotate (Vector3.zero);
		} else {
			player.Rotate (new Vector3 (1, 0, -1) * rotationSpeed);
		}

//		Debug.Log (rotationSpeed);

	}

	void setVertices(){
		screenEdges = Camera.main.GetComponent<EdgeCollider2D> ();

		vertices.Add (new Vector2(camDim.x, 0-camDim.y));
		vertices.Add (new Vector2 (0-camDim.x, 0-camDim.y));
		vertices.Add (new Vector2 (0-camDim.x, camDim.y));
		vertices.Add (new Vector2 (camDim.x, camDim.y));

		//Needed to add this again because the last vector gets ignored
		vertices.Add (new Vector2 (camDim.x, 0 - camDim.y));

		screenEdges.points = vertices.ToArray ();
	}
}