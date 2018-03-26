using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (GridLayoutGroup))]
[AddComponentMenu("UI/Other/TextToSprite")]
public class TextToSprite : MonoBehaviour {
	[System.Serializable]
	public class NumberSprite{
		public string name;
		public Sprite sprite;
	}
	[SerializeField]private bool awakeToHideText =false;
	private Text thisText;
	private string preNumber = "";
	private List<GameObject> spriteList = new List<GameObject>();
	public NumberSprite[] numberSprites = new NumberSprite[10];

	void Awake(){
		thisText = this.GetComponent<Text>();
	}

	void Start () {
		if(awakeToHideText){
			thisText.color = new Color(0,0,0,0);
		}
	}

	void LateUpdate () {
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
			int _arrayNumber = HasSprite(_char[i].ToString());//if == -1 than mean p_textureNumber is not exist in the textureList
			//if other than it's the array number in the textureList
			if(_arrayNumber != -1){
				if((i-_notExistCount) >= spriteList.Count){
					AddTexture();
				}
				SetTexture((i-_notExistCount), _arrayNumber);
			}
			else{
				_notExistCount++;
			}
		}

		int _removeCount = 0;
		if(_char.Length < spriteList.Count){
			_removeCount = spriteList.Count - _char.Length;
		}
		for(int i=0; i<_removeCount;i++){
			Destroy(spriteList[_char.Length]);
			spriteList.RemoveAt(_char.Length);
		}
	}

	private void AddTexture(){
		GameObject _obj = new GameObject("Texture");
		_obj.AddComponent<Image>().raycastTarget = false;
		spriteList.Add(_obj);
		_obj.transform.SetParent(this.gameObject.transform);
		_obj.transform.localScale = new Vector3(1,1,1);
		_obj.transform.localPosition = new Vector3(0,0,0);
	}

	private void SetTexture(int p_objNumber, int p_textureNumber){
		spriteList[p_objNumber].GetComponent<Image>().sprite = numberSprites[p_textureNumber].sprite;
	}

	private int HasSprite(string p_name){
		for(int i=0; i< numberSprites.Length;i++){
			if(numberSprites[i].name == p_name){
				return i;
			}
		}
		return -1;
	}

	public void ReFlash(){
		ChangeTextTexture();
	}
}
