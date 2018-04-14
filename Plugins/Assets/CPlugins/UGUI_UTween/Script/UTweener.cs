using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UTweener : MonoBehaviour {
	public enum LoopType {Once,Loop,PingPong};
	public LoopType loopType = LoopType.Once;
	public AnimationCurve Curve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
	public float Duration = 1.0f;
	public bool ignoreTimeScale =false;
	public UnityEvent OnFinishedEvent;

	protected RectTransform myRectTransfrom;
	protected float time, percent;
	protected bool start = true, pingpong = false;

	void Awake(){
		myRectTransfrom = this.GetComponent<RectTransform> ();
		percent = 1.0f / Duration;
		if (Duration <= 0)	start = false;
	}


	public void OnFinished(){
		OnFinishedEvent.Invoke ();
	}

	public void ReSetToStart(){
		start = true;
		time = 0.0f;
		if (Duration <= 0)	start = false;
	}
}
