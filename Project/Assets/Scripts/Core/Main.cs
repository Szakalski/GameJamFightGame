using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
	private static Main _instance;	
	private SceneManager _sceneManager;
	
	void Awake()
	{
		DontDestroyOnLoad(gameObject);		
		_sceneManager = SceneManager.getInstance();
	}
	
	void Start()
	{
		_sceneManager.loadLevel("MainMenu");
		Screen.sleepTimeout = SleepTimeout.NeverSleep;	
		Screen.orientation = ScreenOrientation.Landscape;
	}
	
	public static Main getInstance()
	{
		if ( _instance == null )
		{
			#if UNITY_EDITOR
				Debug.Log("Finding _main");
			#endif
			GameObject lGO = GameObject.Find("_main");
			_instance = lGO.GetComponent<Main>();
		}
		
		return _instance;
	}
}
