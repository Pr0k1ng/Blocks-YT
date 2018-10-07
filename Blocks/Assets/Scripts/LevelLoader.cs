using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CnControls;

public class LevelLoader : MonoBehaviour {

	public int levelToLoad;
	public string loadPrompt;
	private bool inRange;
	private int completedLevel;
	private bool canLoadLevel;

	public GameObject padlock;

	void Start()
	{
		completedLevel = PlayerPrefs.GetInt ("Level Completed");
		completedLevel++;
		canLoadLevel = levelToLoad <= completedLevel ? true : false;

		if (!canLoadLevel) 
		{
			Instantiate (padlock , new Vector3(transform.position.x , 0.6f , transform.position.z-2f) , Quaternion.Euler(0,90,0));
		}
		
	}
		
	void Update()
	{
		if (canLoadLevel && inRange) 
		{
			SceneManager.LoadScene ("Level"+levelToLoad.ToString());
		}
	}

	void OnTriggerStay(Collider other)
	{
		inRange = true;
		if (canLoadLevel) {
			loadPrompt = "PRESS [E] TO LOAD LEVEL " + levelToLoad.ToString ();
		} else {
			loadPrompt = "LEVEL " + levelToLoad.ToString () + " IS LOCKED";
		}
	}

	void OnTriggerExit()
	{
		inRange = false;
		loadPrompt = "";
	}

	void OnGUI()
	{	
		
		GUI.Label (new Rect (Screen.width * .8f,10f,500f,200f), loadPrompt);
	}
}
