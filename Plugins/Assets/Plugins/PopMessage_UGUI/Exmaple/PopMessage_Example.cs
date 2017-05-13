using UnityEngine;
using System.Collections;

public class PopMessage_Example : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			PopMessage.ConfirmMessage ("Title", "123");
		}
		if (Input.GetKeyDown (KeyCode.S)) {
//			PopMessage.ConfirmMessage ("1","2", "A", this.gameObject);
			PopMessage.YesNoMessage("title", "5678", C, B);
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			PopMessage.YesNoMessage("title", "1234", B, C);
//			PopMessage.ConfirmMessage ("3","4");
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			PopMessage.ConfirmMessage ("Title", "123", A);
		}
	}

	public void A(){
		print ("Click Confrim");
	}

	public void B(){
		print ("Click Yes");
	}
	public void C(){
		print ("Click No");
	}
}
