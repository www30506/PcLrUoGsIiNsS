﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BeginnerGuide : MonoBehaviour,IPointerClickHandler ,IPointerDownHandler,IPointerUpHandler {
	[SerializeField]private Button[] target;

	private Material maskMaterial;
	private Vector4 center;
	private float diameter; // 直径
	private float current =0f;
	private int targetIndex = 0;
	float yVelocity = 0f;
	private Canvas canvas;
	Vector3[] corners = new Vector3[4]; 

	void Awake (){
		canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
		ShowTarget();
	}

	void Update () {
		float value = Mathf.SmoothDamp(current, diameter, ref yVelocity, 0.3f);
		if (!Mathf.Approximately (value, current)) {
			current = value;
			maskMaterial.SetFloat ("_Silder", current);
		}
	}

	private void ShowTarget(){
		target[targetIndex].GetComponent<RectTransform>().GetWorldCorners (corners);
		diameter = Vector2.Distance (WordToCanvasPos(canvas,corners [0]), WordToCanvasPos(canvas,corners [2])) / 2f;

		float x =corners [0].x + ((corners [3].x - corners [0].x) / 2f);
		float y =corners [0].y + ((corners [1].y - corners [0].y) / 2f);

		Vector3 center = new Vector3 (x, y, 0f);
		Vector2 position = Vector2.zero;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, center, canvas.GetComponent<Camera>(), out position);

		center = new Vector4 (position.x,position.y,0f,0f);
		maskMaterial = GetComponent<Image>().material;
//		maskMaterial = maskImage.material;
		maskMaterial.SetVector ("_Center", center);

		(canvas.transform as RectTransform).GetWorldCorners (corners);
		for (int i = 0; i < corners.Length; i++) {
			current = Mathf.Max(Vector3.Distance (WordToCanvasPos(canvas,corners [i]), center),current);
		}

		maskMaterial.SetFloat ("_Silder", current);
	}

	Vector2 WordToCanvasPos(Canvas canvas,Vector3 world){
		Vector2 position = Vector2.zero;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, world, canvas.GetComponent<Camera>(), out position);
		return position;
	}


	//监听按下
	public void OnPointerDown(PointerEventData eventData){
		PassEvent(eventData,ExecuteEvents.pointerDownHandler);
	}

	//监听抬起
	public void OnPointerUp(PointerEventData eventData){
		PassEvent(eventData,ExecuteEvents.pointerUpHandler);
	}

	//监听点击
	public void OnPointerClick(PointerEventData eventData){
		if(eventData.selectedObject.GetComponent<Button>().GetInstanceID() == target[targetIndex].GetInstanceID() && targetIndex < target.Length){
			PassEvent(eventData,ExecuteEvents.pointerClickHandler);
			targetIndex++;
			if(targetIndex < target.Length){
				ShowTarget();
			}
			else{
				CloseTarget();
			}
		}
	}

	private void CloseTarget(){
		this.gameObject.SetActive(false);
	}

	//把事件透下去
	public void  PassEvent<T>(PointerEventData data,ExecuteEvents.EventFunction<T> function) where T : IEventSystemHandler{
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(data, results); 
		GameObject current = data.pointerCurrentRaycast.gameObject ;

		for(int i =0; i< results.Count;i++){
			if(current!= results[i].gameObject){
				ExecuteEvents.Execute(results[i].gameObject, data,function);
				//RaycastAll后ugui会自己排序，如果你只想响应透下去的最近的一个响应，这里ExecuteEvents.Execute后直接break就行。
				break;
			}
		}
	}
}