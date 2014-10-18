using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	
	private static SceneManager _instance;
	
	public static SceneManager getInstance()
	{
		if ( _instance == null )
		{
			#if UNITY_EDITOR
				Debug.Log("Had to create new SceneManager!");
			#endif
			GameObject go = GameObject.Find ("_main");
			if(go == null)
				go = new GameObject("SceneManager");
			_instance = go.AddComponent<SceneManager>();
		}
		
		return _instance;
	}
	
	public void loadLevel(string pName, string uiRoot = "UI Root")
	{
		#if UNITY_EDITOR
			Debug.Log ("SceneManager -> loadLevel: " + pName);
		#endif
		
		Application.LoadLevel(pName);
	}
}
