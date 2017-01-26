using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellCleanup : MonoBehaviour {

	private Rigidbody r;


	// Use this for initialization
	void Start () {
		r = GetComponent<Rigidbody>();
		StartCoroutine ("ShellFade");
	}

	IEnumerator ShellFade() {
		yield return new WaitForSeconds (5f);
		Destroy (gameObject);

	}

	void OnTriggerEnter(Collider c) {
		if (c.tag == "Ground") {
			r.Sleep (); 
		}
	}
}
