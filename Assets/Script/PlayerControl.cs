using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float speed = 10.0f;
	public float rotationSpeed = 5.0f;
	public float ForceJump = 10.0f;
	public GameObject Pawn;
	public bool isGrounded;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//rayCastWall ();
		MainController ();
	}

	void MainController()
	{
		float translation = Input.GetAxis("Vertical") * speed;
		float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

		translation *= Time.deltaTime;
		rotation *= Time.deltaTime*15;

		//Pawn.GetComponent<Rigidbody> ().velocity = new Vector3 (rotation*rotationSpeed, Pawn.GetComponent<Rigidbody> ().velocity.y, translation*speed);
		//Pawn.GetComponent<Rigidbody> ().velocity = new Vector3 (0,rotation,0);
		Pawn.transform.Translate(0, 0, translation);
		Pawn.transform.Rotate(0, rotation, 0);

		if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
			Pawn.GetComponent<Rigidbody> ().AddForce (new Vector3 (0,30*ForceJump, 0));
		}
	}

	void rayCastWall()
	{
		// Bit shift the index of the layer (8) to get a bit mask
		int layerMask = 1 << 8;

		// This would cast rays only against colliders in layer 8.
		// But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.

		RaycastHit hit;
		// Does the ray intersect any objects excluding the player layer
		//if (Physics.Raycast(Pawn.transform.position*5, Pawn.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))

		Vector3 T = Pawn.transform.position + (Pawn.transform.TransformDirection(Vector3.forward));
		Vector3 Direc = Pawn.transform.TransformDirection (Vector3.forward)*0.25f;
		float Distance = 1.0f;

		if(Physics.Raycast(T,Direc,out hit))
		{
			Debug.DrawRay(T,Direc*Distance, Color.green);
			Debug.Log("Did Hit"+hit);
		}
		else
		{
			Debug.DrawRay(T,Direc*Distance,Color.red);
			Debug.Log("Did not Hit");
		}


	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == ("Ground"))
		{
			isGrounded = true;
		}
	}
	// This function is a callback for when the collider is no longer in contact with a previously collided object.
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == ("Ground"))
		{
			isGrounded = false;
		}
	}





}
