using UnityEngine;
using System.Collections;

[AddComponentMenu("UI/Tween/TweenPosition")]
public class UTweenPosition : UTweener {
	protected translateDelegate[] translateType = new translateDelegate[3];
	public Vector3 Form = new Vector3(1,1,1), To = new Vector3(1,1,1);
	protected Vector3 distanceVector, tempVector;

	void Start(){
		translateType [0] = new translateDelegate(Once);
		translateType [1] = new translateDelegate(Loop);
		translateType [2] = new translateDelegate(PingPong);
		distanceVector = To - Form;
	}

	void LateUpdate () {
		if (start) {
			Translate ();
		}
	}
	
	private void Translate(){
		time += ignoreTimeScale? Time.unscaledDeltaTime : Time.deltaTime;
		translateType [(int)loopType]();
	}

	public delegate void translateDelegate();
	public void Once(){
		tempVector = distanceVector * Curve.Evaluate(time*percent);
		tempVector = Form + tempVector;
		myRectTransfrom.anchoredPosition = tempVector ;
		
		if (time > Duration) {
			start = false;
			myRectTransfrom.anchoredPosition = To;
			OnFinished();
			this.enabled = false;
		}
	}
	
	public void Loop(){
		tempVector = distanceVector * Curve.Evaluate (time * percent);
		tempVector = Form + tempVector;
		myRectTransfrom.anchoredPosition = tempVector;
		
		if (time > Duration) {
			time = 0;
		}
	}
	
	public void PingPong(){
		if (pingpong) 
			tempVector = distanceVector * Curve.Evaluate ((Duration - time) * percent);
		else 
			tempVector = distanceVector * Curve.Evaluate (time * percent);
		
		tempVector = Form + tempVector;
		
		myRectTransfrom.anchoredPosition = tempVector;
		if (time > Duration) {
			time = 0;
			pingpong = !pingpong;
		}
	}
}
