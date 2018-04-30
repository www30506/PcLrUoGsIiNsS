using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManerger_Example : MonoBehaviour {
	public Scrollbar musicScrollbar;
	public Scrollbar effectScrollbar;

	void Start () {
		
	}
		
	void Update () {
		if (Input.GetKeyUp (KeyCode.A)) {	
			AudioManerger.PlayMusic (MusicType.Music_Example);
		}
		if (Input.GetKeyUp (KeyCode.S)) {	
			AudioManerger.SetMusicVoice (musicScrollbar.value);
		}
		if (Input.GetKeyUp (KeyCode.D)) {	
			AudioManerger.StopMusic ();
		}


		if (Input.GetKeyUp (KeyCode.Q)) {	
			AudioManerger.PlayEffect (SoundEffectType.Effect_Example);
		}
		if (Input.GetKeyUp (KeyCode.W)) {	
			AudioManerger.SetEffiectVoice (effectScrollbar.value);
		}
	}
}
