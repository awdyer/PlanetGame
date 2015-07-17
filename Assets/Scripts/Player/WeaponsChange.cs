using UnityEngine;
using System.Collections;

public class WeaponsChange : MonoBehaviour {
		
	public Transform weaponSpot;
	public Weapons weaponsScript;

	public GameObject currentWeapon;
	public GameObject currentType;
	
	private GameObject[] typeList;
	private GameObject[] weaponList;
	private int typeNumber = 0;
	/*Adds all weapons to weaponsList*/
	void Start () {
		typeList = new GameObject[10];
		typeList = Resources.LoadAll<GameObject>("WeaponTypes") as GameObject[];

		weaponList = new GameObject[10];
		weaponList = Resources.LoadAll<GameObject>("Weapons") as GameObject[];


		foreach (GameObject obj in typeList){
			GameObject weaponType = Instantiate(obj, weaponSpot.position, weaponSpot.rotation) as GameObject;
			weaponType.SetActive(false);
			weaponType.transform.parent = weaponSpot;
		}

		currentType = typeList [typeNumber];
		print (currentType);

		currentType.SetActive(true);

		currentWeapon = weaponList [typeNumber];
		weaponsScript.Bullet = currentWeapon;
	}
	
	/*Handles user inputs*/
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			if(typeNumber > 0){
				typeNumber -= 1;
				WeaponChange(typeNumber);
			}
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			if(typeNumber < typeList.Length -1){
				typeNumber += 1;
				WeaponChange(typeNumber);
			}
		}
	}

	/*Changes currentWeapon to newWeapon*/
	void WeaponChange (int typeNumber){
		currentType = typeList[typeNumber];
		currentWeapon = weaponList [typeNumber];
		weaponsScript.Bullet = currentWeapon;
	}

	/*Adds new Weapon to weaponsList*/
	void AddWeapons(){
		//yet to be implemented
	}

}
