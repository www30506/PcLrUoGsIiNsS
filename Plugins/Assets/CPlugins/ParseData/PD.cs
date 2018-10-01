using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using UnityEngine.Networking;

public class PD : MonoBehaviour {
	[System.Serializable]
	public class FileClass{
		public string m_path;
		public string m_name;

		public FileClass(){
			m_path = "";
			m_name = "";
		}
	}
	public static Dictionary<string, Dictionary<string, Dictionary<string, object>>> DATA;
	private Dictionary<string, Dictionary<string, Dictionary<string, object>>> data;
	public static Dictionary<string, Dictionary<string, Dictionary<string, object>>> DEFINEDATA;
	private Dictionary<string, Dictionary<string, Dictionary<string, object>>> defineData;
	public FileClass[] m_file;
	private static PD instance;
	public static bool isLoadComplete = false;

	void Awake () {
		List<string> dirs = new List<string>();
		List<string> names = new List<string>();
		GetDirs(Application.streamingAssetsPath+"/Data", ref dirs, ref names);

		SetFile(ref dirs, ref names);

		#if UNITY_ANDROID && ! UNITY_EDITOR
		StartCoroutine(Init_Android());
		#else
		Init();
		#endif
	}

	private static void GetDirs(string dirPath, ref List<string> dirs, ref List<string> names)
	{
		foreach (string path in Directory.GetFiles(dirPath))
		{
			//获取所有文件夹中包含后缀为 .prefab 的路径
			if (System.IO.Path.GetExtension(path) == ".txt")
			{
				
				string _name = Path.GetFileName(path.Substring(path.IndexOf("Data")));
				string _path = Regex.Replace(path.Substring(path.IndexOf("Data")), _name, "") ;
				Debug.LogError(_path);
				Debug.LogError(_name);

				_name = Regex.Replace(_name, ".txt","");
				dirs.Add(_path);
				names.Add(_name);
			}
		}

		if (Directory.GetDirectories(dirPath).Length > 0)  //遍历所有文件夹
		{
			foreach (string path in Directory.GetDirectories(dirPath))
			{
				GetDirs(path, ref dirs, ref names);
			}
		}
	}

	private void SetFile(ref List<string> dirs, ref List<string> names){
		m_file = new FileClass[dirs.Count];

		for(int i=0; i< m_file.Length; i++){
			m_file[i] = new FileClass();
			m_file[i].m_path = dirs[i];
			m_file[i].m_name = names[i];
		}
	}

	IEnumerator Init_Android(){
		instance = this;
		data = null;
		data = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();
		defineData = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();

		//DefineData
		for (int i = 0; i < m_file.Length; i++) {
			WWW _data = new WWW(Application.streamingAssetsPath+"/" + m_file[i].m_path + m_file[i].m_name + ".txt");
			yield return _data;
			DoParseDefineData(i, _data.text);
		}
		PD.DEFINEDATA = defineData;


		//Data
		for (int i = 0; i < m_file.Length; i++) {
			WWW _data = new WWW(Application.streamingAssetsPath+"/" + m_file[i].m_path + m_file[i].m_name + ".txt");
			yield return _data;
			DoParse(i, _data.text);
		}

		PD.DATA = data;
		PD.isLoadComplete = true;
	}

	private void Init(){
		instance = this;
		data = null;
		data = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();
		defineData = new Dictionary<string, Dictionary<string, Dictionary<string, object>>>();

		//DefineData
		for (int i = 0; i < m_file.Length; i++) {
			FileStream file = new FileStream(Application.streamingAssetsPath + "/" + m_file[i].m_path + m_file[i].m_name + ".txt", FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(file);
			DoParseDefineData(i, sr.ReadToEnd());
			file.Close();
			sr.Close();
		}
		PD.DEFINEDATA = defineData;


		//Data
		for (int i = 0; i < m_file.Length; i++) {
			FileStream file = new FileStream(Application.streamingAssetsPath + "/" + m_file[i].m_path + m_file[i].m_name + ".txt", FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(file);
			string _ss = sr.ReadToEnd ();
			DoParse(i, _ss);
			file.Close();
			sr.Close();
		}

		PD.DATA = data;

		PD.isLoadComplete = true;
	}


	void Update () {

	}

	public static void Save(Dictionary<string, Dictionary<string, object>> p_data, string p_key){
		PD.instance.M_Save(p_data, p_key);
	}

	private void M_Save(Dictionary<string, Dictionary<string, object>> p_data, string p_key){
		string _path = "";
		string _name = "";

		//取得傳入key 所對應的檔案位子和名稱(名稱跟key相同)
		for(int i=0; i< m_file.Length; i++){
			if(p_key == m_file[i].m_name){
				_path = m_file[i].m_path;
				_name = m_file[i].m_name;
			}
		}

		if(File.Exists(Application.streamingAssetsPath + "/" + _path + _name + ".txt")){
			File.Delete(Application.streamingAssetsPath + "/" + _path + _name + ".txt");
		}

		FileStream file = new FileStream(Application.streamingAssetsPath + "/" + _path + _name + ".txt", FileMode.Create, FileAccess.Write);
		StreamWriter sw = new StreamWriter(file, Encoding.UTF8);
		//寫入Define
		foreach(string _key in defineData[p_key].Keys){
			string _writeLineStr = "";
			int _key_II_Count = 0;

			foreach(string _key_II in defineData[p_key][_key].Keys){
				_writeLineStr += defineData[p_key][_key][_key_II].ToString();

				if(++_key_II_Count < defineData[p_key][_key].Count){
					_writeLineStr +="\t";
				}
			}

			sw.WriteLine(_writeLineStr);
		}

		//寫入 key查詢
		foreach(string _key in p_data.Keys){
			string _writeLineStr = "";
			int _key_II_Count = 0;

			foreach(string _key_II in p_data[_key].Keys){
				_writeLineStr += _key_II;

				if(++_key_II_Count < p_data[_key].Count){
					_writeLineStr +="\t";
				}
			}

			sw.WriteLine(_writeLineStr);
			break;
		}

		int _key_Count = 0;
		//寫入 資料內容
		foreach(string _key in p_data.Keys){
			string _writeLineStr = "";
			int _key_II_Count = 0;

			foreach(string _key_II in p_data[_key].Keys){
				_writeLineStr += p_data[_key][_key_II];
				if(++_key_II_Count < p_data[_key].Count){
					_writeLineStr +="\t";
				}
			}

			if(++_key_Count < p_data.Count){
				sw.WriteLine(_writeLineStr);
			}
			else{
				sw.Write(_writeLineStr);
			}
		}
		sw.Close();
		file.Close();
	}

	private void DoParse(int p_number, string p_data){
		Dictionary<string, Dictionary<string, object>> data_2 = new Dictionary<string, Dictionary<string, object>>();
		int _startCount = defineData[m_file[p_number].m_name].Count;

		string[] _allLine = p_data.Split('\n');

		string[] _keyData = _allLine[_startCount].Split('\t');
		string[] _typeKey = new string[_keyData.Length];

		for (int i = 0; i < _keyData.Length; i++) {
			_keyData [i] = Regex.Replace (_keyData [i], @"[^a-zA-Z0-9]", "");
			_typeKey [i] = _keyData [i];
		}

		for(int i=_startCount+1; i< _allLine.Length; i++){
			char[] _chardata = _allLine[i].ToCharArray();

			if( _chardata.Length < 1) continue;

			string[] _strData = _allLine[i].Split('\t');

			int _length = _strData.Length;
			Dictionary<string, object> _data = new Dictionary<string, object> ();

			for (int j = 0; j < _typeKey.Length; j++) {
				_strData[j] = Regex.Replace(_strData[j], "\\s", "");
				_data.Add (_typeKey [j], _strData [j]);
			}
			data_2.Add (_strData [0], _data);
		}

		data.Add (m_file[p_number].m_name, data_2);
	}

	private void DoParseDefineData(int p_number, string p_data){
		Dictionary<string, Dictionary<string, object>> _data_2 = new Dictionary<string, Dictionary<string, object>>();

		string[] _allLine = p_data.Split('\n');
		string[] _key = null;
		//紀錄 key
		for(int i=0; i< _allLine.Length; i++){
			string _str = _allLine[i].ToLower();
			if(_str.Contains("[define]")) 
				continue;

			_key = _allLine[i].Split('\t');
			break;
		}

		//DefineData
		int _count = 0;

		for(int i=0; i< _allLine.Length; i++){
			string _str = _allLine[i].ToLower();
			if(_str.Contains("[define]")){
				string[] _strData = _allLine[i].Split('\t');
				Dictionary<string, object> _data = new Dictionary<string, object> ();

				for(int j=0; j<_strData.Length; j++){
					_strData[j] = Regex.Replace(_strData[j], "\\s", "");
					_data.Add (_key [j], _strData [j]);
				}

				_data_2.Add(_count.ToString(), _data);
				_count++;
			}
		}

		defineData.Add(m_file[p_number].m_name, _data_2);
	}
}