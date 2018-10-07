using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class PlayerControl : MonoBehaviour {

	public GameManager manager;

	private Rigidbody rb;
	public float speed;
	private Vector3 move;
	private float maxspeed = 100f;
	private Vector3 spawn;
	public GameObject deathParticles;

	public bool usesManager = true;

	public AudioClip[] audioClip;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		spawn = transform.position;
		if (usesManager)
		{
			manager = manager.GetComponent<GameManager> ();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		float moveHorizontal = CnInputManager.GetAxis ("Horizontal");

		float moveVertical = CnInputManager.GetAxis ("Vertical");

		//if(rb.velocity.magnitude < maxspeed)
		//{
			Vector3 move = new Vector3 (moveHorizontal,0.0f,moveVertical);
	//	}
		rb.AddForce (move*speed);

		if (transform.position.y < -3) 
		{
			die ();
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "Enemy") 
		{
			die ();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Enemy")
		{
			die ();
		}

		if (other.transform.tag == "Trigger")
		{
			manager.CompleteLevel ();
			PlaySound (1);
			Time.timeScale = 0f;
		}

		if (other.transform.tag == "Token") 
		{
			if (usesManager) 
			{
				manager.tokenCount += 1;
			}

			Destroy (other.gameObject);
			PlaySound (0);

		}
	}

	void die()
	{
		Instantiate (deathParticles,transform.position,Quaternion.Euler(270,0,0));
		transform.position = spawn;
	}

	public void PlaySound(int clip)
	{
		AudioSource au = GetComponent<AudioSource> ();

		au.clip = audioClip[clip];
		au.Play ();
	}
}
