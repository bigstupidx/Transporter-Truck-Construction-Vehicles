﻿using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject MainCenter;
	public GameObject [] LocalCenter;

	private int counter = 0;

	private Transform truck;
	private Transform trailer;

	private bool isEnd = false;
	// Use this for initialization
	void Start () 
	{
		truck = GameObject.FindGameObjectWithTag("Truck").transform;
		trailer = GameObject.FindGameObjectWithTag("Trailer").transform;

		foreach(GameObject tr in LocalCenter)
		{
			tr.SetActive(false);
		}
		MainCenter.SetActive(false);
		LocalCenter[counter].SetActive(true);
		GameManager.instanse.currentTarget = LocalCenter[counter];
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isEnd == false && GameManager.isPause == false)
		{
			foreach (GameObject p in LocalCenter)
			{
				if (p.activeInHierarchy)
				{
					float dis = Vector3.Distance(p.transform.position,truck.transform.position);
					if (dis < 15)
					{
						GameManager.instanse.playCollect();
						p.SetActive(false);
						counter++;
						if (counter < LocalCenter.Length)
						{
							LocalCenter[counter].SetActive(true);
							//GameManager.instanse.currentTarget = LocalCenter[counter];
						}
						else
						{
							MainCenter.SetActive(true);
							//GameManager.instanse.currentTarget = MainCenter;
						}
					}
				}
			}
			float dis2 = Vector3.Distance(MainCenter.transform.position,trailer.transform.position);
			if (dis2 < 8 && MainCenter.activeInHierarchy)
			{
				float delta = Mathf.Abs(MainCenter.transform.eulerAngles.y-trailer.eulerAngles.y);
				Debug.Log("delta="+delta);
				
				if (ProgressAI.instance.valuePR >= 1)
				{
					if (MainCenter.tag == "oil")
					{
						GameManager.instanse.showTut2();
					}
					GameManager.instanse.audio.Stop();
					isEnd = true;

					GameManager.instanse.checkLevel();
					ProgressAI.instance.valuePR = 0;
					GameManager.isEmpty = !GameManager.isEmpty;
					if (GameManager.isEmpty)
					{
						ParkAI park = (ParkAI)MainCenter.GetComponent("ParkAI");
						GameManager.instanse.traillers_packs[park.curTruck-1].SetActive(true);
						MainCenter.SendMessage("hideAll");
					}
					else
					{
						foreach (GameObject go in GameManager.instanse.traillers_packs)
						{
							go.SetActive(false);
						}
					}
					MainCenter.SetActive(false);
				}
				else
				if (delta < 20)
				{
					if (!GameManager.instanse.audio.isPlaying) GameManager.instanse.audio.Play();
					if (!GameManager.instanse.progress.activeInHierarchy) GameManager.instanse.progress.SetActive(true);
					ProgressAI.instance.valuePR +=0.01f;
					if (MainCenter.tag == "oil")
					{
						GameManager.instanse.bar_label.text = "Collecting Construction Vehicle";
					}
					if (MainCenter.tag == "station")
					{
						GameManager.instanse.bar_label.text = "Delivering Construction Vehicle";
					}

				}
			}
			else
			{
				if (GameManager.instanse.progress.activeInHierarchy) GameManager.instanse.progress.SetActive(false);
				if (MainCenter.activeInHierarchy) ProgressAI.instance.valuePR = 0;
			}
		}
	}
}
