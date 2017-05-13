using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class PD : MonoBehaviour {
	[SerializeField]private TextAsset[] dataTxts;
	public static Dictionary<string, Dictionary<string, Dictionary<string, object>>> DATA;
	private Dictionary<string, Dictionary<string, Dictionary<string, object>>> data = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();

	void Awake () {
		DontDestroyOnLoad (this);
		for (int i = 0; i < dataTxts.Length; i++) {
			DoParse(i, dataTxts[i].text);
		}
		PD.DATA = data;
	}
	
	void Update () {
		
	}

	private void DoParse(int p_number, string p_data){
		Dictionary<string, Dictionary<string, object>> data_2 = new Dictionary<string, Dictionary<string, object>>();

		string[] _allLine = p_data.Split('\n');

		string[] _keyData = _allLine[0].Split('\t');
		string[] _typeKey = new string[_keyData.Length];

		for (int i = 0; i < _keyData.Length; i++) {
			_keyData [i] = Regex.Replace (_keyData [i], @"[^a-zA-Z0-9]", "");
			_typeKey [i] = _keyData [i];
		}

		for(int i=1; i< _allLine.Length; i++){
			char[] _chardata = _allLine[i].ToCharArray();
			if ( _chardata.Length < 1) continue;

			string[] _strData = _allLine[i].Split('\t');

			int _length = _strData.Length;
			Dictionary<string, object> _data = new Dictionary<string, object> ();

			for (int j = 0; j < _typeKey.Length; j++) {
				_strData[j] = Regex.Replace(_strData[j], "\\s", "");
				_data.Add (_typeKey [j], _strData [j]);
			}
			data_2.Add (_strData [0], _data);
		}

		data.Add (dataTxts[p_number].name, data_2);
	}
}
