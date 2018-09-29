using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerpToPlayer : MonoBehaviour {

	public GameObject Player;
	public float SmoothSpeed = 0.125f;
	public Vector3 Offset;
	GameManager GM;
	public bool GameOn;
	// Use this for initialization
	void Start () {

		Player = GameObject.FindGameObjectWithTag ("Player");
		GM = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager>();

	}
	
	// Update is called once per frame
	void LateUpdate () {
		GameOn = GM.GameStart;

		if (GameOn) {
			//// Set Position Camara
			Vector3 desiredPosition = Player.transform.position + Offset;
			Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, SmoothSpeed*Time.deltaTime);
			transform.position = smoothedPosition;

			float Dis = Vector3.Distance(this.transform.position , desiredPosition);

			Debug.Log (Dis);
			if (Dis < 2.0f) {
				SmoothSpeed = 7.0f;
			}

			//// Set Rotation Camera
			Vector3 relativePos = Player.transform.position - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			transform.rotation = Quaternion.Lerp(transform.rotation,rotation, SmoothSpeed * Time.deltaTime);

		}
	}
}
