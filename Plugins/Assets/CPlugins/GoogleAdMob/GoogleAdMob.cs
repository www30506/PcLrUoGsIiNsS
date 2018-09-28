using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class GoogleAdMob : MonoBehaviour{
	private int nowPlatform;
	[System.Serializable]private class AdMobData{
		public enum Platform{Android, IOS};
		public Platform platform;
		public string appId;
		public string rewardVideo_AdUnitId;
		public string Interstitial_AdUnitId;

		public AdMobData(Platform p_Platform){
			platform = p_Platform;
		}
	}

	[SerializeField]private bool testMode = false;
	[SerializeField]private AdMobData[] AdMobDatas = new AdMobData[]{new AdMobData(AdMobData.Platform.Android), new AdMobData(AdMobData.Platform.IOS)};

	public static GoogleAdMob instance;

	private RewardBasedVideoAd rewardBasedVideo;
	private InterstitialAd interstitial;
	private bool reloadInterstitial = false;
	private bool reloadrewardVideo = false;
	private float reloadTime = 30.0f;
	private float tempTime;


	void Awake(){
		#if UNITY_EDITOR
		this.gameObject.SetActive (false);
		#endif

		instance = this;
		DontDestroyOnLoad (this.gameObject);
	}

	public void Start(){
		#if UNITY_ANDROID
		nowPlatform = 0; //Android
		#elif UNITY_IOS
		nowPlatform = 1; //IOS
		#endif

		MobileAds.Initialize(AdMobDatas[nowPlatform].appId);

		/****		獎勵廣告建置	****/
		this.rewardBasedVideo = RewardBasedVideoAd.Instance;

		rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
		rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
		rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
		rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
		rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
		rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
		rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

		this.RequestRewardBasedVideo();
		/*****************************************/

		/****		插頁廣告建置	****/
		this.RequestInterstitial();
		/*******************************/
	}

	void Update(){
		tempTime += Time.deltaTime;
		if (tempTime > reloadTime) {
			tempTime = 0;
			if (reloadInterstitial) {
				reloadInterstitial = false;
				RequestInterstitial ();
			}

			if (reloadrewardVideo) {
				reloadrewardVideo = false;
				RequestRewardBasedVideo ();
			}
		}
	}

	/*************************		獎勵廣告		*******************************/
	private void RequestRewardBasedVideo(){
		print("【獎勵廣告】建置");

		AdRequest.Builder _builder = new AdRequest.Builder();
		if (testMode) {
			_builder.AddTestDevice (AdCommon.DeviceIdForAdmob);
		}

		this.rewardBasedVideo.LoadAd(_builder.Build(), AdMobDatas[nowPlatform].rewardVideo_AdUnitId);
	}

	private void HandleRewardBasedVideoLoaded(object sender, EventArgs args){
		MonoBehaviour.print("【獎勵廣告】Loaded");
	}

	private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args){
		MonoBehaviour.print("【獎勵廣告】FailedToLoad : " + args.Message);
		reloadrewardVideo = true;
	}

	private void HandleRewardBasedVideoOpened(object sender, EventArgs args){
		MonoBehaviour.print("【獎勵廣告】Opened");
	}

	private void HandleRewardBasedVideoStarted(object sender, EventArgs args){
		MonoBehaviour.print("【獎勵廣告】Started");
	}

	private void HandleRewardBasedVideoClosed(object sender, EventArgs args){
		MonoBehaviour.print("【獎勵廣告】Closed");
		this.RequestRewardBasedVideo();
	}

	private void HandleRewardBasedVideoRewarded(object sender, Reward args){
		string type = args.Type;
		double amount = args.Amount;
		MonoBehaviour.print("【獎勵廣告】Rewarded : " + amount.ToString() + " " + type);
	}

	private void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args){
		MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
	}

	/****************************************************************************/


	/***************************		插頁廣告		*****************************/


	private void RequestInterstitial(){
		print("【插頁廣告】建置");

		interstitial = new InterstitialAd(AdMobDatas[nowPlatform].Interstitial_AdUnitId);

		interstitial.OnAdLoaded += HandleOnAdLoaded;
		interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		interstitial.OnAdOpening += HandleOnAdOpened;
		interstitial.OnAdClosed += HandleOnAdClosed;
		interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

		AdRequest.Builder _builder = new AdRequest.Builder();
		if (testMode) {
			_builder.AddTestDevice (AdCommon.DeviceIdForAdmob);
		}

		interstitial.LoadAd(_builder.Build());
	}

	private void HandleOnAdLoaded(object sender, EventArgs args){
		MonoBehaviour.print("【插頁廣告】Loaded");
	}

	private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args){
		MonoBehaviour.print("【插頁廣告】FailedToLoad : "+ args.Message);
		reloadInterstitial = true;
	}

	private void HandleOnAdOpened(object sender, EventArgs args){
		MonoBehaviour.print("【插頁廣告】Opened");
	}

	private void HandleOnAdClosed(object sender, EventArgs args){
		MonoBehaviour.print("【插頁廣告】Closed");
		interstitial.Destroy ();
		RequestInterstitial();
	}

	private void HandleOnAdLeavingApplication(object sender, EventArgs args){
		MonoBehaviour.print("【插頁廣告】HandleAdLeavingApplication");
	}
	/***************************************************************************/


	public static void ShowRewardedVideo(){
		instance.M_ShowRewardedVideo ();
	}

	public void M_ShowRewardedVideo(){
		if (rewardBasedVideo.IsLoaded ()) {
			print ("【獎勵廣告】 展示");
			rewardBasedVideo.Show ();
		}
		else {
			print ("【獎勵廣告】 未準備好");
		}
	}

	public static void ShowInterstitialAds(){
		instance.M_ShowInterstitialAds ();
	}

	public void M_ShowInterstitialAds(){
		if (interstitial.IsLoaded ()) {
			print ("【插頁廣告】 展示");
			interstitial.Show ();
		} 
		else {
			print ("【插頁廣告】 未準備好");
		}
	}
}