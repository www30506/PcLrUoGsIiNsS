using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BackgroundMusicType{First};
public enum SoundEffectType{First};

public class AudioManerger : MonoBehaviour {
	public static AudioManerger instance;
	AudioSource music;
	AudioSource soundEffect;

	void Awake(){
		instance = this;
		music = GameObject.Find ("Main Camera/BackgroundMusic").GetComponent<AudioSource>();
		soundEffect = GameObject.Find ("Main Camera/SoundEffect").GetComponent<AudioSource>();
	}
		
	void Start () {
		
	}
	
	void Update () {
		
	}

	/// <summary>
	/// 播放音樂
	/// </summary>
	public static void PlayMusic(BackgroundMusicType p_type){
		AudioManerger.instance.M_PlayMusic (p_type);
	}

	public void M_PlayMusic(BackgroundMusicType p_type){
		music.clip = Resources.Load ("Sounds/" + p_type.ToString ()) as AudioClip;
		music.Play ();
	}

	/// <summary>
	/// 停止音樂
	/// </summary>
	public static void StopMusic(){
		AudioManerger.instance.M_StopMusic();
	}

	public void M_StopMusic(){
		music.Stop ();
	}

	/// <summary>
	/// 播放音效
	/// </summary>
	/// <param name="p_type">P type.</param>
	public static void PlaySoundEffect(SoundEffectType p_type){
		AudioManerger.instance.M_PlaySoundEffect (p_type);
	}

	public void M_PlaySoundEffect(SoundEffectType p_type){
		soundEffect.clip = Resources.Load ("Sounds/" + p_type.ToString ()) as AudioClip;
		soundEffect.Play ();
	}
}
