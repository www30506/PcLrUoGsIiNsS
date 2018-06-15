using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTest_UGUIExample : MonoBehaviour {
	[SerializeField]private UnitTest_UGUI unitTest_UGUI;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.R)){
			unitTest_UGUI.Record();
		}

		if(Input.GetKeyUp(KeyCode.S)){
			unitTest_UGUI.StopRecord();
//			unitTest_UGUI.
		}

		if(Input.GetKeyUp(KeyCode.P)){
			unitTest_UGUI.PlayRecord();
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
