【1】
匯入AdMob插件
https://github.com/googleads/googleads-mobile-unity/releases/tag/v3.15.1
->googleMobileAds.unitypackage


【2】
在unity匯入安桌需要的額外插件
Assets -> Play Services Resolver -> Android Resolver -> Resolve


【3】
將CPlugins -> AdMob腳本匯入專案內


【4】
將GoogleAdMob.cs掛在最初始的場景


【5】
在GoogleAdMob.cs 將各平台的appId、rewardVideo_AdUnitId、Interstitial_AdUnitId
輸入到對應的AdMobDatas
PS：請不要更改AdMobDatas的platfrom資料


【6】
獎勵型廣告
展示呼叫ShowRewardedVideo()
當廣告播放完畢會呼叫HandleRewardBasedVideoRewarded()可以將獲得東西的資料放在這邊
PS：這不是主執行緒 有些東西無法使用
請在ShowRewardedVideo延遲0.5秒呼叫另一個function
再從HandleRewardBasedVideoRewarded 去給定一個值來判斷是否獎勵成功


【7】
插頁型廣告
展示呼叫ShowInterstitialAds
要執行的東西直接ShowInterstitialAds裡面就可以了