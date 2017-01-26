using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicCamera : MonoBehaviour {

	public Transform target;
	private Camera mainCam;

	// Use this for initialization
	void Start () {
		mainCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		mainCam.transform.position = target.position + new Vector3 (0, 15, 0);
	}
}
