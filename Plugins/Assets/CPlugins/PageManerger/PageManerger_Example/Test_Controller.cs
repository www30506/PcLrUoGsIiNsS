using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Controller : MonoBehaviour {
	
	IEnumerator Start () {
		PageManerger.CloseAllPage ();
		yield return null;
		PageManerger.ChangePage (PageType.Test_MainPage);
	}
	
	void Update () {
		
	}
}
