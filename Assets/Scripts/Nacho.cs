using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nacho : MonoBehaviour {
    
    //USE YET
    //Animator anim;
    //public GameObject Effect;

    public int type;
    //public string ClipName, NewName; // 클립이름 PuzzleManager에서 자동으로 설정함 
    public int col; // PuzzleManager의 컬럼 번호 (ArrayList에서의 인덱스를 바로 찾아오기 위함)
    public int row; // PuzzleManager의 컬럼 번호 (ArrayList에서의 인덱스를 바로 찾아오기 위함)

    public bool isSelected = false;
    public bool isDiagonal = false;
    public bool isUp = false;
    public bool isPop = false;

    public static bool isDragging = false;
    public static bool shouldDeselect = false;
    public static bool isTurn;
    public static Nacho startNacho;
    public static Nacho recentNacho;
    public static Nacho beforeRecentNacho;
    public static bool shouldPop = false;

    PuzzleManager manager;

    // Use this for initialization
    void Start () {
        
        //anim = GetComponent<Animator>();
        //anim.Play(ClipName);
        // 하이어라키의 PuzzleManager를 가져온다.
        manager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void SelectNacho(){
        if(!isSelected && GameDirector.touch)
        {
            manager.popStack.Push(this);
            beforeRecentNacho = recentNacho;
            recentNacho = this;
            isSelected = true;
            transform.Rotate(new Vector3(0, 0, 10.0f));
        }
    }

    public void DeselectNacho(){
        if(isSelected){
            isSelected = false;
            transform.Rotate(new Vector3(0, 0, -10.0f));
        }
    }

    private void OnMouseEnter()
    {
        if (isDragging && IsDiagonal())
        {
            SelectNacho();
        }

        if(isDragging && beforeRecentNacho == this){
            Debug.Log("turn?");
            isTurn = !isTurn;
            manager.popStack.Push(this);
            recentNacho = this;
        }
    }

    public bool IsDiagonal(){

        if ((recentNacho.isUp != this.isUp) &&
            (recentNacho.col == this.col || recentNacho.row == this.row) &&
            (Mathf.Abs(recentNacho.col - this.col) <= 1) && (Mathf.Abs(recentNacho.row - this.row) <= 1)
           )
            return true;

        return false;
    }

    private void OnMouseDown()
    {
        Debug.Log("began2");
        startNacho = this;
        isDragging = true;
        recentNacho = this;
        SelectNacho();
    }

    private void OnMouseUp()
    {
        Debug.Log("end2");
        isDragging = false;
        shouldDeselect = true;

        if(manager.popStack.Count >= 3){
            shouldPop = true;

        } else {
            manager.popStack.Clear();
            isTurn = false;
        }
    }


}
