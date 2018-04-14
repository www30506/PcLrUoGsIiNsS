using UnityEngine;
using System.Collections;

[AddComponentMenu("UI/Tween/TweenColor")]
public class UTweenColor : UTweener {
	public Color Form = new Color(1,1,1,1);
	public Color To = new Color(0,0,0,1);
	protected translateDelegate[] translateType = new translateDelegate[3];
	private Color distanceVector, tempVector;
	private CanvasRenderer crd;
	
	void Start(){
		translateType [0] = new translateDelegate(Once);
		translateType [1] = new translateDelegate(Loop);
		translateType [2] = new translateDelegate(PingPong);
		distanceVector = To - Form;
		crd = this.GetComponent<CanvasRenderer> ();
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
		tempVector = distanceVector * Curve.Evaluate(time * percent);
		tempVector = Form + tempVector;
		crd.SetColor(tempVector) ;

		if (time > Duration) {
			start = false;
			crd.SetColor(To);
			OnFinished();
			this.enabled = false;
		}
	}
	
	public void Loop(){
		tempVector = distanceVector * Curve.Evaluate (time * percent);
		tempVector = Form + tempVector;
		crd.SetColor (tempVector);
		
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
		
		crd.SetColor (tempVector);
		if (time > Duration) {
			time = 0;
			pingpong = !pingpong;
		}
	}
}
