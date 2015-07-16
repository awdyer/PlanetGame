using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
	
	// public vars
	public float mouseSensitivityX = 250;
	public float mouseSensitivityY = 250;
	public float walkSpeed = 5;
	public float jumpForce = 500;
	public LayerMask groundedMask;
	public GameObject planet;
	
	// System vars
	bool grounded;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	Transform cameraTransform;
	Rigidbody rigidbody;

	private Vector3 startPos;
	
	//shooting object
	void Start() {
		startPos = transform.position;
	}


	void Awake() {
		Screen.lockCursor = true;
		cameraTransform = Camera.main.transform;
		rigidbody = GetComponent<Rigidbody> ();
	}
	private void parent(bool hit) {
		if (hit == true) {
			this.transform.parent = planet.transform;
		} else {
			this.transform.parent = null;
		}
	}

	void Update() {
	
		// Look rotation:
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime);
		verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation,-60,60);
		cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
		
		// Calculate movement:
		float inputX = Input.GetAxisRaw("Horizontal");
		float inputY = Input.GetAxisRaw("Vertical");
		
		Vector3 moveDir = new Vector3(inputX,0, inputY).normalized;
		Vector3 targetMoveAmount = moveDir * walkSpeed;
		moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMoveVelocity,.15f);
		
		// Jump
		if (Input.GetButtonDown("Jump")) {
			//if(grounded){
			print ("jump");
				rigidbody.AddForce(transform.up * jumpForce);
			//}
			//parent(false);
		}
		
		// Grounded check
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, 4.3f + planet.transform.localScale.x/40, groundedMask)) {
			//print ("on ground");
			grounded = true;
			parent(true);

		}
		else {
			//print ("off ground");
			grounded = false;
			parent(false);
		}

	}
	void OnCollisionEnter (Collision col){
		print ("I just hit" + col.gameObject);
		if (col.gameObject.tag == "planet") {
			grounded = true;
			parent(true);
		}

	}
	void OnTriggerEnter (Collider col){
		print (col.gameObject);
		if (col.gameObject.tag == "water") {
			//half speed here and make player loose health
			transform.position = startPos;
		}
	}
	void FixedUpdate() {
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
		rigidbody.MovePosition(rigidbody.position + localMove);
	}
}