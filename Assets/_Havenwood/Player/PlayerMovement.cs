using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkMoveStopRadius = 0.5f;
	[SerializeField] float attackMoveStopRadius = 5.0f;
	ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination;
	Vector3 clickPoint;

	bool isInDirectMode = false;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }

	// Fixed update is called in sync with physics
	private void FixedUpdate()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			currentDestination = transform.position;
			isInDirectMode = !isInDirectMode;
		}

		if (isInDirectMode)
		{
			ProcessDirectMovement();
		}
		else
		{
			ProcessMouseMovement();
		}
	}

	private void ProcessDirectMovement()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		Vector3 moveement = v * cameraForward + h * Camera.main.transform.right;

		thirdPersonCharacter.Move(moveement, false, false);
	}

	private void ProcessMouseMovement()
	{
		if (Input.GetMouseButton(0))
		{
			clickPoint = cameraRaycaster.hit.point;
			switch (cameraRaycaster.currentLayerHit)
			{
				case Layer.Walkable:
					currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);
					break;
				case Layer.Enemy:
					currentDestination = ShortDestination(clickPoint, attackMoveStopRadius);
					break;
				default:
					Debug.Log("Unknown layer detected.");
					return;
			}
		}

		WalkToDestination();
	}

	private void WalkToDestination()
	{
		var playerToClickPoint = currentDestination - transform.position;
		if (playerToClickPoint.magnitude >= walkMoveStopRadius)
		{
			thirdPersonCharacter.Move(playerToClickPoint, false, false);
		}
		else { thirdPersonCharacter.Move(Vector3.zero, false, false); }
	}

	private Vector3 ShortDestination (Vector3 destination, float shortening)
	{
		Vector3 reductionVector = (destination - transform.position).normalized * shortening;
		return destination - reductionVector;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		Gizmos.DrawLine(transform.position, currentDestination);
		Gizmos.DrawSphere(currentDestination, 0.1f);
		Gizmos.DrawSphere(clickPoint, 0.2f);

		Gizmos.color = new Color(255,0,0,0.5f);
		Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);


		
	}
}

