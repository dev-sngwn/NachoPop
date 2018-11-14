using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nacho : MonoBehaviour {
    
    //USE YET
    Animator anim;
    public GameObject Effect;

    public string ClipName, NewName; // 클립이름 PuzzleManager에서 자동으로 설정함 
    public int index; // PuzzleManager의 컬럼 번호 (ArrayList에서의 인덱스를 바로 찾아오기 위함)
    public bool isDead = false;
    
    PuzzleManager manager;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
        //anim.Play(ClipName);
        // 하이어라키의 PuzzleManager를 가져온다.
        manager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
        {
            Debug.Log("@@");
        }
	}

    void OnMouseDown()
    {
        Debug.Log(index);
    }
}
