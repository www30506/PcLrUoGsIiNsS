using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Controller : MonoBehaviour {
	
	void Start () {
		PageManerger.CloseAllPage ();
		PageManerger.ChangePage (PageType.Test_MainPage);
	}
	
	void Update () {
		
	}
}
