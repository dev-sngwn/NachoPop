using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    public GameObject YNacho;
    public GameObject RNacho;
    public GameObject Empty;
    public GameObject Field;
    public static Nacho recentNacho;
    public Canvas canvas;

    public GameObject[,] Block = new GameObject[8, 9];

    void Start()
    {
        InitField();
    }


    // Update is called once per frame
    void Update()
    {
        DetectDragging();
        DetectDeselect();
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

    void DetectDeselect()
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
        Field.transform.localScale = new Vector3(ratio / 6.5F, ratio / 6.5F, 0);

        Field.AddComponent<FieldController>();

        Instantiate(Field, new Vector3(0, 0, 1000), Quaternion.identity);

        GameObject nacho = null;
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

                        if (col == 2 || col == 4 || col == 6) { nacho = YNacho; }
                        else { nacho = Empty; }
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
                        }
                        else
                        {
                            x_start = -0.8161F;
                            y_start = 1.1778F;
                            x_space = 0.2028F;
                            y_space = 0.3431F;
                            nacho = YNacho;
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
                        }
                        else
                        {
                            x_start = -0.8161F;
                            y_start = 1.2231F;
                            x_space = 0.2028F;
                            y_space = 0.3431F;
                            nacho = RNacho;
                        }

                        break;

                    case 7:
                        x_start = -0.8161F;
                        y_start = 1.2231F;
                        x_space = 0.2028F;
                        y_space = 0.3431F;

                        if (col == 2 || col == 4 || col == 6) { nacho = RNacho; }
                        else { nacho = Empty; }

                        break;

                    default:
                        break;
                }

                Block[row, col] = CreateNacho(
                        nacho, row, col,
                        new Vector3(
                            ratio * x_start + (col * ratio * x_space),
                            ratio * y_start - (row * ratio * y_space), 1000));
            }

        }
    }

    public GameObject CreateNacho(GameObject nacho, int row, int col, Vector3 pos) // idx는 컬럼의 인덱스 번호이고, pos는 실제 생성 위치
    {
        GameObject temp = Instantiate(nacho) as GameObject;
        //temp.transform.SetParent(transform); // 생성된 캐릭터를 PuzzleManager의 자식으로 넣는다.

        // 애니메이션 클립 이름을 char01 ~ 05까지 5종류로 생성한다. (클립은 6까지 있으므로 Range(1, 7)로도 가능하다)
        //temp.GetComponent<Nacho>().ClipName = string.Format("char{0:00}", 1);
        temp.transform.localPosition = pos;
        temp.name = nacho.ToString();
        temp.GetComponent<Nacho>().col = col;
        temp.GetComponent<Nacho>().row = row;

        return temp;
    }
    
}
