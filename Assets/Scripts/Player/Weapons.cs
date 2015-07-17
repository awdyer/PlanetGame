using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {
	
	//the point we want to fire the bullet from
	public Transform FiringPoint;	
	//the camera
	public Camera MainCamera;
	//the point we want the bullet to travel to
	public Vector3 TargetPoint;
	private Vector3 point;

	private bullet bullet; 

	public float fireRate = 0.5f;
	private float nextFire = 0.0f;
	
	private GameObject Gunshot;
	private GameObject floor;
	//how long the bullet can travel before detroying itself
	private float WeaponRange = 30.0f;
	
	public float scaleAmount = 1.2501f;
	

	public int gunEnergy = 100;
	private bool energy;
	public GameObject Bullet;

	private bool scaling = false;

	void Start(){
		point = new Vector3 (0.5f, 0.5f, 0f);
		//FiringPoint.parent = GameObject.Find ("space-gun").transform;
	}

	void Update(){
		//Recharges gun;
		if (gunEnergy < 100) {
			gunEnergy += 1;
		}
		//Checks gun has energy
		if (gunEnergy < 0) {
			energy = false;
		} else {
			energy = true;
		}
		//the beam comes from the camera itself to the tagetpoint
		Ray beam = MainCamera.ViewportPointToRay(point);

		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast (beam, out hit)){
			//tells the beam this is the target point
			TargetPoint=hit.point;
			//draws the beam in the senceview so you can see it
			Debug.DrawLine (FiringPoint.position, hit.point);      
		}
		//Handles user inputs
		userInputs ();
	}

	/*Handles user inputs*/
	void userInputs(){
		//Left Shoot Press
		if(Input.GetButtonDown("Fire1") && Time.time > nextFire && energy){
			gunEnergy -= 10;
			//updates nextFire
			nextFire = Time.time + fireRate;
			//instantiates bullet/object
			Gunshot = Instantiate(Bullet,FiringPoint.position,FiringPoint.rotation) as GameObject;
			Gunshot.GetComponent<bullet>().Point = TargetPoint;
			Gunshot.GetComponent<bullet>().Shoot = true;
			Destroy (Gunshot, WeaponRange);
			Destroy (Gunshot.GetComponent<bullet>().Target, WeaponRange);
		}

		//Right Button Press
		if (Input.GetButtonDown ("Fire2") && Time.time > nextFire && energy && scaling == false) {
			scaling = true;
			//instantiates bullet/object
			Gunshot = Instantiate(Bullet,FiringPoint.position,FiringPoint.rotation) as GameObject;
			Gunshot.transform.parent = FiringPoint;
			ScaleObject(Gunshot.transform);
		}
		//Right Shoot Hold
		if (Input.GetButton ("Fire2") && energy) {
			ScaleObject(Gunshot.transform);
		}
		
		//Right Shoot Realease
		if (Input.GetButtonUp ("Fire2") || (!energy)) {
			scaling = false;
			//tells object where to go
			nextFire = Time.time + fireRate;
			Gunshot.transform.parent = GameObject.Find("Planet").transform;
			scaling = false;
			Gunshot.GetComponent<bullet>().Point = TargetPoint;
			Gunshot.GetComponent<bullet>().Shoot = true;
			Destroy (Gunshot, WeaponRange);
			Destroy (Gunshot.GetComponent<bullet>().Target, WeaponRange);
		}
		
		/*
		if (hit.collider.tag == "frozen" && gunEnergy > 0) {
			gunEnergy -= 1;
			ScaleObject(hit.collider.gameObject);
		}*/
	}
	void instantiateObject(){
		nextFire = Time.time + fireRate;
		//instantiates bullet/object
		Gunshot = Instantiate(Bullet,FiringPoint.position,FiringPoint.rotation) as GameObject;
		Gunshot.GetComponent<bullet>().Point = TargetPoint;
		Gunshot.GetComponent<bullet>().Shoot = true;
		Destroy (Gunshot, WeaponRange);
		Destroy (Gunshot.GetComponent<bullet>().Target, WeaponRange);
	}

	/*Scales Bullet*/
	void ScaleObject(Transform scaleObject){
		print ("scaling");
		gunEnergy -= 2;
		if (scaleObject.localScale.magnitude < 50) {
			//scales Object
			scaleObject.localScale = new Vector3 (scaleObject.localScale.x * scaleAmount, scaleObject.localScale.y * scaleAmount, scaleObject.localScale.z * scaleAmount);
			scaleObject.transform.position += 0.015f * FiringPoint.right;
		}
	}
}











/*
		//forzen floor test
		if (Input.GetKey (KeyCode.Mouse1)) {
			if (hit.collider.tag == "water" && gunEnergy > 0){

				floor = Instantiate(frozenFloor,TargetPoint, planet.transform.rotation) as GameObject;
				gunEnergy -= 1;
				//find the vector pointing from our position to the target
				Vector3 direction = (planet.transform.position - floor.transform.position).normalized;
				//create the rotation we need to be in to look at the target
				Quaternion lookRotation = Quaternion.LookRotation(direction);	
				//rotate us over time according to speed until we are in the required rotation
				floor.transform.rotation = lookRotation;
			}			
		}

 //Shoot Hold
		if (Input.GetButton ("Fire1")) {
			if (hit.collider.tag == "water"){
				ScaleObject(hit.collider.gameObject);
			}
		}
 */
