using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssetBundles;

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
}
