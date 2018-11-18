using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour {

    PuzzleManager manager;
    Camera cam;

    // Use this for initialization
    void Start () {
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        manager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
    }

    // Update is called once per frame
    void Update ()
    {

        //if(Input.GetMouseButtonDown(0))
        //{

        //    Vector3 touch = cam.ScreenToWorldPoint(Input.mousePosition);
        //    float temp, distance = 9999f;
        //    GameObject NearNacho = null;

        //    foreach (ArrayList list in manager.Block)
        //    {
        //        foreach (GameObject nacho in list)
        //        {

        //            temp = Mathf.Pow(touch.x - nacho.gameObject.transform.position.x, 2) + Mathf.Pow(touch.y - nacho.gameObject.transform.position.y, 2);

        //            if(distance >= temp){
        //                distance = temp;
        //                NearNacho = nacho.gameObject;
        //            }
        //        }
        //    }
        //    NearNacho.gameObject.GetComponent<Nacho>().isSelected = true;
        //    Nacho.isDragging = true;
        //    Debug.Log(NearNacho.gameObject.GetComponent<Nacho>().index);
        //}

    }


}
