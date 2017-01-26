using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class basicGunScript : MonoBehaviour {


	public enum GunType {Semi,Burst,Auto};
	public GunType gunType;

	public float rpm;
	//components
	public Transform spawn;
	private LineRenderer tracer;
	private AudioSource audio;
	public Rigidbody shell;
	public Transform shellEjectPoint;

	//system
	private float secondsBetweenShots;
	private float nextPossibleShootTime;

	void Start(){
		secondsBetweenShots = 60 / rpm;
		if (GetComponent<LineRenderer>()) {
			tracer = GetComponent<LineRenderer>();
			audio = GetComponent<AudioSource> ();
		}
	}

	public void Shoot() {

		if (CanShoot ()) {

			Ray ray = new Ray (spawn.position, spawn.forward);
			RaycastHit hit;
			float shotDistance = 20;

			if (Physics.Raycast (ray, out hit, shotDistance)) {
				shotDistance = hit.distance;
			}

			Debug.DrawRay (ray.origin, ray.direction * shotDistance, Color.red, 1);
			nextPossibleShootTime = Time.time + secondsBetweenShots;

			audio.Play ();

			if (tracer) {
				StartCoroutine ("RenderTracer", ray.direction * shotDistance);
			}

			Rigidbody newShell = Instantiate (shell, shellEjectPoint.position, shellEjectPoint.rotation) as Rigidbody;
			newShell.AddForce (shellEjectPoint.forward * Random.Range (10f, 50f));// + shellEjectPoint.forward * Random.Range (-10, 10));


		} 
	}

	public void ShootAuto(){
		if (gunType == GunType.Auto) {
			Shoot ();

		}
	}

	private bool CanShoot() {
		bool canShoot = true;

		if (Time.time < nextPossibleShootTime){
			canShoot = false;
		}
		return canShoot;
	}

	IEnumerator RenderTracer(Vector3 hitPoint) {
		tracer.enabled = true;
		tracer.SetPosition (0, spawn.position);
		tracer.SetPosition (1, spawn.position + hitPoint);
		yield return null;
		tracer.enabled = false;
	}

}
