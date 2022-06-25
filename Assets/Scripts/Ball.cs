using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{


    void OnCollisionEnter(Collision target)
    {

        if (target.gameObject.tag == "red")
        {
            base.gameObject.GetComponent<Collider>().enabled = false;
            target.gameObject.GetComponent<MeshRenderer>().enabled = true;
            target.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            base.GetComponent<Rigidbody>().AddForce(Vector3.down * 50, ForceMode.Impulse);
            Debug.Log("GAME OVER");
            GameManager.Instance.LoadFailScreen();
            Destroy(base.gameObject, .5f);
            
        }
        else
        {
            GetComponent<AudioSource>().Play();
            base.gameObject.GetComponent<Collider>().enabled = false;
            GameObject gameObject = Instantiate(Resources.Load("splash1")) as GameObject;
            gameObject.transform.parent = target.gameObject.transform;
            gameObject.GetComponent<SpriteRenderer>().material.color = GameManager.Instance.CurrentColor;
            Destroy(gameObject, 0.1f);
            target.gameObject.name = "color";
            target.gameObject.tag = "red";
            StartCoroutine(ChangeColor(target.gameObject));
        }

    }

    IEnumerator ChangeColor(GameObject g)
    {
        yield return new WaitForSeconds(0.1f);
        g.gameObject.GetComponent<MeshRenderer>().enabled = true;
        g.gameObject.GetComponent<MeshRenderer>().material.color = GameManager.Instance.CurrentColor;
        Destroy(base.gameObject);

        if (GameManager.Instance.BallsCount <= 0)
        {
            GameManager.Instance.NextCircle();
        }
        GameManager.Instance.ReadyToCreateBall = true;

    }


}
