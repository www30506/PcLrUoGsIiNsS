using UnityEngine;
using System.Collections;

[AddComponentMenu("UI/Tween/TweenRotation")]
public class UTweenRotation : UTweener {
	protected translateDelegate[] RotateType = new translateDelegate[3];
	public Vector3 Form = new Vector3(1,1,1), To = new Vector3(1,1,1);
	protected Vector3 distanceVector, tempVector;
	public float startDelayTime = 0.0f;

	void Start(){
		RotateType [0] = new translateDelegate(Once);
		RotateType [1] = new translateDelegate(Loop);
		RotateType [2] = new translateDelegate(PingPong);
//		distanceVector = To - Form;
	}

	void OnEnable(){
		float _distance_1 = Mathf.Abs(To.z - Form.z);
		float _distance_2 = Mathf.Abs((To.z-360) - Form.z);
		float _distance_3 = Mathf.Abs(To.z - Form.z);
		float _distance_4 = Mathf.Abs(To.z - (Form.z-360));

		if (_distance_2 < _distance_1) {
			To = new Vector3 (To.x, To.y, To.z - 360);
		} else {
			if (_distance_4 < _distance_3) {
				To = new Vector3 (To.x, To.y, To.z + 360);
			}
		}

		distanceVector = To - Form;
	}

	void LateUpdate () {
		if (startDelayTime > 0) {
			startDelayTime -= Time.deltaTime;
			return;
		}

		if (start) {
			Rotation ();
		}
	}
	
	private void Rotation(){
		time += ignoreTimeScale? Time.unscaledDeltaTime : Time.deltaTime;
		RotateType [(int)loopType]();
	}
	
	public delegate void translateDelegate();
	public void Once(){
		tempVector = distanceVector * Curve.Evaluate(time*percent);
		tempVector = Form + tempVector;
		myRectTransfrom.localEulerAngles  = tempVector ;
		
		if (time > Duration) {
			start = false;
			myRectTransfrom.localEulerAngles = To;
			OnFinished();
			this.enabled = false;
		}
	}
	
	public void Loop(){
		tempVector = distanceVector * Curve.Evaluate (time * percent);
		tempVector = Form + tempVector;
		myRectTransfrom.localEulerAngles  = tempVector;
		
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
		
		myRectTransfrom.localEulerAngles  =tempVector;
		if (time > Duration) {
			time = 0;
			pingpong = !pingpong;
		}
	}
}
