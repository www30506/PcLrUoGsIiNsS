using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasScaler))]
[RequireComponent(typeof(GraphicRaycaster))]
public abstract class Page_Base : MonoBehaviour {
	[HideInInspector]public bool isClosing = false;
	[HideInInspector]public bool isOpening = false;
	public Canvas canvas;
	private Vector2 prePosition;
	private RectTransform m_RectTransform;

	void Awake(){
		m_RectTransform = this.GetComponent<RectTransform> ();
		prePosition = m_RectTransform.anchoredPosition;
	}
	/// <summary>
	///  返回上一頁
	/// </summary>
	public void BackPage(){
		PageManerger.BackPage ();
	}

	/// <summary>
	/// 關閉該頁面
	/// 由PageManerger呼叫
	/// </summary>
	public void Close(){
		StartCoroutine (IE_Close());
	}

	IEnumerator IE_Close(){
		if (canvas == null) {
			canvas = this.GetComponent<Canvas> ();
		}

		isClosing = true;
		OnClose ();
		yield return StartCoroutine (IE_OnClose ());
		canvas.enabled = false;
		m_RectTransform.anchoredPosition = prePosition;
		isClosing = false;
	}


	/// <summary>
	/// 當關閉頁面觸發
	/// 給繼承複寫用
	/// </summary>
	protected virtual void OnClose(){
	}

	protected virtual IEnumerator IE_OnClose(){
		if (1 == 2) {
			yield return null;
		}
	}

	/// <summary>
	/// 開啟該頁面
	/// 由PageManerger呼叫
	/// </summary>
	public void Open(){
		StartCoroutine (IE_Open ());
	}

	IEnumerator IE_Open(){
		if (canvas == null) {
			canvas = this.GetComponent<Canvas> ();
		}
		canvas.enabled = true;

		isOpening = false;
		OnOpen ();
		yield return StartCoroutine (IE_OnOpen ());
		isOpening = true;
	}

	/// <summary>
	/// 當開啟頁面觸發
	/// 給繼承複寫用
	/// </summary>
	protected virtual void OnOpen(){
	
	}

	protected virtual IEnumerator IE_OnOpen(){
		if (1 == 2) {
			yield return null;
		}
	}
}
