using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    public GameObject YNacho;
    public GameObject RNacho;
    public GameObject Empty;
    public GameObject Field;
    public Canvas temp;

    public ArrayList[] Block = new ArrayList[8];
    //public Transform Nacho;

    void Start()
    {
        initField();
    }


    // Update is called once per frame
    void Update()
    {


    }

    void initField()
    {
        var ratio = (float)Screen.height / Screen.width;
        temp = GameObject.Find("Canvas").GetComponent<Canvas>();

        if (ratio < 1.8F) ratio = 2.5F;

        YNacho.transform.localScale = new Vector3(ratio / 50, ratio / 50, 0);
        RNacho.transform.localScale = new Vector3(ratio / 50, ratio / 50, 0);
        Field.transform.localScale = new Vector3(ratio / 6.5F, ratio / 6.5F, 0);

        Instantiate(Field, new Vector3(0, 0, 1000), Quaternion.identity);

        for (int i = 0; i < 8; i++)
        {
            Block[i] = new ArrayList();
        }

        GameObject nacho;
        float x_space = 0, y_space = 0, x_start = 0, y_start = 0;

        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                switch (y)
                {
                    case 0:
                        x_start = -0.8161F;
                        y_start = 1.1778F;
                        x_space = 0.2028F;
                        y_space = 0.3431F;

                        if (x == 2 || x == 4 || x == 6) { nacho = YNacho; }
                        else { nacho = Empty; }

                        Block[y].Add(
                            CreateNacho(
                                nacho, x,
                                new Vector3(
                                    ratio * x_start + (x * ratio * x_space),
                                    ratio * y_start - (y * ratio * y_space),
                                    1000)));
                        break;

                    case 1:
                    case 3:
                    case 5:

                        if (x % 2 == 0)
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

                        Block[y].Add(
                            CreateNacho(
                                nacho, x,
                                new Vector3(
                                    ratio * x_start + (x * ratio * x_space),
                                    ratio * y_start - (y * ratio * y_space),
                                    1000)));
                        break;

                    case 2:
                    case 4:
                    case 6:
                        if (x % 2 == 0)
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

                        Block[y].Add(
                            CreateNacho(
                                nacho, x,
                                new Vector3(
                                    ratio * x_start + (x * ratio * x_space),
                                    ratio * y_start - (y * ratio * y_space),
                                    1000)));
                        break;

                    case 7:
                        x_start = -0.8161F;
                        y_start = 1.2231F;
                        x_space = 0.2028F;
                        y_space = 0.3431F;

                        if (x == 2 || x == 4 || x == 6) { nacho = RNacho; }
                        else { nacho = Empty; }

                        Block[y].Add(
                            CreateNacho(
                                nacho, x,
                                new Vector3(
                                    ratio * x_start + (x * ratio * x_space),
                                    ratio * y_start - (y * ratio * y_space),
                                    1000)));
                        break;

                    default:
                        break;
                }
            }

        }
    }

    public GameObject CreateNacho(GameObject nacho, int idx, Vector3 pos) // idx는 컬럼의 인덱스 번호이고, pos는 실제 생성 위치
    {
        //GameObject temp = Instantiate(nacho, pos, Quaternion.identity) as GameObject;
        GameObject temp = Instantiate(nacho) as GameObject;
        //temp.transform.SetParent(transform); // 생성된 캐릭터를 PuzzleManager의 자식으로 넣는다.

        // 애니메이션 클립 이름을 char01 ~ 05까지 5종류로 생성한다. (클립은 6까지 있으므로 Range(1, 7)로도 가능하다)
        //temp.GetComponent<Nacho>().ClipName = string.Format("char{0:00}", 1);
        temp.transform.localPosition = pos;
        temp.name = nacho.ToString();
        temp.GetComponent<Nacho>().index = idx;
        
        return temp;
    }
    
}
