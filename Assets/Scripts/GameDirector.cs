using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour {

    GameObject timeBar;
    GameObject gameover;
    GameObject puzzleManager;
    public static bool touch = true;

    // Use this for initialization
    void Start () {

        this.timeBar = GameObject.Find("TimeBar");
        this.gameover = GameObject.Find("GameOver");
        this.puzzleManager = GameObject.Find("PuzzleManager");

        StartCoroutine(TimeCheck());
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    IEnumerator TimeCheck()
    {
        for(int i = 0; i < 60; i++)
        {
            this.timeBar.GetComponent<Image>().fillAmount -= 1.0f / 60;

            yield return new WaitForSeconds(1.0f);
        }
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        touch = false;
        Nacho.shouldDeselect = true;
        puzzleManager.GetComponent<PuzzleManager>().DetectDeselect();

        for (int i = 0; i < 16; i++)
        {
            this.gameover.GetComponent<Image>().fillAmount += 1.0f / 16;

            yield return new WaitForSeconds(0.08f);
        }

    }
}
