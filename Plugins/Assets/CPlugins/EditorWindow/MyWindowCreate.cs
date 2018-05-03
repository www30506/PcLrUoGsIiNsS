using UnityEditor;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

public class MyWindowCreate : EditorWindow{
	private int key = 0;
	private int maintype = 1;
	private int secondtype = 2;
	private int txtname = 3;
	private int txtpath = 4;
	private GUISkin guiskin;
	private List<List<string>> horizontalList = new List<List<string>>();
	private enum SaveStatue{None, Error, Compelte};
	private SaveStatue saveStatue = SaveStatue.None;

	void Awake(){
		guiskin = Resources.Load("EditorWindow/EditorWindow_GUISkin") as GUISkin;
		CreateData();
	}

	private void CreateData(){
		FileStream file = new FileStream(Application.streamingAssetsPath + "/Data/EditorWindowMain.txt", FileMode.Open, FileAccess.Read);
		StreamReader sr = new StreamReader(file);

		string _str = sr.ReadLine();

		while(string.IsNullOrEmpty(_str) == false){
			
			List<string> _verticalList = new List<string>();
			string[] _strArray = Regex.Split(_str, "\\t");

			for(int i=0; i<_strArray.Length; i++){
				_verticalList.Add(_strArray[i]);
			}

			horizontalList.Add(_verticalList);

			_str = sr.ReadLine();
		}
		sr.Close();
		file.Close();
	}

	[MenuItem("Window/MyWindowCreate")]
	public static void ShowMyWindowCreate(){
		MyWindowCreate window = (MyWindowCreate)EditorWindow.GetWindow(typeof(MyWindowCreate));
		window.Show();
	}

	void OnGUI(){
		if(saveStatue == SaveStatue.Compelte){
			EditorUtility.DisplayDialog("系統", "存檔成功", "Reset");
			saveStatue = SaveStatue.None;
		}
		else if (saveStatue == SaveStatue.Error){
			EditorUtility.DisplayDialog("系統", "存檔失敗", "Reset");
			saveStatue = SaveStatue.None;
		}

		CreateType();
		CreateHorizontal();
		SaveBtn();
		AddDataBtn();
	}

	private void AddDataBtn(){
		if(GUI.Button(new Rect(0, (horizontalList.Count)*50+20, 1000, 30), "新增資料")){
			AddData();
		}
	}

	private void AddData(){
		List<string> _newData = new List<string>();
		for(int i=0; i<horizontalList[horizontalList.Count-1].Count; i++){
			if(i == key){
				_newData.Add((int.Parse(horizontalList[horizontalList.Count-1][i])+1).ToString());
			}
			else{
				_newData.Add("");
			}
		}

		horizontalList.Add(_newData);
	}

	private void SaveBtn(){
		if(GUI.Button(new Rect(horizontalList[0].Count * 150 +250 , 10, 200,200), "存檔",guiskin.GetStyle("Button"))){
			SaveData();
		}
	}

	private void SaveData(){
		saveStatue = SaveStatue.Error;
		if(File.Exists(Application.streamingAssetsPath + "/Data/EditorWindowMain.txt")){
			File.Delete(Application.streamingAssetsPath + "/Data/EditorWindowMain.txt");
		}
		FileStream file = new FileStream(Application.streamingAssetsPath + "/Data/EditorWindowMain.txt", FileMode.Create, FileAccess.Write);
		StreamWriter sw = new StreamWriter(file, Encoding.UTF8);

		for(int i=0;i<horizontalList.Count;i++){
			string _writeLineStr = "";
			for(int j=0; j<horizontalList[i].Count; j++){
				if(j !=0){
					_writeLineStr +="\t";
				}
				_writeLineStr += horizontalList[i][j];
			}
			sw.WriteLine(_writeLineStr);
		}
		sw.Close();
		file.Close();
		saveStatue = SaveStatue.Compelte;
	}

	private void CreateType(){
		for(int i=0; i<horizontalList[0].Count; i++){
			Rect _rect = new Rect(i*150+50, 10, 140, 45);
			GUI.Box(_rect, horizontalList[0][i], guiskin.GetStyle("Box"));
		}
		GUI.Box(new Rect(horizontalList[0].Count*150+50, 10, 140, 45), "Change", guiskin.GetStyle("Box"));
	}

	private void CreateHorizontal(){
		for(int i=1; i<horizontalList.Count; i++){
			CreateVertical(i);
		}
	}

	private void CreateVertical(int p_row){
		for(int i=0; i< horizontalList[p_row].Count; i++){
			Rect _rect = new Rect(i * 150+50, p_row*50+20, 140, 45);

			if(i == maintype || i == secondtype){
				string _tempStr = horizontalList[p_row][i];
				_tempStr = EditorGUI.TextField(_rect, _tempStr, guiskin.GetStyle("M_TextField"));
				horizontalList[p_row][i] = _tempStr;
			}
			else{
				GUI.Label(_rect, horizontalList[p_row][i] ,guiskin.GetStyle("M_Label"));
			}
		}
		//刪除按鈕
		if(p_row !=1){
			if(GUI.Button(new Rect(0, p_row*50+20, 45, 45), "X")){
				DeleteHorizontalList(p_row);
			}
		}
		//txtPath按鈕
		try{
			if(GUI.Button(new Rect(horizontalList[p_row].Count*150+50, p_row*50+20, 140, 45), "Change")){
				OpenSelectFilePath(p_row);
			}
		}
		catch{
		}
	}

	private void DeleteHorizontalList(int p_row){
		horizontalList.RemoveAt(p_row);
		for(int i=p_row ; i<horizontalList.Count; i++){
			horizontalList[i][key] = (i).ToString();
		}
	}

	private void OpenSelectFilePath(int p_row){
		string path = EditorUtility.OpenFilePanel("Overwrite with png", "", "txt");
		if (path.Length != 0){

			string[] _path = Regex.Split(path, "/StreamingAssets/");
			string[] _str = Regex.Split(_path[1], "/");
			string _txtName = "";
			string _txtPath = "";

			for(int i=0; i<_str.Length -1 ;i++){
				if(i != 0){
					_txtPath +="/";
				}
				_txtPath += _str[i];	
			}

			_txtName = Regex.Replace(_str[_str.Length-1], ".txt", "");
			horizontalList[p_row][txtname] = _txtName;
			horizontalList[p_row][txtpath] = _txtPath;
		}
	}
}