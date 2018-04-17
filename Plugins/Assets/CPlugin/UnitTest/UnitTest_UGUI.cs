using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;

public class UnitTest_UGUI : MonoBehaviour {
	[SerializeField]private TextAsset testData;
	[SerializeField]private TestRecordJson recordJsonData;
	[SerializeField]private TestRecordJson playRecordJsonData;
	private bool isRecord = false;
	/******************/

	void Start () {
		recordJsonData = new TestRecordJson();
	}

	void Update () {
		if(Input.GetKeyUp(KeyCode.R)){
			StartCoroutine(record());
		}

		if(Input.GetKeyUp(KeyCode.S)){
			StopRecord();
		}

		if(Input.GetKeyUp(KeyCode.P)){
			StartCoroutine(PlayRecord());
		}
	}

	IEnumerator PlayRecord(){
		var timer = System.Diagnostics.Stopwatch.StartNew();
		testData = Resources.Load("UnitTest/TestData") as TextAsset;
		playRecordJsonData = JsonUtility.FromJson<TestRecordJson>(testData.text);

		int _Testcount = 0;
		while(_Testcount < playRecordJsonData._data.Count){
			if(timer.Elapsed.TotalSeconds > playRecordJsonData._data[_Testcount].time){
				play(playRecordJsonData._data[_Testcount].postion);
				_Testcount++;
			}
			yield return null;
		}
		print("測試結束");
	}

	private void StopRecord(){
		isRecord = false;
		StreamWriter _writer = new StreamWriter(Application.dataPath + "/Resources/UnitTest/" + testData.name + ".txt");
		_writer.WriteLine(JsonUtility.ToJson(recordJsonData));
		_writer.Close();
		print("儲存的紀錄 : " + JsonUtility.ToJson(recordJsonData));
	}


	IEnumerator record()
	{
		if(isRecord) yield break;

		isRecord = true;
		var timer = System.Diagnostics.Stopwatch.StartNew();
		while (isRecord){
			yield return null;
			if (!Input.GetMouseButtonDown(0)){
				continue;
			}

			var go = EventSystem.current.currentSelectedGameObject;
			if (go == null){
				continue;
			}

			var p = Input.mousePosition;
			Debug.Log(string.Format("座標={0}x{1} 物件名稱={2} 經過時間={3}", p.x, p.y, go.name, timer.Elapsed.TotalSeconds));
			TestRecord _TestRecord = new TestRecord(new Vector2(p.x, p.y) , timer.Elapsed.TotalSeconds);
			recordJsonData._data.Add(_TestRecord);
		}
	}

	void play(Vector2 p_postion)
	{
		
		var ev = new PointerEventData(EventSystem.current);
		var results = new List<RaycastResult>();
		ev.position = p_postion;
		ev.button = PointerEventData.InputButton.Left;
		EventSystem.current.RaycastAll(ev, results);

		foreach (var result in results)
		{
			//EventTrigger
			if(result.gameObject.GetComponent<EventTrigger>() != null){
				result.gameObject.GetComponent<EventTrigger>().OnPointerUp(null);
				result.gameObject.GetComponent<EventTrigger>().OnPointerDown(null);
			}

			ExecuteEvents.Execute<IPointerClickHandler>(
				result.gameObject,
				ev,
				(handler, eventData) => handler.OnPointerClick((PointerEventData)eventData));
		}
	}

	public void Hi(){
		print("Hi");
	}

	public void Click(){
		print("Click");
	}

	public void Up(int p_number){
		print("Up " +p_number);
	}

	[System.Serializable]
	public class TestRecordJson{
		public List<TestRecord> _data;

		public TestRecordJson(){
			_data = new List<TestRecord>();
		}
	}

	[System.Serializable]
	public class TestRecord{
		public Vector2 postion;
		public double time;

		public TestRecord(Vector2 p_Postion, double p_time){
			time = p_time;
			postion = p_Postion;
		}
	}
}
