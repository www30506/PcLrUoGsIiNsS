using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseDataExample : MonoBehaviour {

	void Start () {
		print("====  讀取資料 ====");

		for(int i =0; i< PD.DATA ["Example"].Count; i++){
			print(PD.DATA ["Example"][(i+1).ToString()]["Name"] + " , " +  PD.DATA ["Example"][(i+1).ToString()]["Score"] + " , " +  PD.DATA ["Example"][(i+1).ToString()]["Description"]);
		}

		print("====  寫入資料 ====");
		print(PD.DATA ["Example"] ["1"] ["Name"]);
		PD.DATA ["Example"] ["1"] ["Name"] = "克羅";
		PD.DATA ["Example"] ["2"] ["Name"] = "希司";
		PD.DATA ["Example"] ["1"] ["Score"] = "10";
		PD.DATA ["Example"] ["2"] ["Score"] = "20";
		print(PD.DATA ["Example"] ["1"] ["Name"]);
		PD.Save(PD.DATA ["Example"], "Example");
	}

	void Update () {
		
	}
}
