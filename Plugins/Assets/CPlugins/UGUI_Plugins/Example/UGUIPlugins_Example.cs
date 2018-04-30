using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UGUIPlugins_Example : MonoBehaviour {
	public Text addScoreText;
	public Text textToSprie;
	public Text textToTexture;
	public Text textTypewrite;

	void Start () {
		Invoke("Example_AddScore", 0.1f);
		Example_textToSprie();
		Example_textToTexture();
		Example_TextTypewrite();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void Example_AddScore(){
		addScoreText.text = 100.ToString();
	}

	private void Example_textToSprie(){
		//這會將Text裡面的每一個文字轉換為單獨的key 再用key去生成Sprite圖片
		textToSprie.text = "12";
	}

	private void Example_textToTexture(){
		//這會將Text裡面的每一個文字轉換為單獨的key 再用key去生成Texture圖片
		textToTexture.text = "12";
	}

	private void Example_TextTypewrite(){
		textTypewrite.text = "哈哈哈哈哈哈哈哈哈哈哈哈哈";
	}
}
