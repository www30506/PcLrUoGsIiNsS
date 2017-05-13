using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CanvasGroup))]
public class DefaultClosePageAnim : MonoBehaviour {
	[SerializeField]private AnimationCurve curve;
	private CanvasGroup canvasGroup;
	public float duration = 0.5f;

	void Awake(){
		canvasGroup = this.GetComponent<CanvasGroup> ();
	}

	void Start () {
		
	}
	
	void Update () {
		
	}

	public void Play (){
		StartCoroutine (IE_Play ());
	}

	IEnumerator IE_Play(){
		float _time = 0.0f;
		while (_time < duration) {
			_time += Time.deltaTime;
			canvasGroup.alpha = curve.Evaluate(_time/duration);
			yield return null;
		}

		canvasGroup.alpha = curve.Evaluate(1);
		yield return null;
	}

}
