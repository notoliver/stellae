using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelScript : MonoBehaviour {
//Attach this script to the GameManager and use it to load the other scene

	public string levelToLoad;

	public void ApplicationLoadLevel(){
		SceneManager.LoadScene (levelToLoad);
	}
}
