using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTest_UGUIExample : MonoBehaviour {
	[SerializeField]private UnitTest_UGUI unitTest_UGUI;

	void Start () {
		
	}

	void Update () {
		if(Input.GetKeyUp(KeyCode.R)){
			unitTest_UGUI.Record();
		}

		if(Input.GetKeyUp(KeyCode.S)){
			unitTest_UGUI.StopRecord();
		}

		if(Input.GetKeyUp(KeyCode.P)){
			unitTest_UGUI.PlayRecord();
		}

		if(Input.GetKeyUp(KeyCode.Z)){
			print("時間100倍");
			Time.timeScale = 100;
		}
		if(Input.GetKeyUp(KeyCode.X)){
			print("時間恢復");
			Time.timeScale = 1;
		}
	}

	public void Hi(){
		print("Hi");
	}

	public void Down(){
		print("Down");
	}

	public void Up(int p_number){
		print("Up " +p_number);
	}
}
