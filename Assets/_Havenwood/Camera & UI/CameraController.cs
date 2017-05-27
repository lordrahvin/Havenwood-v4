using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField] Transform cameraFocus;
	Transform player;
	float zoomAmount;
	float cameraHeight;
	float zoom;
	Vector3 playerFloatingRefPoint;
	Vector3 VectorFromCamToPlayerRefPoint;
	Vector3 CamFollowPoint;
	Vector3 rightCamPoint;
	Vector3 leftCamPoint;
	Vector3 camPoint;
	Vector3 oldCameraPos;
	Vector3 newCameraPos;
	Vector3 camFollowVector;
	//Vector3 currentVelocity = Vector3.zero;
	const float zoomFactor = 15f;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		zoomAmount = Camera.main.fieldOfView;
		cameraHeight = transform.position.y;
		playerFloatingRefPoint = new Vector3(player.position.x, cameraHeight, player.position.z);
		VectorFromCamToPlayerRefPoint = playerFloatingRefPoint - transform.position;
		CamFollowPoint = playerFloatingRefPoint - VectorFromCamToPlayerRefPoint;
		camFollowVector = VectorFromCamToPlayerRefPoint;
	}

	private void Update()
	{
		rightCamPoint = Quaternion.Euler(0f, 1f, 0f) * VectorFromCamToPlayerRefPoint;
		leftCamPoint = Quaternion.Euler(0f, -1f, 0f) * VectorFromCamToPlayerRefPoint;
		rightCamPoint = playerFloatingRefPoint - rightCamPoint;
		leftCamPoint = playerFloatingRefPoint - leftCamPoint;
		camPoint = CamFollowPoint;
		zoom = 0.0f;
		if (Input.GetKey(KeyCode.N)) { camPoint = leftCamPoint; }
		if (Input.GetKey(KeyCode.M)) { camPoint = rightCamPoint; }
		if (Input.GetKey(KeyCode.J)) { zoom = 0.2f; }
		if (Input.GetKey(KeyCode.K)) { zoom = -0.2f; }
		if (Input.mouseScrollDelta.x >= 0.2f) { camPoint = rightCamPoint; }
		if (Input.mouseScrollDelta.x <= -0.2f) { camPoint = leftCamPoint; }
		if (Input.mouseScrollDelta.y >= 0.2f) { zoom = -0.2f; }
		if (Input.mouseScrollDelta.y <= -0.2f) { zoom = 0.2f; }

	}

	void LateUpdate() {

		oldCameraPos = transform.position;
		newCameraPos = camPoint;

		
		//transform.position = Vector3.SmoothDamp(oldCameraPos, newCameraPos, ref currentVelocity, 0.3f);
		transform.position = Vector3.Lerp(oldCameraPos, newCameraPos, 0.3f);

		playerFloatingRefPoint = new Vector3(player.position.x, cameraHeight, player.position.z);
		VectorFromCamToPlayerRefPoint = playerFloatingRefPoint - camPoint;
		CamFollowPoint = playerFloatingRefPoint - camFollowVector;

		if (camPoint == leftCamPoint || camPoint == rightCamPoint) 
		{ 
			camFollowVector = VectorFromCamToPlayerRefPoint;
			Quaternion oldRotation = transform.rotation;
			transform.rotation = Quaternion.Lerp(oldRotation, Quaternion.LookRotation(cameraFocus.position - transform.position), 0.3f);
		} 
		

		//Adjust Camera Zoom
		zoomAmount = Mathf.Clamp(zoomAmount + (zoom * zoomFactor),30,60);
		Camera.main.fieldOfView = zoomAmount;
	}
}
