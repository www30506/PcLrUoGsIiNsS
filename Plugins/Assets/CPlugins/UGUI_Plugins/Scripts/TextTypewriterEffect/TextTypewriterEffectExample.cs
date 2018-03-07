using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTypewriterEffectExample : MonoBehaviour {
	[SerializeField]private Text m_text;
	void Start () {
		
	}

	void Update () {
		if(Input.GetKeyUp(KeyCode.A)){
			m_text.text = "adfhgushdfuighiodsfhgodfg";
		}
		if(Input.GetKeyUp(KeyCode.S)){
			m_text.text = "你好嗎你好嗎你好嗎你好嗎你好嗎你好嗎你好嗎你好嗎你好嗎";
		}
	}
}
