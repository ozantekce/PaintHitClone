using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{



    public TYPE type;


    // Start is called before the first frame update
    void Start()
    {

        iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
        {
            "y",
            0,
            "easetype",
            iTween.EaseType.easeInOutCirc,
            "time",
            0.4f,
            "OnComplete",
            "RotateCircle"
        }));

    }

    void RotateCircle()
    {

        if(type == TYPE.type2)
        {
            iTween.RotateBy(base.gameObject, iTween.Hash(new object[]
            {
            "y",
            0.8f,
            "time",
            GameManager.Instance.rotationTime,
            "easeType",
            iTween.EaseType.easeInOutQuad,
            "loopType",
            iTween.LoopType.pingPong,
            "delay",
            0.4
            }));
        }

        if(type == TYPE.type4)
        {
            iTween.RotateBy(base.gameObject, iTween.Hash(new object[]
            {
            "y",
            0.75f,
            "time",
            GameManager.Instance.rotationTime,
            "easeType",
            iTween.EaseType.easeInOutQuad,
            "loopType",
            iTween.LoopType.pingPong,
            "delay",
            0.5
            }));
        }

        if (type == TYPE.type5)
        {
            iTween.RotateBy(base.gameObject, iTween.Hash(new object[]
            {
            "y",
            1f,
            "time",
            GameManager.Instance.rotationTime,
            "easeType",
            iTween.EaseType.easeInOutQuad,
            "loopType",
            iTween.LoopType.pingPong,
            "delay",
            1
            }));
        }


    }


    // Update is called once per frame
    void Update()
    {
        if(type == TYPE.type1)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * GameManager.Instance.rotationSpeed);
        }
        if(type == TYPE.type3)
        {
            transform.Rotate(Vector3.down * Time.deltaTime * (GameManager.Instance.rotationSpeed + 20));
        }

    }








    public enum TYPE
    {
        type1,
        type2,
        type3,
        type4,
        type5
    }



}
