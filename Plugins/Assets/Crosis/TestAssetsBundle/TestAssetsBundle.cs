using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssetBundles;
using UnityEngine.SceneManagement;

public class TestAssetsBundle : MonoBehaviour {
	[SerializeField]private Transform parent;
	private string assetBundleName = "level1";
	private string assetName = "Cube";

	void Start () {
		
	}
	
	void Update () {
		
	}

	public void Init(){
		StartCoroutine(Initialize());
	}

	protected IEnumerator Initialize()
	{
		InitializeSourceURL();

		// Initialize AssetBundleManifest which loads the AssetBundleManifest object.
		var request = AssetBundleManager.Initialize();
		if (request != null)
			yield return StartCoroutine(request);
	}

	void InitializeSourceURL()
	{
		// If ODR is available and enabled, then use it and let Xcode handle download requests.
		#if ENABLE_IOS_ON_DEMAND_RESOURCES
		if (UnityEngine.iOS.OnDemandResources.enabled)
		{
		AssetBundleManager.SetSourceAssetBundleURL("odr://");
		return;
		}
		#endif
		#if DEVELOPMENT_BUILD || UNITY_EDITOR
		// With this code, when in-editor or using a development builds: Always use the AssetBundle Server
		// (This is very dependent on the production workflow of the project.
		//      Another approach would be to make this configurable in the standalone player.)
		AssetBundleManager.SetDevelopmentAssetBundleServer();
		return;
		#else
		// Use the following code if AssetBundles are embedded in the project for example via StreamingAssets folder etc:

		AssetBundleManager.SetSourceAssetBundleURL(Application.streamingAssetsPath + "/");

		// Or customize the URL based on your deployment or configuration
		//AssetBundleManager.SetSourceAssetBundleURL("http://www.MyWebsite/MyAssetBundles");
		return;
		#endif
	}

	public void Load(){
		StartCoroutine(IE_Load());
	}

	IEnumerator IE_Load(){
		AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, "Cube", typeof(GameObject));
		if (request == null)
			yield break;
		yield return StartCoroutine(request);

		GameObject prefab = request.GetAsset<GameObject>();

		if (prefab != null)
			GameObject.Instantiate(prefab);
	}

	public void Load2(){
		StartCoroutine(IE_Load2());
	}

	IEnumerator IE_Load2(){
		AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync("level2", "Capsule1", typeof(GameObject));
		if (request == null)
			yield break;
		yield return StartCoroutine(request);

		GameObject prefab = request.GetAsset<GameObject>();

		if (prefab != null)
			GameObject.Instantiate(prefab);
	}

	public void OnResetScene(){
		SceneManager.LoadScene(0);
	}

	public void OnUnload_False(){
		Resources.UnloadUnusedAssets();
	}
}


/*
ORD
【 行動 】
檢視	開啟	關閉	拿下	放入	完成	前往	檢視二
*************************************************

【 初始物品 】
我	檢視	開啟	關閉	拿下	大廳	掛
*************************************************

【 總共物品 】
我	大廳	二樓	壁畫	雕像	梯子	螢幕	一樓	大門	櫃子
裝置	紙條	木盒子	鑰匙	裝飾(劍)	裝飾(弓)	裝飾(盾)	裝飾(旗子)

二樓物品	壁畫	雕像	梯子	螢幕	裝飾(劍)	裝飾(弓)	紙條	木盒子
一樓物品	大門	櫃子	裝置	鑰匙	裝飾(盾)	裝飾(旗子)

共用	裝飾(劍)	裝飾(弓)	裝飾(盾)	裝飾(旗子)	鑰匙	紙條

<<狀態>>
樓層狀態	Floor	>	One or Two or Null(代表在Two)	
梯子狀態	Ladder	>	Null Or PutDown
裝置狀態	Device	>	
木盒子狀態	WoodenBox	>	Open or Null

	StartText	我是奈特，原本在城堡裡面生活的人....在我沉靜在回憶的時候城堡傳來巨大的搖晃...看來我還是先離開城堡吧。

*****************		二樓		****************

	我	檢視	大廳	(刪除大廳)我在大廳的二樓，而前面是前往一樓的樓梯，我沿著樓梯往下走，此時城堡又傳來巨大的搖晃，天花板上面的燈掉了下來把一樓跟二樓連接的樓梯砸壞了，看來要找其他的方式從【二樓】下去了。
	
	我	檢視	二樓	(判斷 Floor)
				Two	>	觸發 我檢視二	二樓
				One or Null	>	我必須要先到二樓才能檢視。

	我	檢視二	二樓	右邊走道的牆壁上有很多【壁畫】，左邊的走道有好幾個騎士的【雕像】，等等...我看到在左邊走道的底部有個可以下去的【梯子】。

	我	檢視	雕像	(獲得【裝飾(劍)) 一個騎士拿著劍的雕像，在雕像的後面找到一張【紙條】和一個【裝飾品】，不知道是誰丟在這邊的。

	我	檢視	紙條	
騎士訓練須知
當集合鐘聲響起，一分鐘內穿好頭盔、手套、盔甲、褲子、鞋子，並到集合場集合。


	我	檢視	壁畫	上面畫著四個騎士，紅色騎士拿著弓箭，綠色騎士戴著長劍，藍色騎士背著一個盾牌，黃色騎士手中拿著旗子，下面有個【木盒子】。

	我	檢視	木盒子	木製的盒子，上面有個星型的鑰匙孔。
	我	開啟	木盒子	(判斷 WoodenBox)
						Open	>	已經開打了。
						Open	>	我需要用鑰匙才能打開。
	
	鑰匙	開啟	木盒子	(WoodenBox > Open)(獲得 【裝飾(弓))我使用鑰匙將盒子打開，裡面放著一個【裝飾品】。
	
	我	檢視	梯子	(判斷 Ladder 狀態)
			Null	回答	(我 檢視二 梯子)
			PutDown	>	梯子已經放下，可以前往一樓了。

	我	檢視二	梯子	一個可以伸縮的梯子，我嘗試使用手來將它放下但是沒有作用，似乎是要靠機器才能控制，旁邊有一個像【螢幕】的東西，應該就是靠它控制的吧。

	我	檢視	螢幕	控制梯子的機器，上面有許多的圖案。

	我	開啟	螢幕	(觸發 我開啟螢幕)(圖形鎖)

	我	完成	螢幕	(刪除梯子)(重新獲得一樓)(Ladder > PutDown)當我輸入完螢幕後，機器開始運轉，梯子連結到了【一樓】。

	我	前往	一樓	(判斷 Floor 狀態)
				One 我已經在一樓了。
				
				Two or Null	(判斷 Ladder)
					PutDown	>	(Floor > One)我從梯子下去到了一樓。(隱藏二樓的東西)
					Null	>	我需要先把梯子放下才能到一樓。

****************************************************


*****************		二樓		****************
	我	前往	二樓	(判斷 Floor 狀態)
				Two or Null 我已經在二樓了。
				One	(Floor > Two)我沿著梯子爬上二樓。(隱藏一樓的東西)
				
	
	我	檢視	一樓	(判斷Floor)
				Two	>	(觸發 我檢視二	一樓)
				one or Null	>	我必須要先到二樓才能檢視。	

	我	檢視二	一樓	在我左手邊是城堡的【大門】，中間是通往二樓的樓梯，但是已經無法通行了，右手邊有一個【櫃子】。

	我	檢視	裝飾(劍)	劍造型的裝飾品，大概一個手掌大小，做得十分精緻。
	我	檢視	裝飾(弓)	弓造型的裝飾品，大概一個手掌大小，做得十分精緻。
	我	檢視	裝飾(盾)	盾造型的裝飾品，大概一個手掌大小，做得十分精緻。
	我	檢視	裝飾(旗子)	旗子造型的裝飾品，大概一個手掌大小，做得十分精緻。

	裝飾(劍)	放入	xx	(刪除 【裝飾(劍)】)
	裝飾(弓)	放入	xx	(刪除 【裝飾(弓)】)
	裝飾(盾)	放入	xx	(刪除 【裝飾(盾)】)
	裝飾(旗子)	放入	xx	(刪除 【裝飾(旗子)】)

	我	檢視	櫃子	(獲得 【裝飾(旗子) 【裝飾(盾)】) 櫃子上面放著許多獎盃，是過去家族在騎士比賽得到的，旁邊有兩個個精緻的【裝飾品】和一把【鑰匙】。

	我	檢視	鑰匙	銅製的鑰匙，但是樣子挺奇怪的，最前方是一個星型。
	
	我	檢視	裝置	畫面的四個角有著不同顏色，xxxxx

	我	完成	裝置	(觸發 故事結局)	裝置發出了綠色的光芒，此時右手邊的大門緩緩開起。
	
	我	檢視	大門	大門被鎖著，也沒有看到可以手把之類的東西，似乎要靠其他的東西來打開。

	
	****************************************************************
	*                                       結局答案                                  *
	****************************************************************
	我想起來了我是奈特，為了幫助海德救瑪麗安，我開始進行實驗，一開始以老鼠進行實驗的時候還算順利，後來進行人體實驗發生預料之外的事情....原本我以為失敗了，當我回去休息後城堡內有傳來求救的聲音，這時候我第一個想到的是我的實驗，我衝到實驗室卻發現實驗體已經消失了，我開始往城堡內傳來求救聲音的地方，卻看到我的實驗體已經變成怪物在城堡內殺人，而被實驗體殺的人也都變成怪物，我不顧一切的逃跑，但是當時實在太混亂了我被人撞倒後昏迷了過去，但我是怎麼逃出去的?
*/