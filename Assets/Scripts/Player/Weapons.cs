using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {
	
	//the point we want to fire the bullet from
	public Transform FiringPoint;
	private GameObject currentType;
	private GameObject selectedWeapon;
	private GameObject selectedWType;

	public Transform weaponSpot;
	private GameObject WeaponType;
	public GameObject Tree;
	public GameObject Water;

	//the bullet itself
	private GameObject Bullet;
	public GameObject TreeBullet;
	public GameObject WaterBullet;
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
	private float WeaponRange = 335.0f;
	
	public float scaleAmount = 1.2501f;

	public GameObject frozenFloor;
	public GameObject planet;

	public float gunEnergy = 10.0f;

	void Start(){
		SelectedWeapon (Tree, TreeBullet);
		point = new Vector3 (0.5f, 0.5f, 0f);
	}

	void Update(){
		if (gunEnergy < 10) {
			gunEnergy += 0.025f;
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
		//Shoot Press
		if(Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFire){
			//updates nextFire
			nextFire = Time.time + fireRate;
			//instantiates bullet/object
			Gunshot = Instantiate(Bullet,FiringPoint.position,FiringPoint.rotation) as GameObject;

			Gunshot.GetComponent<bullet>().FiringPoint = FiringPoint;
			//parents object
			Gunshot.transform.parent = GameObject.Find("space-gun").transform;
			//destroys object
			Destroy (Gunshot, WeaponRange);


		}
		//Shoot Hold
		if (Input.GetKey (KeyCode.Mouse0)) {
			if (hit.collider.tag == "water"){
				ScaleObject(hit.collider.gameObject);
			}
		}

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

		//Shoot Realease
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			//tells object where to go
			Gunshot.GetComponent<bullet>().Shoot = true;
			Gunshot.GetComponent<bullet>().Point = TargetPoint;
		}

		//Switches weapons
		if (Input.GetKeyDown (KeyCode.Q)) {
			SelectedWeapon(Tree, TreeBullet);
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			SelectedWeapon(Water, WaterBullet);
		}

		if (hit.collider.tag == "frozen" && gunEnergy > 0) {
			gunEnergy -= 0.1f;
			ScaleObject(hit.collider.gameObject);
		}

	}

	void SelectedWeapon(GameObject selectedType, GameObject selectedWeapon){
		if(!(currentType == selectedType)){
			Destroy (currentType);
			currentType = Instantiate (selectedType, weaponSpot.position, weaponSpot.rotation) as GameObject;
			currentType.transform.parent = MainCamera.transform;
			Bullet = selectedWeapon;
		}
	}

	void ScaleObject(GameObject scaleObject){
		if (scaleObject.transform.localScale.magnitude < 3) {
			scaleObject.transform.localScale = new Vector3 (scaleObject.transform.localScale.x * scaleAmount, scaleObject.transform.localScale.y * scaleAmount, scaleObject.transform.localScale.z * scaleAmount);
		}
	}
}

