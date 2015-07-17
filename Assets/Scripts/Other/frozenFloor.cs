using UnityEngine;
using System.Collections;

public class frozenFloor : MonoBehaviour {

	private float scaleStep = 0.01f;
	public float startAmount = 0.5f;
	public float endAmount = 0f;

	private Vector3 startScale;
	private Vector3 endScale;
	private Vector3 maxScale;

	private bool scale = false;

	void Start () {
		startScale = new Vector3 (startAmount, startAmount, startAmount/10);
		endScale = new Vector3 (endAmount, endAmount, endAmount/10);
		//sets objects initial scale
		transform.localScale = startScale;
	}

	void Update () {
		//if (scale == true) {
			//scales object at rate of scaleStep
			transform.localScale = Vector3.Lerp (transform.localScale, endScale, scaleStep);
		if (transform.localScale.magnitude < 0.1) {
			DestroyObject(gameObject);
		}
		//}
	}
}
