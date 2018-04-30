using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenShot_Example : MonoBehaviour {
	public Image target;
	public RawImage target_II;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){
			target.sprite = ScreenShot.ToSprite();
		}
		if(Input.GetKeyDown(KeyCode.S)){
			target_II.texture = ScreenShot.ToTexture();
		}
	}
}
