using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GUISkin skin;

	void OnGUI()
	{
		GUI.skin = skin;
		GUI.Label (new Rect (10, 10,300,80), "BLOCKS");

		if (GUI.Button (new Rect (10, 50, 400, 100), "PLAY")) 
		{
			
			SceneManager.LoadScene ("World_Select");
		}

		if (GUI.Button (new Rect (10, 160, 400, 100), "CONTINUE")) 
		{
			SceneManager.LoadScene (PlayerPrefs.GetInt("Level Completed"));
		}

		if (GUI.Button (new Rect (10, 270, 400, 100), "QUIT")) 
		{
			Application.Quit ();
		}

		if (GUI.Button (new Rect (10, 380, 400, 100), "DELETE SAVE FILE")) 
		{
			PlayerPrefs.DeleteAll ();
		}
	}
}
