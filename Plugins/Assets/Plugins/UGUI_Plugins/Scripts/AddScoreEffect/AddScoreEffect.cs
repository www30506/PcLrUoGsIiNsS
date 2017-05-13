using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddScoreEffect : MonoBehaviour {
	private Text scoreText;
	private int targetScore;  //要執行特效的目標分數
	private int nowScore;
	public float changeScoreTime = 1.0f;
	private float tempTime = 0.0f;
	private bool doEffect = false;
	private int baseScore = 0;

	void Awake(){
		scoreText = this.gameObject.GetComponent<Text> ();
	}


	void Start () {
		scoreText.text = "0";
		nowScore = int.Parse(scoreText.text);
		targetScore = 0;
	}


	void LateUpdate () {
		if (IsTargetScoreChange() && doEffect == false) {
			doEffect = true;
		}
			
		if (doEffect == true) {
			if (IsTargetScoreChange ()) {
				DataInit ();
			}

			if(nowScore < targetScore){
				DoAddScoreEffect ();
			}
			else if(nowScore > targetScore){
				DoReduceScoreEffect ();
			}
		}
	}

	private bool IsTargetScoreChange(){
		if (nowScore != int.Parse (scoreText.text))
			return true;
		return false;
	}

	private void DataInit(){
		targetScore = int.Parse (scoreText.text);
		tempTime = 0.0f;
		baseScore = nowScore;
	}

	private void DoAddScoreEffect(){
		tempTime += Time.deltaTime;

		if (tempTime < changeScoreTime) {
			nowScore = baseScore + (int)((targetScore - baseScore) * (tempTime / changeScoreTime));
			scoreText.text = nowScore.ToString ();
		}
		else {
			scoreText.text = targetScore.ToString ();
			nowScore = int.Parse (scoreText.text);
			doEffect = false;
			tempTime = 0.0f;
		}
	}


	private void DoReduceScoreEffect(){
		tempTime += Time.deltaTime;

		if (tempTime < changeScoreTime) {
			nowScore = baseScore - (int)((baseScore - targetScore) * (tempTime / changeScoreTime));
			scoreText.text = nowScore.ToString ();
		}
		else {
			scoreText.text = targetScore.ToString ();
			nowScore = int.Parse (scoreText.text);
			doEffect = false;
			tempTime = 0.0f;
		}
	}
}
