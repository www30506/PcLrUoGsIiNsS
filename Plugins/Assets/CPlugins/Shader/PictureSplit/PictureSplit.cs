using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureSplit : MonoBehaviour {
	private RawImage rawImage;
	[SerializeField]private Material pictureSplit;
	[SerializeField]private Transform canvasParent;
	private RenderTexture texture;
	private Camera m_mainCam;

	void Start () {
	}

	void Update () {
		
		if(Input.GetKeyUp(KeyCode.A)){
			//這邊是擷取螢幕到目標RawImage上面
			m_mainCam = Camera.main;
			texture = new RenderTexture (Screen.width, Screen.height, 24);
			RenderTexture.active = texture;
			RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
			m_mainCam.targetTexture = rt; //Create new renderTexture and assign to camera

			m_mainCam.Render();

			RenderTexture.active = rt;

			m_mainCam.targetTexture = null;
			RenderTexture.active = null; //Clean
			CreateRawImage();
			rawImage.texture = rt;
		}

		if(Input.GetKeyUp(KeyCode.S)){
			StartCoroutine(RawImageSplite());
		}
	}

	private void CreateRawImage(){
		GameObject _obj = new GameObject();
		rawImage = _obj.AddComponent<RawImage>();
		rawImage.material = pictureSplit;
		_obj.transform.SetParent(canvasParent, false);
		_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
	}

	IEnumerator RawImageSplite(){
		float _value=0;

		while(_value < 1){
			rawImage.material.SetFloat("_Value", _value);
			yield return null;
			_value += Time.deltaTime;
		}
	}
}