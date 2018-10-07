using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour {

	public float delayTime;
	// Use this for initialization
	void Start () {
		StartCoroutine (go ());
	}
	
	IEnumerator go()
	{
		while (true) 
		{
			gameObject.GetComponent<Animation>().Play();
			yield return new WaitForSeconds (3f);
		}
	}
}
