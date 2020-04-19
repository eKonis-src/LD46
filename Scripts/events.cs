using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Diagnostics;

public class events : MonoBehaviour
{
	public GameObject game;
	public Stopwatch stopWatch = new Stopwatch();
	public int nbzbmort;
	public Text timetext;
	public GameObject refil;
	public shooting amo;
	public GameObject scoreboard;
	public Text scoretext;
	public Text zb_text;
	public Text bossTxt;
    // Start is called before the first frame update
    void Start()
    {
        stopWatch.Start();   
    }

    // Update is called once per frame
    void Update()
    {
    	if (amo.ammo >= 5){
    		refil.SetActive(false);
    	}
        if (amo.ammo < 5){
        	refil.SetActive(true);
        }
    }

    public void win(){
    game.SetActive(false);
    stopWatch.Stop();
    TimeSpan ts = stopWatch.Elapsed;
    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts.Hours, ts.Minutes, ts.Seconds,
    ts.Milliseconds / 10);
    scoreboard.SetActive(true);
    timetext.text = elapsedTime;
    double score = (1000 - nbzbmort*10 + 1800 - Math.Floor(ts.TotalSeconds));
    scoretext.text = score.ToString();
    zb_text.text = "Number Of Dead Zombies :"+nbzbmort;
    }

    public void lost(){
    game.SetActive(false);
    stopWatch.Stop();
    TimeSpan ts = stopWatch.Elapsed;
    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts.Hours, ts.Minutes, ts.Seconds,
    ts.Milliseconds / 10);
    scoreboard.SetActive(true);
    timetext.text = elapsedTime;
    double score = (1000 - Boss.life/10 - nbzbmort*10 - Math.Floor(ts.TotalSeconds));
    scoretext.text = score.ToString();
    bossTxt.text = "Boss health left :"+Boss.life;
    zb_text.text = "Number Of Dead Zombies :"+nbzbmort;
    }
}