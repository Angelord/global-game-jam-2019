using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeveloperControls : MonoBehaviour {

	//Controls
	//Shift + Z + Number 	- Skip to level Number
	//Shift + X + 1 	 	- Skip wave
	//Shift + X + 2		 	- Skip stage
	//Shift + X + 3		 	- Gain 100 candy

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

	private void Start() {
		if(!Application.isEditor) {
			this.enabled = false;
		}
	}
 
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

		if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.X) && Input.GetKeyDown(KeyCode.Alpha1)) {
			GameObject spawnerObj = GameObject.Find("Spawner");
			if(spawnerObj != null) {
				spawnerObj.GetComponent<EnemySpawner>().SkipWave();
			}
		}

		if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.X) && Input.GetKeyDown(KeyCode.Alpha2)) {
			GameObject stageObj = GameObject.Find("Stage");
			if(stageObj != null) {
				stageObj.GetComponent<Stage>().StageOver();
			}
		}	

		if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.X) && Input.GetKeyDown(KeyCode.Alpha3)) {
			Progress.Candy += 100;
		}	
	}
}
