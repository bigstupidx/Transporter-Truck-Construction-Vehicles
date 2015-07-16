using UnityEngine;
using System.Collections;

public class LoadingScene : MonoBehaviour {

	public static int preloadScene = 2;

	private AsyncOperation async;
	public UILabel label;
	public GameObject wheel;
	private bool startLoading = true;

	void Start () 
	{
		//label = (UILabel)gameObject.GetComponent("UILabel");
		StartCoroutine("_Start");
	}

	IEnumerator _Start() {
		//int levelint = PlayerPrefs.GetInt("Player Level");
		Debug.Log("Loading... ");
		async = Application.LoadLevelAsync(2);
		Debug.Log("Loading complete");
		
		yield return async;
		
	}


	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (startLoading) label.text = (Mathf.Floor(async.progress*100)).ToString()+"%";
		Vector3 an = wheel.transform.eulerAngles;
		an.z+=10*Time.deltaTime;
		wheel.transform.eulerAngles = an;
	}
}
