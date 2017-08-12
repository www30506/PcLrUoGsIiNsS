using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelOverrideExample : MonoBehaviour {
	[SerializeField]private string testName;
	[LabelOverride("複寫過的名稱")][SerializeField]
	private string testNameOverride;

	[System.Serializable]
	public class TestLabelOverrideClass{
		[LabelOverride("名稱")]public string objectName;
	}

	[SerializeField]private TestLabelOverrideClass testLabelOverrideClass;

	[SerializeField]private List<TestLabelOverrideClass> testLabelOverrideClassList;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
