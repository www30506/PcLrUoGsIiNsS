using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManerger_Example : MonoBehaviour {

	void Start () {
		
	}
		
	void Update () {
		if (Input.GetKeyUp (KeyCode.A)) {	
			AudioManerger.SetMusicVoice (0.2f);
		}

		if (Input.GetKeyUp (KeyCode.S)) {	
			AudioManerger.SetEffiectVoice (0.2f);
		}

		if (Input.GetKeyUp (KeyCode.D)) {	
			AudioManerger.PlayMusic (MusicType.Music_Example);
		}

		if (Input.GetKeyUp (KeyCode.F)) {	
			AudioManerger.PlayEffect (SoundEffectType.Effect_Example);
		}

		if (Input.GetKeyUp (KeyCode.G)) {	
			AudioManerger.StopMusic ();
		}
	}
}
