using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeveloperControls : MonoBehaviour {

	private int MAX_LEVELS = 6; 

 	private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };
 
	private void Update () {

		if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Z)) {
			for(int i = 0 ; i < MAX_LEVELS; i++){
				if(Input.GetKeyDown(keyCodes[i])){
					int numberPressed = i + 1;

					Progress.Day = numberPressed - 1;
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
			}
		}	

		if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Z) && Input.GetKeyDown(KeyCode.Alpha8)) {
			GameObject stageObj = GameObject.Find("Stage");
			if(stageObj != null) {
				stageObj.GetComponent<Stage>().StageOver();
			}
		}	

		if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Z) && Input.GetKeyDown(KeyCode.Alpha7)) {
			Progress.Candy += 100;
		}	
	}
}
