using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public GameObject YNacho;
    public GameObject RNacho;
    public GameObject GNacho;
    public GameObject KNacho;
    public GameObject Empty;
    public GameObject Field;
    public static Nacho recentNacho;
    public int tempScore;
    public int totalScore;
    public Text scoreText;
    public Text scoreBackground;
    public Canvas canvas;
    public Stack<Nacho> popStack = new Stack<Nacho>();
    public AudioClip scoreSound;
    public AudioClip popSound;
    public AudioSource scoreSoundSource;
    public AudioSource popSoundSource;

    public GameObject[,] Block = new GameObject[8, 9];

    private Touch touch;

    void Start()
    {

        scoreSoundSource = this.gameObject.AddComponent<AudioSource>();
        scoreSoundSource.clip = scoreSound;

        popSoundSource = this.gameObject.AddComponent<AudioSource>();
        popSoundSource.clip = popSound;

        scoreText.text = totalScore.ToString();
        scoreBackground.text = totalScore.ToString();
        totalScore = 0;
        tempScore = 0;
        InitField();
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {

            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Debug.Log("began");
                    break;
                case TouchPhase.Moved:
                    Debug.Log("move");
                    break;
                case TouchPhase.Ended:
                    Debug.Log("end");
                    break;
            }
        }
        //DetectDragging();

        DetectPop();
        DetectDeselect();
    }

    IEnumerator ScoringEffect(int total, int temp)
    {
        float delay = 0.5f / temp * 20;
        int amount = 0;

        if (temp / 20 <= 4){
            delay = 0.1f;
            amount = 20;
        } else if (temp / 20 <= 9){

            delay = 0.065f;
            amount = 30;
        } else
        {
            delay = 0.065f;
            amount = 40;

        }

        for (int i = 0; i <= temp; i += amount) 
        {
            if (i + amount > temp) { 
                i = temp;
                scoreText.text = (i + total).ToString();
                scoreBackground.text = (i + total).ToString();
                break; 
            }

            scoreText.text = (i + total).ToString();
            scoreBackground.text = (i + total).ToString();
            Debug.Log(i + total);
            scoreSoundSource.Play();
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator PopSound(int total, int score)
    {
        float delay = 0.5f / score * 20;

        for (int i = 0; i <= score; i += 20)
        {
            scoreText.fontSize = 60;
            scoreBackground.fontSize = 60;
            yield return new WaitForSeconds(delay / 4 * 3);
            scoreText.text = (i + total).ToString();
            scoreBackground.text = (i + total).ToString();
            popSoundSource.Play();
            scoreText.fontSize = 40;
            scoreBackground.fontSize = 40;
            yield return new WaitForSeconds(delay / 4 * 1);
        }
    }

    void DetectDragging()
    {
        foreach (GameObject nacho in Block)
        {
            if (nacho.GetComponent<Nacho>().isSelected)
            {
                recentNacho = nacho.GetComponent<Nacho>();
                Nacho.isDragging = true;
            }
        }
    }

    private void CancelPop()
    {
        popStack.Clear();
        Nacho.shouldPop = false;
        Nacho.isTurn = false;
    }

    void DetectPop(){

        if(Nacho.shouldPop){

            Nacho start = null, center = null, end = null;

            int count = popStack.Count;

            Debug.Log(count);
            if(!(count == 5 || count == 9 || count == 13 || count == 17)){
                CancelPop();
                return;
            }

            for (int i = 0; i < count; i++){

                if (i == 0) end = popStack.Pop(); 
                else if (i == count / 2) center = popStack.Pop();
                else if (i == count - 1) start = popStack.Pop();
                else popStack.Pop();
            }

            Debug.Log(end.col + ", " + center.col + ", " + start.col);

            if ((start.row == end.row || start.row == center.row || center.row == end.row)
                && (start.type == center.type && center.type == end.type)) 
            {
                if((end.row == start.row && start.row == center.row) || !Nacho.isTurn)
                {
                    Debug.Log(Nacho.isTurn);
                    CancelPop();
                    return;
                }

                Debug.Log("POP!!");


                Nacho left = null, right = null, mid = null;

                if (start.col < end.col && start.col < center.col)
                {
                    left = start;

                    if(center.col < end.col){
                        right = end;
                        mid = center;
                    }
                    else {
                        right = center;
                        mid = end;
                    }
                }
                else if (center.col < start.col && center.col < end.col)
                {
                    left = center;

                    if (start.col < end.col)
                    {
                        right = end;
                        mid = start;
                    }
                    else
                    {
                        right = start;
                        mid = end;
                    }

                }
                else
                {
                    left = end;

                    if (start.col < center.col)
                    {
                        right = center;
                        mid = start;
                    }
                    else
                    {
                        right = start;
                        mid = center;
                    }
                }

                int rightCol = right.col;
                int leftCol = left.col;
                int midRow = center.row;

                if (start.isUp){

                    for (int j = left.row; j >= midRow; j--)
                    {
                        for (int i = leftCol; i <= rightCol; i++)
                        {
                            Debug.Log("(" + j +", " +i + ")");
                            Block[j, i].GetComponent<Nacho>().isPop = true;
                        }
                        leftCol++;
                        rightCol--;
                    }
                } 
                else
                {
                    for (int j = left.row; j <= center.row; j++)
                    {
                        for (int i = leftCol; i <= rightCol; i++)
                        {
                            Debug.Log("(" + j + ", " + i + ")");
                            Block[j, i].GetComponent<Nacho>().isPop = true;
                        }
                        leftCol++;
                        rightCol--;
                    }

                }

                foreach (GameObject nacho in Block)
                {
                    if (nacho.GetComponent<Nacho>().isPop)
                    {
                        //POP EXECUTE
                        tempScore += 20;
                        nacho.GetComponent<Nacho>().transform.Rotate(0, 0, 30.0f);
                        nacho.GetComponent<Nacho>().isPop = false;
                        GameObject temp;
                        int type;

                        if(nacho.GetComponent<Nacho>().isUp){
                            if(Random.Range(0, 2) % 2 == 0){
                                temp = GNacho;
                                type = 3;

                            } else {
                                temp = YNacho;
                                type = 1;
                            }

                        } 
                        else
                        {
                            if (Random.Range(0, 2) % 2 == 0)
                            {
                                temp = KNacho;
                                type = 4;

                            }
                            else
                            {
                                temp = RNacho;
                                type = 2;
                            }

                        }

                        Block[nacho.GetComponent<Nacho>().row, nacho.GetComponent<Nacho>().col]
                        = CreateNacho(temp,
                                      type,
                                      nacho.GetComponent<Nacho>().row,
                                      nacho.GetComponent<Nacho>().col,
                                      nacho.transform.position);

                        Destroy(nacho);
                        
                    }
                }
                int totalTmp = totalScore;
                StartCoroutine(ScoringEffect(totalTmp, tempScore));
                totalScore += tempScore;
                StartCoroutine(PopSound(totalTmp, tempScore));
                tempScore = 0;
            }
            else
                Debug.Log("fail..");

            Nacho.shouldPop = false;
            Nacho.isTurn = false;
        }

    }

    public void DetectDeselect()
    {

        foreach (GameObject nacho in Block)
        {
            if (Nacho.shouldDeselect)
            {
                Nacho.isDragging = false;
                nacho.GetComponent<Nacho>().DeselectNacho();
            }
        }

        Nacho.shouldDeselect = false;

    }

    void InitField()
    {
        var ratio = (float)Screen.height / Screen.width;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        if (ratio < 1.8F) ratio = 2.5F;

        YNacho.transform.localScale = new Vector3(ratio / 50, ratio / 50, 0);
        RNacho.transform.localScale = new Vector3(ratio / 50, ratio / 50, 0);
        GNacho.transform.localScale = new Vector3(ratio / 50, ratio / 50, 0);
        KNacho.transform.localScale = new Vector3(ratio / 50, ratio / 50, 0);
        Field.transform.localScale = new Vector3(ratio / 6.5F, ratio / 6.5F, 0);
        
        Instantiate(Field, new Vector3(0, 0, 1000), Quaternion.identity);

        GameObject nacho = null;
        int type = 0;
        float x_space = 0, y_space = 0, x_start = 0, y_start = 0;

        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                switch (row)
                {
                    case 0:
                        x_start = -0.8161F;
                        y_start = 1.1778F;
                        x_space = 0.2028F;
                        y_space = 0.3431F;

                        if (col == 2 || col == 4 || col == 6) { nacho = YNacho; type = 1; }
                        else { nacho = Empty; type = 0; }
                        break;

                    case 1:
                    case 3:
                    case 5:

                        if (col % 2 == 0)
                        {
                            x_start = -0.8161F;
                            y_start = 1.2231F;
                            x_space = 0.2028F;
                            y_space = 0.3431F;
                            nacho = RNacho;
                            type = 2;
                        }
                        else
                        {
                            x_start = -0.8161F;
                            y_start = 1.1778F;
                            x_space = 0.2028F;
                            y_space = 0.3431F;
                            nacho = YNacho;
                            type = 1;
                        }
                        break;

                    case 2:
                    case 4:
                    case 6:
                        if (col % 2 == 0)
                        {
                            x_start = -0.8161F;
                            y_start = 1.1778F;
                            x_space = 0.2028F;
                            y_space = 0.3431F;
                            nacho = YNacho;
                            type = 1;
                        }
                        else
                        {
                            x_start = -0.8161F;
                            y_start = 1.2231F;
                            x_space = 0.2028F;
                            y_space = 0.3431F;
                            nacho = RNacho;
                            type = 2;
                        }

                        break;

                    case 7:
                        x_start = -0.8161F;
                        y_start = 1.2231F;
                        x_space = 0.2028F;
                        y_space = 0.3431F;

                        if (col == 2 || col == 4 || col == 6) { nacho = RNacho; type = 2; }
                        else { nacho = Empty; type = 0; }

                        break;

                    default:
                        break;
                }

                Block[row, col] = CreateNacho(
                        nacho, type, row, col,
                        new Vector3(
                            ratio * x_start + (col * ratio * x_space),
                            ratio * y_start - (row * ratio * y_space), 1000));
            }

        }
    }

    public GameObject CreateNacho(GameObject nacho, int type, int row, int col, Vector3 pos) // idx는 컬럼의 인덱스 번호이고, pos는 실제 생성 위치
    {
        GameObject temp = Instantiate(nacho) as GameObject;
        //temp.transform.SetParent(transform); // 생성된 캐릭터를 PuzzleManager의 자식으로 넣는다.

        // 애니메이션 클립 이름을 char01 ~ 05까지 5종류로 생성한다. (클립은 6까지 있으므로 Range(1, 7)로도 가능하다)
        //temp.GetComponent<Nacho>().ClipName = string.Format("char{0:00}", 1);
        temp.transform.localPosition = pos;
        temp.name = nacho.ToString();

        if(type % 2 == 1) temp.GetComponent<Nacho>().isUp = true;
        else temp.GetComponent<Nacho>().isUp = false;

        temp.GetComponent<Nacho>().type = type;
        temp.GetComponent<Nacho>().col = col;
        temp.GetComponent<Nacho>().row = row;

        return temp;
    }
    
}
