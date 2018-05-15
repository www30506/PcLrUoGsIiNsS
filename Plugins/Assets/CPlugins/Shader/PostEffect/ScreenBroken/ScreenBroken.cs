using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBroken : PostEffectBase {
	public Texture BumpMap;
	public float satCount;
	public float _Value;
	void Start () {
		
	}
	
	void Update () {
		
	}

	void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture){
		float scaleX , scaleY ;

		if(sourceTexture.width > sourceTexture.height){	
			scaleX = 1.0f;
			scaleY = (float) sourceTexture.height / (float) sourceTexture.width;
		}		
		else{
			scaleX = (float) sourceTexture.width / (float) sourceTexture.height;
			scaleY = 1.0f;	
		}

//		scaleX = (float) sourceTexture.width / (float) sourceTexture.height;
//		scaleY = (float) sourceTexture.height / (float) sourceTexture.width;

		if(_Material != null){
			_Material.SetFloat ("_satCount", satCount);
			_Material.SetFloat ("_scaleX", scaleX);
			_Material.SetFloat ("_scaleY", scaleY);
			_Material.SetTexture ("_BumpTex", BumpMap);
			_Material.SetFloat ("_Value", _Value);
			Graphics.Blit (sourceTexture, destTexture, _Material,0);
		}
		else {
			Graphics.Blit (sourceTexture, destTexture);
		}
	}
}
