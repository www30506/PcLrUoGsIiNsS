using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicType{Music_Example};
public enum SoundEffectType{Effect_Example};

public class AudioManerger : MonoBehaviour {
	public static AudioManerger instance;
	[SerializeField]private AudioSource muisc;
	[SerializeField]private AudioSource effect;

	void Awake(){
		instance = this;
	}
		
	void Start () {
		
	}
	
	void Update () {
		
	}

	private static void CreateAudioManerger(){
		Instantiate (Resources.Load ("Plugins/AudioManerger/AudioManerger") as GameObject);
	}

	/// <summary>
	/// 設定音樂音量
	/// </summary>
	public static void SetMusicVoice(float p_voice){
		if (instance == null) {
			CreateAudioManerger ();
		}
		AudioManerger.instance.M_SetMusicVoice (p_voice);
	}

	public void M_SetMusicVoice(float p_voice){
		muisc.volume = p_voice;
	}

	/// <summary>
	/// 設定音效音量
	/// </summary>
	public static void SetEffiectVoice(float p_voice){
		if (instance == null) {
			CreateAudioManerger ();
		}
		AudioManerger.instance.M_SetEffiectVoice (p_voice);
	}

	public void M_SetEffiectVoice(float p_voice){
		effect.volume = p_voice;
	}


	/// <summary>
	/// 播放音樂
	/// </summary>
	public static void PlayMusic(MusicType p_type){
		if (instance == null) {
			CreateAudioManerger ();
		}
		AudioManerger.instance.M_PlayMusic (p_type);
	}

	public void M_PlayMusic(MusicType p_type){
		muisc.clip = Resources.Load ("Sounds/" + p_type.ToString ()) as AudioClip;
		muisc.Play ();
	}

	/// <summary>
	/// 停止音樂
	/// </summary>
	public static void StopMusic(){
		if (instance == null) {
			CreateAudioManerger ();
		}
		AudioManerger.instance.M_StopMusic();
	}

	public void M_StopMusic(){
		muisc.Stop ();
	}

	/// <summary>
	/// 播放音效
	/// </summary>
	/// <param name="p_type">P type.</param>
	public static void PlayEffect(SoundEffectType p_type){
		if (instance == null) {
			CreateAudioManerger ();
		}
		AudioManerger.instance.M_PlayEffect (p_type);
	}

	public void M_PlayEffect(SoundEffectType p_type){
		effect.clip = Resources.Load ("Sounds/" + p_type.ToString ()) as AudioClip;
		effect.Play ();
	}
}
