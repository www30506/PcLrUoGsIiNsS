using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenShot : MonoBehaviour {
	private Camera m_camera;
	private Texture2D tempTexture2D;
	public static ScreenShot instance;
	private Rect rect;

	void Awake(){
		instance = this;
		m_camera = this.GetComponent<Camera>();
		rect = new Rect(0, 0, Screen.width, Screen.height);
	}

	void Start () {
	}

	void Update () {
	}


	public static Sprite ToSprite(){

		if(instance == null){
			Debug.LogError("\"ScreenshotSystem\" Component doesn't in any Camera");
			return null;
		}
		return instance.M_ToSprite();
	}

	public Sprite M_ToSprite(){
		tempTexture2D = CaptureCamera(m_camera, rect, false);
		Sprite _sprite = Sprite.Create(tempTexture2D,new Rect(0, 0, tempTexture2D.width, tempTexture2D.height),Vector2.zero);
		return _sprite;
	}


	public static Texture ToTexture(){
		if(instance == null){
			Debug.LogError("\"ScreenshotSystem\" Component doesn't in any Camera");
			return null;
		}
		return instance.M_ToTexture();
	}

	public Texture M_ToTexture(){
		tempTexture2D = CaptureCamera(m_camera, rect, false);
		Texture _texture = tempTexture2D;
		return _texture;
	}

	Texture2D CaptureCamera(Camera camera, Rect rect, bool p_saveToLocal)   
	{  
		// 创建一个RenderTexture对象  
		RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 0);  
		// 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
		camera.targetTexture = rt;  
		camera.Render();  
		//ps: --- 如果这样加上第二个相机，可以实现只截图某几个指定的相机一起看到的图像。  
		//ps: camera2.targetTexture = rt;  
		//ps: camera2.Render();  
		//ps: -------------------------------------------------------------------  

		// 激活这个rt, 并从中中读取像素。  
		RenderTexture.active = rt;  
		Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24,false);  
		screenShot.ReadPixels(rect, 0, 0);// 注：这个时候，它是从RenderTexture.active中读取像素  
		screenShot.Apply();  

		// 重置相关参数，以使用camera继续在屏幕上显示  
		camera.targetTexture = null;  
		//ps: camera2.targetTexture = null;  
		RenderTexture.active = null; // JC: added to avoid errors  
		GameObject.Destroy(rt);  
		// 最后将这些纹理数据，成一个png图片文件  
		byte[] bytes = screenShot.EncodeToPNG();  
		string filename = Application.dataPath + "/Screenshot.png";  
		if(p_saveToLocal){
			System.IO.File.WriteAllBytes(filename, bytes);  
		}
		return screenShot;  
	}  
}
