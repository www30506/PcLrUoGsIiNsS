using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof (GridLayoutGroup))]
[AddComponentMenu("UI/Other/TextToTexture")]
public class TextToTextTextture : MonoBehaviour {
	[System.Serializable]
	public class NumberTexture{
		public string name;
		public Texture texture;
	}

	private Text thisText;
	private string preNumber = "";
	private List<GameObject> textureList = new List<GameObject>();
	public NumberTexture[] numberTextures = new NumberTexture[10];
	[SerializeField]private bool hideTextOnStart = true;

	void Awake(){
		thisText = this.GetComponent<Text>();
	}

	void Start () {
		if (hideTextOnStart) {
			thisText.color = new Color (1, 1, 1, 0);
		}
	}

	void Update () {
		if(preNumber != thisText.text){
			preNumber = thisText.text;
			ChangeTextTexture();
		}
	}

	private void ChangeTextTexture(){
		string _string = preNumber;
		char[] _char = _string.ToCharArray();
		int _notExistCount = 0;

		for(int i=0; i< _char.Length;i++){
			int _arrayNumber = HasTexture(_char[i].ToString());//if == -1 than mean p_textureNumber is not exist in the textureList
															   //if other than it's the array number in the textureList
			if(_arrayNumber != -1){
				if((i-_notExistCount) >= textureList.Count){
					AddTexture();
				}
				SetTexture((i-_notExistCount), _arrayNumber);
			}
			else{
				print ("Error 不在列表裡面 ");
				_notExistCount++;
			}
		}

		int _removeCount = 0;
		if(_char.Length < textureList.Count){
			_removeCount = textureList.Count - _char.Length;
		}
		for(int i=0; i<_removeCount;i++){
			Destroy(textureList[_char.Length]);
			textureList.RemoveAt(_char.Length);
		}
	}

	private void AddTexture(){
		GameObject _obj = new GameObject("Texture");
		_obj.AddComponent<RawImage>();
		textureList.Add(_obj);
		_obj.transform.SetParent(this.gameObject.transform);
	}

	private void SetTexture(int p_objNumber, int p_textureNumber){
		print (p_objNumber);
		textureList[p_objNumber].GetComponent<RawImage>().texture = numberTextures[p_textureNumber].texture;
	}

	private int HasTexture(string p_name){
		for(int i=0; i< numberTextures.Length;i++){
			if(numberTextures[i].name == p_name){
				return i;
			}
		}
		return -1;
	}
}
