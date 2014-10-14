using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 6.0F;
	private Vector3 moveDestination = Vector3.zero;
	private Vector3 startingPoint;
	private Vector3 requestedDirection;
	private bool isMoving = false;
	
	void Update() {
		CharacterController controller = GetComponent<CharacterController>();
		
		// If not moving - give chance to move
		if (!isMoving) {
			requestedDirection = GetMoveDirection();
			// Start movement
			if (requestedDirection != Vector3.zero) {
				isMoving = true;
				// Note the starting point in case way is blocked and return is needed
				startingPoint = transform.position;
				moveDestination = transform.position + requestedDirection;
			}
		}
		// If the entity is moving
		if (isMoving) {
			// If it is close enough to its destination - snap to the grid
			if (Vector3.Distance(moveDestination, transform.position) < 0.1f) {
				transform.position = moveDestination;
				// Stop moving
				isMoving = false;
			}
			// Move toward destination
			else {
				Vector3 moveDirection = transform.TransformDirection((moveDestination - transform.position).normalized);
				moveDirection *= speed;
				controller.Move(moveDirection * Time.deltaTime);
			}
		}
	}
    
	void OnControllerColliderHit(ControllerColliderHit hit) {
		Rigidbody body = hit.collider.attachedRigidbody;
		// If encountered some obstacle, return to the starting point
		if (body == null) {
			// Return to starting position
			transform.position = startingPoint;
			// End movement
			isMoving = false;
		}
	}
	
	private Vector3 GetMoveDirection() {
		Vector3 direction;
		if 		(Input.GetButton("move SW")) {
			direction = new Vector3(-1f, 0f, -1f);
		}
		else if (Input.GetButton("move S")) {
			direction = new Vector3(0f, 0f, -1f);
		}
		else if (Input.GetButton("move SE")) {
			direction = new Vector3(1f, 0f, -1f);
		}
		else if (Input.GetButton("move W")) {
			direction = new Vector3(-1f, 0f, 0f);
		}
		else if (Input.GetButton("move E")) {
			direction = new Vector3(1f, 0f, 0f);
		}
		else if (Input.GetButton("move NW")) {
			direction = new Vector3(-1f, 0f, 1f);
		}
		else if (Input.GetButton("move N")) {
			direction = new Vector3(0f, 0f, 1f);
		}
		else if (Input.GetButton("move NE")) {
			direction = new Vector3(1f, 0f, 1f);
		}
		else {
			direction = Vector3.zero;
		}
		
		return direction;
    }
}