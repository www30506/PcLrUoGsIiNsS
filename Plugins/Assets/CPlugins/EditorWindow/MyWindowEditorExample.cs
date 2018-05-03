using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MyWindowEditorExample : MonoBehaviour {
	public Image m_Color;
	public Text intText;
	public Text StringText;
	public enum Type{a1,a2,a3,a4};
	public Text enumText;
	public Toggle toggle;

	void Start () {
		Test_Color();
		Test_Int();
		Test_String();
		Test_Toggle();
		Test_Enum();
	}

	void Update () {
		
	}

	private void Test_Color(){
		Color _color;
		ColorUtility.TryParseHtmlString(PD.DATA["Test_1"]["1"]["color"].ToString(), out _color);
		m_Color.color = _color;
	}

	private void Test_Int(){
		intText.text = PD.DATA["Test_1"]["1"]["level"].ToString();
	}

	private void Test_String(){
		StringText.text = PD.DATA["Test_1"]["1"]["des"].ToString();
	}

	private void Test_Toggle(){
		toggle.isOn =  PD.DATA["Test_1"]["1"]["des"].ToString() == "True"? true:false;
	}

	private void Test_Enum(){
		//要使用Enum.Parse 要using System
		//轉成Enum
		Type _type = (Type)Enum.Parse(typeof(Type), PD.DATA["Test_1"]["1"]["ddd"].ToString());
		enumText.text = _type.ToString();
	}
}
