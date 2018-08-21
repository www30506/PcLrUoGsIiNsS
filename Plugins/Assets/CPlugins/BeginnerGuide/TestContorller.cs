using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestContorller : MonoBehaviour {
	[SerializeField]private BeginnerGuide beginnerGuide;

	void Start () {
		beginnerGuide.StartBeginnerGuide();
	}

	void Update () {
		
	}

	public void OnA(){
		Debug.Log("A");
	}

	public void OnB(){
		Debug.Log("B");
	}

	public void OnC(){
		Debug.Log("C");
	}
}
