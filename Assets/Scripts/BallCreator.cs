using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCreator : MonoBehaviour
{

    public GameObject BallPrefab;
    public float speed;
    

    public void CreateBall()
    {

        GameManager gameManager = GameManager.Instance;
        Color color = gameManager.CurrentColor;
        if (gameManager.BallsCount <= 0 || !gameManager.ReadyToCreateBall)
        {
            return;
        }
        gameManager.ReadyToCreateBall = false;
        gameManager.BallsCount--;

        gameManager.Invoke("UpdateBallImages", 0.1f);

        GameObject gameObject = Instantiate<GameObject>(BallPrefab,transform.position, Quaternion.identity);
        gameObject.GetComponent<MeshRenderer>().material.color = color;
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * speed, ForceMode.Impulse);

    }




}
