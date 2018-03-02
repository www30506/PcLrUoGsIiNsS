using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

#if UNITY_EDITOR
using UnityEditor;

public class MyWindowEditor : EditorWindow {
	private GameObject PD_Obj;
	private string[] MainTypes;
	private int MainTypeNumber = 0;
	private List<string[]> SecondTypes;
	private List<string[]> secondtxtName;
	private int SecondTypeNumber = 0;
	private string[][][] secondData;
	private GUIStyle nullGUIStyle;
	private GUISkin guiskin;
	private List<List<List<string>>> secondDataList = new List<List<List<string>>>();
	private List<string> MainKeyList = new List<string>(); //拿來給secondDataList查詢是哪一個dictionary
	private int contentWidth = 150; //元件寬度
	private int contentHeigt = 40; //元件高度
	private static int WindowWidth = 1920; //視窗寬度
	private static int WindowHeigh = 1030; //視窗高度

	void Awake(){
		nullGUIStyle = new GUIStyle();
		nullGUIStyle = GUIStyle.none;
		CreatePD();
		Init_PD_MainData();
		Init_PD_SecondData();
		InitMainTyps();
		InitSecondTyps();
		InitSecondData();
		guiskin = Resources.Load("EditorWindow/EditorWindow_GUISkin") as GUISkin;
	}

	private void CreatePD(){
		if(PD_Obj == null){
			PD_Obj = new GameObject("EditorWindow");
			PD_Obj.AddComponent<PD>();
		}
	}

	private void Init_PD_MainData(){
		PD_Obj.GetComponent<PD>().SetPDTxt(new string[1]{"EditorWindowMain"}, new string[1]{"Data"});
	}

	private void Init_PD_SecondData(){
		string[] _name = new string[PD.DATA["EditorWindowMain"].Count+1];
		string[] _path = new string[PD.DATA["EditorWindowMain"].Count+1];

		_name[0] = "EditorWindowMain";
		_path[0] = "Data";
		for(int i=1; i <_name.Length; i++){
			_name[i] = PD.DATA["EditorWindowMain"][(i).ToString()]["txtname"].ToString();
			_path[i] = PD.DATA["EditorWindowMain"][(i).ToString()]["txtpath"].ToString();
		}

		PD_Obj.GetComponent<PD>().SetPDTxt(_name, _path);
	}

	private void InitMainTyps(){
		List<string> _types = new List<string>();

		for(int i=0; i< PD.DATA["EditorWindowMain"].Count; i++){
			string _type = PD.DATA["EditorWindowMain"][(i+1).ToString()]["maintype"].ToString();
			if(_types.Contains(_type) == false){
				_types.Add(_type);
			}
		}

		MainTypes = new string[_types.Count];
	
		for(int i=0; i<_types.Count; i++){
			MainTypes[i] = _types[i];
		}
	}

	private void InitSecondTyps(){
		SecondTypes = new List<string[]>();
		secondtxtName = new List<string[]>();

		for(int i=0; i< MainTypes.Length; i++){
			List<string> _typeList = new List<string>();
			List<string> _nameList = new List<string>();

			for(int j=0; j< PD.DATA["EditorWindowMain"].Count; j++){
				if(PD.DATA["EditorWindowMain"][(j+1).ToString()]["maintype"].ToString() == MainTypes[i]){
					_typeList.Add(PD.DATA["EditorWindowMain"][(j+1).ToString()]["secondtype"].ToString());
					_nameList.Add(PD.DATA["EditorWindowMain"][(j+1).ToString()]["txtname"].ToString());
				}
			}

			SecondTypes.Add(_typeList.ToArray());
			secondtxtName.Add(_nameList.ToArray());
		}
	}


	private void InitSecondData(){
		foreach(string key_I in PD.DATA.Keys){
			List<List<string>> _list_data_II= new List<List<string>>();

			foreach(string key_II in PD.DATA[key_I].Keys){
				List<string> _list_data_III = new List<string>();

				foreach(string key_III in PD.DATA[key_I][key_II].Keys){
					_list_data_III.Add(PD.DATA[key_I][key_II][key_III].ToString());

				}

				_list_data_II.Add(_list_data_III);
			}

			MainKeyList.Add(key_I);
			secondDataList.Add(_list_data_II);
		}
	}

	void Update () {
	}

	void OnDestroy(){
		//EditorWindow 關閉時會呼叫
		if(PD_Obj != null){
			if(PD.DATA != null){
				PD.DATA = null;
				Debug.Log("釋放 PD.DATA");
			}

			DestroyImmediate(PD_Obj);
		}

		EditorStyles.popup.fontSize = 10;
		EditorStyles.popup.fixedHeight = 15;
	}

	[MenuItem("Window/MyWindowEditor %#g")]
	public static void ShowWindow(){
		MyWindowEditor window = (MyWindowEditor)EditorWindow.GetWindowWithRect(typeof(MyWindowEditor), new Rect(0, 0, MyWindowEditor.WindowWidth, MyWindowEditor.WindowHeigh));
		EditorStyles.popup.fontSize = 12;
		EditorStyles.popup.fixedHeight = 30;
		window.Show();
	}

	public string tempStr;
	private int tempInt;
	private Vector2 scrollPosition = Vector2.zero;

	private Color tempColor = Color.white;

	void OnGUI(){
		ShowMainType();
		ShowSecondType();
		ShowData();

		if(GUI.Button(new Rect(0,MyWindowEditor.WindowHeigh - 100,MyWindowEditor.WindowWidth,90), "存檔",guiskin.GetStyle("Button"))){
			SaveData();
		}
	}

	private int preMainTypeNumber;
	private int preSeconTypeNumber;

	private void ShowMainType(){
		preMainTypeNumber = MainTypeNumber;
		MainTypeNumber = GUI.Toolbar(new Rect(0, 0, MyWindowEditor.WindowWidth, 50), MainTypeNumber, MainTypes, guiskin.GetStyle("Toolbar"));
		if(preMainTypeNumber != MainTypeNumber){
			SecondTypeNumber = 0;
			EditorGUI.FocusTextInControl("");
		}
	}

	private void ShowSecondType(){
		preSeconTypeNumber = SecondTypeNumber;
		SecondTypeNumber = GUI.Toolbar(new Rect(0, 60, MyWindowEditor.WindowWidth, 50), SecondTypeNumber, SecondTypes[MainTypeNumber], guiskin.GetStyle("Toolbar"));
		if(preSeconTypeNumber != SecondTypeNumber){
			EditorGUI.FocusTextInControl("");
		}
	}

	private void ShowData(){
		string _txtname = secondtxtName[MainTypeNumber][SecondTypeNumber];	
		int _txtnameNumber = MainKeyList.IndexOf(_txtname);
		int _W = secondDataList[_txtnameNumber][0].Count * contentWidth;
		int _H = (secondDataList[_txtnameNumber].Count) * contentHeigt;

		GUI.BeginScrollView(new Rect(20,140,MyWindowEditor.WindowWidth - 40,50), scrollPosition, new Rect(0,0,_W+10,0),false, true, nullGUIStyle, nullGUIStyle);
		//顯示名稱並記錄型態
		List<string> _types = new List<string>();
		foreach(string key in PD.DEFINEDATA[_txtname].Keys){
			foreach(string key_II in PD.DEFINEDATA[_txtname][key].Keys){
				if(PD.DEFINEDATA[_txtname][key][key_II].ToString().Contains("name")){
					int _count = 0;
					foreach(string _key_II in PD.DEFINEDATA[_txtname][key].Keys){
						Rect _rect = new Rect(_count * contentWidth, 0, contentWidth -10, 30);
						string _str = PD.DEFINEDATA[_txtname][key][_key_II].ToString();
						_str = Regex.Replace(_str, @"\[define\]", "");
						CreateBox(_rect, _str);
						_count++;
					}
				}
				else if(PD.DEFINEDATA[_txtname][key][key_II].ToString().Contains("type")){
					foreach(string _key_II in PD.DEFINEDATA[_txtname][key].Keys){
						_types.Add(PD.DEFINEDATA[_txtname][key][_key_II].ToString());
					}
				}
				break;
			}
		}

		GUI.EndScrollView();

		scrollPosition = GUI.BeginScrollView(new Rect(20,200,MyWindowEditor.WindowWidth -40 ,MyWindowEditor.WindowHeigh - 320), scrollPosition, new Rect(0,0,_W,_H));
		for(int i=0; i<secondDataList[_txtnameNumber].Count; i++){
			for(int j = 0; j<secondDataList[_txtnameNumber][i].Count; j++){
				Rect _rect = new Rect(j*contentWidth, (i)*contentHeigt, contentWidth-10, 30);
				if(_types[j].Contains("type")){
					CreateLabel(_rect, secondDataList[_txtnameNumber][i][j]);
				}
				else if(_types[j].Contains("label")){
					CreateLabel(_rect, secondDataList[_txtnameNumber][i][j]);
				}
				else if(_types[j].Contains("string")){
					CreateStringTextField(_rect, _txtnameNumber, i, j);
				}
				else if(_types[j].Contains("int")){
					CreateIntTextField(_rect, _txtnameNumber, i, j);
				}
				else if(_types[j].Contains("toggle")){
					CreateToggle(_rect,_txtnameNumber, i, j);
				}
				else if(_types[j].Contains("option")){
					string _allOptions = _types[j];
					string _replace = Regex.Replace(_allOptions, "option", "");
					_replace = Regex.Replace(_replace, "{", "");
					_replace = Regex.Replace(_replace, "}", "");

					string[] _options = Regex.Split(_replace, ",");
					CreateOption(_rect,_txtnameNumber, i, j, _options);
				}
				else if(_types[j].Contains("color")){
					CreateColor(_rect, _txtnameNumber, i, j);
				}
				else{
					Debug.LogError("存在未定義的類別 : " + _types[j]);
				}
			}
		}

		GUI.EndScrollView();
	}

	private void CreateColor(Rect p_rect, int p_txtName, int p_key_I, int p_key_II){
		tempStr = secondDataList[p_txtName][p_key_I][p_key_II].ToString();
		ColorUtility.TryParseHtmlString("#"+tempStr, out tempColor);
		tempColor = EditorGUI.ColorField(p_rect, tempColor);
		tempStr = ColorUtility.ToHtmlStringRGBA(tempColor);
		secondDataList[p_txtName][p_key_I][p_key_II] = tempStr;
	}

	private void CreateLabel(Rect p_rect, string p_data){
		GUI.Label(p_rect, p_data,guiskin.GetStyle("M_Label"));
	}

	private void CreateBox(Rect p_rect, string p_data){
		GUI.Box(p_rect, p_data, guiskin.GetStyle("Box"));
	}

	private void CreateStringTextField(Rect p_rect, int p_txtName, int p_key_I, int p_key_II){
		tempStr = secondDataList[p_txtName][p_key_I][p_key_II].ToString();
		tempStr = EditorGUI.TextField(p_rect, tempStr, guiskin.GetStyle("M_TextField"));
		secondDataList[p_txtName][p_key_I][p_key_II] = tempStr;
	}

	private void CreateToggle(Rect p_rect, int p_txtName, int p_key_I, int p_key_II){
		bool _toggle = false;
		string _value = secondDataList[p_txtName][p_key_I][p_key_II].ToString().ToLower();

		_toggle = (_value == "true")? true:false;
		_toggle = GUI.Toggle(p_rect, _toggle, _value, guiskin.GetStyle("M_Toggle"));

		secondDataList[p_txtName][p_key_I][p_key_II] = _toggle?"True": "False";
	}

	private void CreateIntTextField(Rect p_rect, int p_txtName, int p_key_I, int p_key_II){
		tempStr = secondDataList[p_txtName][p_key_I][p_key_II].ToString();
		tempStr = GUI.TextField(p_rect, tempStr,guiskin.GetStyle("M_TextField"));
		tempStr = Regex.Replace(tempStr, "[^0-9]", "");
		secondDataList[p_txtName][p_key_I][p_key_II] = tempStr;
	}

	private void CreateOption(Rect p_rect, int p_txtName, int p_key_I, int p_key_II, string[] p_options){
		int _number = 0;
		string _data = secondDataList[p_txtName][p_key_I][p_key_II].ToString();
		for(int i=0; i< p_options.Length; i++){
			if(_data == p_options[i]){
				_number = i;
				break;
			}
		}

		tempInt = _number;
		tempInt = EditorGUI.Popup(p_rect, tempInt, p_options);
		secondDataList[p_txtName][p_key_I][p_key_II] = p_options[tempInt].ToString();
	}

	private void SaveData(){
		Debug.Log("保存");

		for(int i=0; i<MainKeyList.Count; i++){
			int j=0;
			Dictionary<string, Dictionary<string, object>> _data = new Dictionary<string, Dictionary<string, object>>();
			foreach(string key_II in PD.DATA[MainKeyList[i]].Keys){
				Dictionary<string, object> _data_III = new Dictionary<string, object>();
				int k=0;
				foreach(string Key_III in PD.DATA[MainKeyList[i]][key_II].Keys){
					_data_III.Add(Key_III, secondDataList[i][j][k]);

					k++;
				}
				_data.Add(key_II, _data_III);
				j++;
			}
			PD.Save(_data, MainKeyList[i]);
		}
	}
}
#endif