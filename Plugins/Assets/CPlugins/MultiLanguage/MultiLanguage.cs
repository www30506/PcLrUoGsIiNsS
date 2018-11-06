using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public enum LanguageType{English = 1, TraditionalChinese, SimplifiedChinese};
public class MultiLanguage : MonoBehaviour {
	[SerializeField] private string textKey;
	public static LanguageType nowlanguage = LanguageType.English;
	public static Dictionary<string, Dictionary<string, object>> DATA;

	void Start () {
		Text _text = this.GetComponent<Text>();
		if(_text !=null){
			_text.text = MultiLanguage.GetText(textKey);
		}
	}

	void Update () {
		
	}

	public static void SetLanguageTxt(string p_StreamingPath){
		FileStream file = new FileStream(Application.streamingAssetsPath + "/" + p_StreamingPath + ".txt", FileMode.Open, FileAccess.Read);
		StreamReader sr = new StreamReader(file);
		DoParseDefineData(sr.ReadToEnd());
		file.Close();
		sr.Close();
	}

	private static void DoParseDefineData(string p_data){
		DATA = new Dictionary<string, Dictionary<string, object>>();
		string[] _allLine = p_data.Split('\n');
		string _key = null;

		for(int i=0; i< _allLine.Length; i++){
			string[] _strData = _allLine[i].Split('\t');
			Dictionary<string, object> _data = new Dictionary<string, object> ();
			//加入key資料
			_data.Add("Key",_strData[0]);

			for(int j=1; j<_strData.Length; j++){
				_key = ((LanguageType)j).ToString();
				_strData[j] = Regex.Replace(_strData[j], "\\s", "");
				_data.Add (_key, _strData [j]);
			}

			DATA.Add(_strData[0], _data);
		}
	}

	public static void ChangeLanguage(LanguageType p_LanguageType){
		nowlanguage = p_LanguageType;
	}

	public static string GetText(string p_key){
		string _text = DATA[p_key][nowlanguage.ToString()].ToString();
		return _text;
	}
}
