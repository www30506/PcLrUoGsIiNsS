using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualjoystickSystemExample : MonoBehaviour {
	public VirtualjoystickSystem virtualjoystickSystem;

	void Start () {
		
	}
	
	void Update () {
		if(virtualjoystickSystem.GetDirection() != Vector2.zero){
			print(virtualjoystickSystem.GetDirection());
		}
	}
}
