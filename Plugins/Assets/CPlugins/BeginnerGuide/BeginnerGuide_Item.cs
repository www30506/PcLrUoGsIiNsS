using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginnerGuide_Item : MonoBehaviour {
	[SerializeField]private float textStartDelay;
	[SerializeField]private int wordSpeed = 5;
	[SerializeField]private string TextKey;
	[SerializeField]private Text descriptionText;

	private string descriptionStr;
	private bool changeText;
	private float wordIntervalsTime;
	private float tempTime;
	private int textIndex;

	void Start () {
		wordIntervalsTime = 1/(float)wordSpeed;
	}

	void OnEnable(){
		if(descriptionText != null){
			descriptionStr = TextKey;
			descriptionText.text = "";
			changeText = true;
			tempTime = 0 - textStartDelay;
			textIndex =0;
		}
	}

	void OnDiseable(){
		if(descriptionText != null){
			descriptionText.text = "";
			changeText = false;
		}
	}

	void Update () {
		if(changeText){
			tempTime += Time.deltaTime;

			if(tempTime > wordIntervalsTime){
				tempTime -=wordIntervalsTime;
				textIndex++;

				descriptionText.text = descriptionStr.Substring(0,textIndex);

				if(descriptionText.text == descriptionStr){
					changeText = false;
				}
			}
		}
	}
}
