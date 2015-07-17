using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	public Transform planet;

	public Rigidbody rigidBody;
	private bool hit = false;

	//the target point of the bullet
	public Vector3 Point;
	//the normal speed we want the bullet to travel at
	public float BulletSpeed = 0.05f;

	public bool Shoot = false;


	private bool targetSet = false;

	//values that will be set in the Inspector
	public GameObject Target;
	public float RotationSpeed = 10.0f;
	
	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;

	public bool destructable;

	void Start(){
		planet = GameObject.Find ("Planet").transform;
	}

	void Update(){
		//find the vector pointing from our position to the target
		_direction = (planet.transform.position + transform.position).normalized;
		
		//create the rotation we need to be in to look at the target
		_lookRotation = Quaternion.LookRotation(_direction);
		
		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);

		if (hit == false) {
			if (Shoot == true) {
				targetPosition ();
				rigidBody.isKinematic = false;
				transform.position = Vector3.MoveTowards (transform.position, Target.transform.position, BulletSpeed);
				//print ("Scaling Bullet" + transform.localScale);
				//transform.localScale += scale;
				//transform.position += 0.015f * transform.forward;
		
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
			Target.transform.position = Point;
			Target.transform.parent = planet.transform;
			targetSet = true;
			//Sets objects s it can collide 
			GetComponent<Collider> ().isTrigger = false;
		}
	}
	void hitTrue(GameObject hitObject){
		hit = true;
		rigidBody.isKinematic = true;
		Destroy (Target);
		transform.parent = hitObject.transform;
		print ("hit: " + hitObject);
	}
}

