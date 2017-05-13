using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_MainPage : Page_Base {

	void Start () {
		
	}

	void Update () {
		
	}

	protected override void OnOpen (){

		print ("Test_MainPage Open");
	}

	protected override IEnumerator IE_OnOpen ()
	{
		yield return null;
		print ("Test_MainPage IE_Open");
	}

	protected override void OnClose (){

		print ("Test_MainPage Close");
	}

	protected override IEnumerator IE_OnClose ()
	{
		yield return null;
		print ("Test_MainPage IE_Close");
	}


	public void OnToSecondPage(){
		PageManerger.ChangePage (PageType.Test_SecondPage);
	}
}
