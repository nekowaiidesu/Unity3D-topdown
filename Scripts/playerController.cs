using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class playerController : MonoBehaviour {

	//handling
	public float  rotationSpeed = 450;
	public float walkSpeed = 5;
	public float runSpeed = 6;


	//System
	private Quaternion targetRotation;

	//Components
	private CharacterController controller;
	private Camera cam;
	public basicGunScript gun;


	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		//ControlWSAD ();
		ControlMouse();

		if (Input.GetButtonDown ("Fire1")) {
			gun.Shoot ();
		}

		else if (Input.GetButton("Fire1")){
			gun.ShootAuto();
		}
	}

	void ControlMouse(){

		Vector3 mousePos = Input.mousePosition;
		mousePos = cam.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
		targetRotation = Quaternion.LookRotation (mousePos - new Vector3(transform.position.x, 0, transform.position.z));
		transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);

		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		Vector3 motion = input;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)? .7f:1;
		motion *= (Input.GetButton("Run"))?runSpeed:walkSpeed;
		motion += Vector3.up * -8;

		controller.Move (motion * Time.deltaTime);
	}




	void ControlWSAD(){
		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));

		if (input != Vector3.zero) {
			targetRotation = Quaternion.LookRotation (input);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
		}

	    Vector3 motion = input;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)? .7f:1;
		motion *= (Input.GetButton("Run"))?runSpeed:walkSpeed;
		motion += Vector3.up * -8;

		controller.Move (motion * Time.deltaTime);
	}
}
