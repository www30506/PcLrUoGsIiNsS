using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/Mekudo", 20)]
public class Mekudo : MonoBehaviour {
	[SerializeField]private TextureWrapMode wrapMode = TextureWrapMode.Repeat;
	[SerializeField]private Vector2 second;
	private float speed_X;
	private float speed_Y;
	private RawImage rawImage;

	void Awake(){
		rawImage = this.GetComponent<RawImage>();
		SetSpeed();
	}

	void  Start(){
		rawImage.texture.wrapMode = wrapMode;
	}
	
	void Update () {
		#if UNITY_EDITOR
		SetSpeed();
		#endif
		rawImage.uvRect = new Rect((speed_X * Time.time) %1.0f, (speed_Y* Time.time)%1.0f, 1,1);
	}

	private void SetSpeed(){
		if(second.x ==0.0f){
			speed_X = 0;
		}
		else{
			speed_X = 1/second.x;
		}

		if(second.y ==0.0f){
			speed_Y = 0;
		}
		else{
			speed_Y = 1/second.y;
		}
	}
}
