using System.Collections;
using UnityEngine;
 
class PlayerController: MonoBehaviour {
    private float moveSpeed = 10f;
    
    private Vector3 destination;
    private float movePoint;
    
    private Vector2 input;
    private bool isMoving = false;
    
    public void Update() {
    	if (!isMoving){
			Move();
		}
    }
    
    private void Move() {
    	Vector2 direction = GetMoveDirection();
    	// If there is a movement
    	if (direction != Vector2.zero) {
    		destination = transform.position + new Vector3(direction.x, 0f, direction.y);
    		StartCoroutine(move(transform));
    	}
    }
    
    private Vector2 GetMoveDirection() {
    	Vector2 direction;
    	if 		(Input.GetButton("move SW")) {
    		direction = new Vector2(-1f, -1f);
		}
		else if (Input.GetButton("move S")) {
			direction = new Vector2(0f, -1f);
		}
		else if (Input.GetButton("move SE")) {
			direction = new Vector2(1f, -1f);
		}
		else if (Input.GetButton("move W")) {
			direction = new Vector2(-1f, 0f);
		}
		else if (Input.GetButton("move E")) {
			direction = new Vector2(1f, 0f);
		}
		else if (Input.GetButton("move NW")) {
			direction = new Vector2(-1f, 1f);
		}
		else if (Input.GetButton("move N")) {
			direction = new Vector2(0f, 1f);
		}
		else if (Input.GetButton("move NE")) {
			direction = new Vector2(1f, 1f);
		}
		else {
			direction = new Vector2(0f, 0f);
		}
		
		return direction;
    }
    
    public IEnumerator move(Transform transform) {
    	// Start moving
        isMoving = true;
        
        // While destination is still away
        while (Vector3.Distance(transform.position, destination) > 0.1f) {
        	transform.position += (destination - transform.position).normalized * moveSpeed * Time.deltaTime;
        	yield return null;
        }
        // Once we are close enough
        transform.position = destination;
        // Stop moving
        isMoving = false;
        
        yield return true;
     }
}
	