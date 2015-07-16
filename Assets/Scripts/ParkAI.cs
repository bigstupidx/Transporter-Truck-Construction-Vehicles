using UnityEngine;
using System.Collections;

public class ParkAI : MonoBehaviour {

	public int curTruck = 1;

	void Start () 
	{
		if (gameObject.tag == "station")
		{
			gameObject.transform.FindChild("trucks").gameObject.SetActive(false);
		}
		else
		{
			curTruck = Random.Range(1,5);
			for (int i=1;i<5;i++)
			{
				if (curTruck != i) gameObject.transform.FindChild("trucks").FindChild("g"+i).gameObject.SetActive(false);
			}
		}
	}

	void hideAll()
	{
		gameObject.transform.FindChild("trucks").gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
