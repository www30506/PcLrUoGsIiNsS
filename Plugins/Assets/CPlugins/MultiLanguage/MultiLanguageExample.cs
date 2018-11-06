using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLanguageExample : MonoBehaviour {

	void Awake(){
//		MultiLanguage.ChangeLanguage(LanguageType.English);
		MultiLanguage.SetLanguageTxt("Crosis/Level1");
		MultiLanguage.ChangeLanguage(LanguageType.TraditionalChinese);
//		MultiLanguage.ChangeLanguage(LanguageType.SimplifiedChinese);
	}

	void Start () {
		print(MultiLanguage.nowlanguage);
		MultiLanguage.ChangeLanguage(LanguageType.English);
		print(MultiLanguage.GetText("Level1_Test1"));
		MultiLanguage.ChangeLanguage(LanguageType.TraditionalChinese);
		print(MultiLanguage.GetText("Level1_Test1"));
		MultiLanguage.ChangeLanguage(LanguageType.SimplifiedChinese);
		print(MultiLanguage.GetText("Level1_Test1"));
	}

	void Update () {
		
	}
}
