using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nacho : MonoBehaviour {
    
    //USE YET
    Animator anim;
    public GameObject Effect;

    public string ClipName, NewName; // 클립이름 PuzzleManager에서 자동으로 설정함 
    public int col; // PuzzleManager의 컬럼 번호 (ArrayList에서의 인덱스를 바로 찾아오기 위함)
    public int row; // PuzzleManager의 컬럼 번호 (ArrayList에서의 인덱스를 바로 찾아오기 위함)
    public bool isDead = false;
    public bool isSelected = false;
    public bool isStart = false;
    public bool isDiagonal = false;
    public static bool isDragging = false;
    public static bool shouldDeselect = false;

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

        }
	}

    public void SelectNacho(){
        if(!isSelected)
        {
            //PuzzleManager.recentNacho = this;
            isSelected = true;
            transform.Rotate(new Vector3(0, 0, 10.0f));
            //Debug.Log(row + ", " + col);
        }
    }

    public void DeselectNacho(){
        if(isSelected){
            isStart = false;
            isSelected = false;
            transform.Rotate(new Vector3(0, 0, -10.0f));
        }
    }

    private void OnMouseEnter()
    {
        if(isDragging && IsDiagonal())
        {
            PuzzleManager.recentNacho = this;
            SelectNacho();
        }
    }

    private bool IsDiagonal(){

        PuzzleManager.recentNacho = this;

        Debug.Log("(" + PuzzleManager.recentNacho.col + ", " + PuzzleManager.recentNacho.row + ")" + " " + this.col + ", " + this.row);
        Debug.Log(PuzzleManager.recentNacho.col == this.col || PuzzleManager.recentNacho.row == this.row);

        return PuzzleManager.recentNacho.col == this.col || PuzzleManager.recentNacho.row == this.row ? true : false;
    }

    private void OnMouseDown()
    {
        PuzzleManager.recentNacho = this;
        isStart = true;
        SelectNacho();
    }

    private void OnMouseUp()
    {
        Debug.Log("(" + PuzzleManager.recentNacho.row + ", " + PuzzleManager.recentNacho.col + ")" + " " + this.row + ", " + this.col);

        shouldDeselect = true;
    }
}
