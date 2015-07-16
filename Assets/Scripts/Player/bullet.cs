using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {
	public GameObject planet;
	public Rigidbody rigidBody;
	private bool hit = false;

	//the target point of the bullet
	public Vector3 Point;
	//the normal speed we want the bullet to travel at
	public float BulletSpeed = 5.0f;
	//this script is design to fire even if it doesnt hit anything 
	//this controller the speed if it fires that way
	public float BulletSpeed2 = 5.0f;

	private Vector3 Destination = new Vector3(0.0f,0.0f,0.0f);

	public bool Shoot = false;
	private float scaleAmount = -0.05f;
	private Vector3 scale;
	public Transform FiringPoint;
	public GameObject target;
	private bool targetSet = false;



	//values that will be set in the Inspector
	public Transform Target;
	public float RotationSpeed = 10.0f;
	
	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;


	void Start(){
		planet = GameObject.Find ("planet");
		scale = new Vector3 (scaleAmount, scaleAmount, scaleAmount);



	}

	void Update(){
		//find the vector pointing from our position to the target
		_direction = (planet.transform.position - transform.position).normalized;
		
		//create the rotation we need to be in to look at the target
		_lookRotation = Quaternion.LookRotation(_direction);
		
		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);

			if (hit == false) {
				if (Shoot == false) {
					//print ("Scaling Bullet" + transform.localScale);
					transform.localScale += scale;
					transform.position += 0.015f * transform.forward;
				} else {
					//sets the target point of the bullet
					targetPosition();
					//moves bullet towards the target point
					transform.position = Vector3.MoveTowards (transform.position, target.transform.position, BulletSpeed);
				}
				if (transform.position == Point) {
					hitTrue (null);
				}
			}

	}

	void OnCollisionEnter (Collision col){
		if(Shoot == true){
			hitTrue (col.gameObject);

		}
	}

	void targetPosition (){
		if (targetSet == false) {
			target.transform.position = Point;
			target.transform.parent = planet.transform;
			targetSet = true;
			//Sets objects s it can collide 
			GetComponent<Collider> ().isTrigger = false;
		}
	}
	void hitTrue(GameObject hitObject){
		hit = true;
		rigidBody.isKinematic = true;
		Destroy (target);
		transform.parent = GameObject.Find ("planet").transform;
		print ("hit: " + hitObject);
	}
}

