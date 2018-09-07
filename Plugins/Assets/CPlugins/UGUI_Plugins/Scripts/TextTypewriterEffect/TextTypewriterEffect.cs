using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Other/TextTypewriter")]
public class TextTypewriterEffect : MonoBehaviour {
	[Header("每秒幾個字")]
	[SerializeField]private int wordSpeed = 5;
	private Text m_text;
	private Text m_text2;

	private string changeString;
	private string oldString;
	private char[] changeChars;
	private int nowNumber;
	private float tempTime;
	private float intervalTime;
	private bool isChange = false;
	private int tempIndex = 0;

	void Awake(){
		m_text = this.GetComponent<Text>();
		CreateText2();
		m_text.enabled = false;
		intervalTime = wordSpeed/60.0f;
	}

	private void CreateText2(){
		m_text2 = new GameObject("Text").AddComponent<Text>();
		m_text2.transform.SetParent(m_text.transform, false);
		m_text2.GetComponent<RectTransform>().anchorMin = new Vector2(0,0);
		m_text2.GetComponent<RectTransform>().anchorMax = new Vector2(1,1);
		m_text2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
		m_text2.GetComponent<RectTransform>().sizeDelta = new Vector2(0,0);

		m_text2.font = m_text.font;
		m_text2.fontStyle = m_text.fontStyle;
		m_text2.fontSize = m_text.fontSize;
		m_text2.lineSpacing = m_text.lineSpacing;
		m_text2.supportRichText = m_text.supportRichText;
		m_text2.alignment = m_text.alignment;
		m_text2.alignByGeometry = m_text.alignByGeometry;
		m_text2.horizontalOverflow = m_text.horizontalOverflow;
		m_text2.verticalOverflow = m_text.verticalOverflow;
		m_text2.resizeTextForBestFit = m_text.resizeTextForBestFit;
		m_text2.color = m_text.color;
		m_text2.raycastTarget = m_text.raycastTarget;
	}

	void Start () {
		
	}

	void Update () {
		if(m_text.text != oldString){
			isChange = true;
			changeString = m_text.text;
			m_text.text += tempIndex;
			oldString = m_text.text;
			tempIndex++;
			if(tempIndex>5) {
				tempIndex = 0;
			}

			changeChars = changeString.ToCharArray();
			nowNumber = 0;
			m_text2.text = "";
			intervalTime = 1.0f/wordSpeed;
		}

		if(isChange){
			tempTime +=Time.deltaTime;
			if(nowNumber < changeChars.Length){
				if(tempTime > intervalTime){
					tempTime -=intervalTime;
					ChangeText();
				}
			}
			else{
				isChange = false;
			}
		}
	}

	private void ChangeText(){
		m_text2.text += changeChars[nowNumber++];
	}
}
