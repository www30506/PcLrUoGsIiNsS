using UnityEngine;
using System.Collections;


[AddComponentMenu("UI/Tween/TweenAlpha")]
public class UTweenAlpha : UTweener {
	[Range(0f,1f)]
	public float Form;
	[Range(0f,1f)]
	public float To;
	protected translateDelegate[] translateType = new translateDelegate[3];
	private float distanceVector, tempVector;
	private CanvasRenderer crd;
	private CanvasGroup canvasGroup;
	private enum AlphaType{CanvasRenderer, CanvasGroup}
	[SerializeField]private AlphaType alphaType = AlphaType.CanvasRenderer;

	void Start(){
		translateType [0] = new translateDelegate(Once);
		translateType [1] = new translateDelegate(Loop);
		translateType [2] = new translateDelegate(PingPong);
		distanceVector = To - Form;

		switch (alphaType) {
		case AlphaType.CanvasGroup:
			canvasGroup = this.GetComponent<CanvasGroup> ();
			break;

		case AlphaType.CanvasRenderer:
			crd = this.GetComponent<CanvasRenderer> ();
			break;
		}
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
		SetAlpha(tempVector) ;
		
		if (time > Duration) {
			start = false;
			SetAlpha(To);
			OnFinished();
			this.enabled = false;
		}
	}
	
	public void Loop(){
		tempVector = distanceVector * Curve.Evaluate (time * percent);
		tempVector = Form + tempVector;
		SetAlpha(tempVector) ;
		
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
		SetAlpha(tempVector) ;

		if (time > Duration) {
			time = 0;
			pingpong = !pingpong;
		}
	}

	private void SetAlpha(float p_value){
		switch (alphaType) {
		case AlphaType.CanvasGroup:
			canvasGroup.alpha = p_value;
			break;

		case AlphaType.CanvasRenderer:
			crd.SetAlpha (p_value);
			break;
		}
	}
}
