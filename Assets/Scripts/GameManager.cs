
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //Singleton
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    public GameObject GameScreen, LevelCompleteScreen, FailScreen, PauseScreen, MainScreen;

    public float rotationSpeed = 70f;
    public float rotationTime = 3;

    private Color currentColor;

    public Color CurrentColor { get => currentColor; set => currentColor = value; }
    public int BallsCount { get => ballsCount; set => ballsCount = value; }
    public bool ReadyToCreateBall { get => readyToCreateBall; set => readyToCreateBall = value; }

    private int circleNo;

    private int ballsCount=5;
    private int currentLevel;
    private int totalCircles;


    private bool readyToCreateBall;

    public Transform circles;

    public GameObject environment;

    public AudioSource gameComplate, gameFail;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    public void NextCircle()
    {

        DownOldCircles();

        if (circleNo >= totalCircles)
        {
            //change level
            StartCoroutine(NextLevel());
            Debug.Log("level over");
            return;
        }
        Debug.Log(circleNo);
        UpdateLevel();
        ChangeCurrentColor();
        CreateCircle();
        UpdateBallImages();

    }

    public Text levelText;


    IEnumerator NextLevel()
    {
        gameComplate.Play();
        currentLevel = PlayerPrefs.GetInt("C_Level", 1);

        GameScreen.SetActive(false);
        environment.SetActive(false);
        LevelCompleteScreen.SetActive(true);
        levelText.text = "Level " + currentLevel;

        currentLevel++;
        PlayerPrefs.SetInt("C_Level", currentLevel);

        yield return new WaitForSeconds(1.2f);

        FindObjectOfType<BackgroundManager>().RandomBackground();
        GameScreen.SetActive(true);
        environment.SetActive(true);
        LevelCompleteScreen.SetActive(false);
        circleNo = 0;
        foreach (Transform child in circles.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        UpdateLevel();
        ChangeCurrentColor();
        CreateCircle();
        UpdateBallImages();

    }


    public void LoadFailScreen()
    {
        gameFail.Play();
        GameScreen.SetActive(false);
        environment.SetActive(false);
        FailScreen.SetActive(true);

    }

    public void LoadMainScreen()
    {
        FailScreen.SetActive(false);
        MainScreen.SetActive(true);
    }

    public void LoadGameScreen()
    {

        MainScreen.SetActive(false);
        environment.SetActive(true);
        GameScreen.SetActive(true);

        foreach (Transform child in circles.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        circleNo = 0;
        readyToCreateBall = true;
        PlayerPrefs.SetInt("C_Level", 1);
        UpdateLevel();
        ChangeCurrentColor();
        CreateCircle();
        UpdateBallImages();

    }


    public void RestartGame()
    {
        GameScreen.SetActive(true);
        environment.SetActive(true);
        FailScreen.SetActive(false);

        foreach (Transform child in circles.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        circleNo = 0;
        readyToCreateBall = true;
        PlayerPrefs.SetInt("C_Level", 1);
        UpdateLevel();
        ChangeCurrentColor();
        CreateCircle();
        UpdateBallImages();
    }



    void UpdateLevel()
    {
        currentLevel = PlayerPrefs.GetInt("C_Level", 1);

        if (currentLevel == 1)
        {
            ballsCount = 3;
            totalCircles = 2;
        }

        if (currentLevel == 2)
        {
            ballsCount = 3;
            totalCircles = 3;
        }

        if (currentLevel == 3)
        {
            ballsCount = 3;
            totalCircles = 4;
        }

        if (currentLevel == 4)
        {
            ballsCount = 3;
            totalCircles = 5;
        }

        if (currentLevel == 5)
        {
            ballsCount = 3;
            totalCircles = 5;
        }

        if (currentLevel == 6)
        {
            ballsCount = 3;
            totalCircles = 5;
        }

        if (currentLevel == 7)
        {
            ballsCount = 3;
            totalCircles = 5;
        }

        if (currentLevel >= 8 && currentLevel <= 12)
        {
            ballsCount = 4;
            totalCircles = 5;
        }

        if (currentLevel >= 13 && currentLevel <= 20)
        {
            ballsCount = 4;
            totalCircles = 6;
            rotationSpeed = 120;
            rotationTime = 2;
        }

        if (currentLevel >= 21)
        {
            ballsCount = 5;
            totalCircles = 6;
            rotationSpeed = 140;
            rotationTime = 1;
        }


        


    }

    public void ChangeCurrentColor()
    {
        Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        currentColor = color;

        BallCreator ballCreator = FindObjectOfType<BallCreator>();
        ballCreator.GetComponent<MeshRenderer>().material.color = currentColor;
    }


    public void DownOldCircles()
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag("circle");

        GameObject gameObject = GameObject.Find("Circle" + (circleNo-1));

        for (int i = 0; i < 24; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);

        }
        gameObject.transform.GetChild(24).gameObject.GetComponent<MeshRenderer>().material.color = currentColor;


        if (gameObject.GetComponent<iTween>())
        {
            gameObject.GetComponent<iTween>().enabled = false;
        }

        foreach (GameObject target in array)
        {
            iTween.MoveBy(target, iTween.Hash(new object[]
            {
                "y",
                -2.98f,
                "easetype",
                iTween.EaseType.spring,
                "time",
                0.5f


            }));
        }

    }

    public void CreateCircle()
    {

        GameObject gameObject = Instantiate(Resources.Load("round"),circles) as GameObject;
        Circle circle = gameObject.GetComponent<Circle>();
        circle.type = (Circle.TYPE)Random.Range(0, 5);
        circle.transform.position = new Vector3(0, 20, 23);
        circle.name = "Circle" + circleNo;

        circleNo++;

        MakeHurdles(gameObject);


    }


    private void MakeHurdles(GameObject circle)
    {

        int[] array;
        if (circleNo == 1)
        {
            array = new int[]
            {
                Random.Range(1,3),
            };
        }
        else if (circleNo == 2)
        {
            array = new int[]
            {
                Random.Range(1,3),
                Random.Range(15,17)
            };

        }
        else if (circleNo == 3)
        {
            array = new int[]
            {
                Random.Range(1,3),
                Random.Range(4,6),
                Random.Range(18, 20)
            };

        }
        else if (circleNo == 4)
        {
            array = new int[]
            {
                Random.Range(1,3),
                Random.Range(4,6),
                Random.Range(15, 17),
                Random.Range(22, 24)
            };
        }
        else
        {
            array = new int[]
            {
                Random.Range(1,3),
                Random.Range(4,6),
                Random.Range(11, 13),
                Random.Range(8, 10),
                Random.Range(15, 17)
            };
        }

        for (int i = 0; i < array.Length; i++)
        {
            circle.transform.GetChild(array[i]).gameObject.GetComponent<MeshRenderer>().enabled = true;
            circle.transform.GetChild(array[i]).gameObject.GetComponent<MeshRenderer>().material.color = currentColor;
            circle.transform.GetChild(array[i]).gameObject.tag = "red";
        }

    }


    public Image[] ballImages;
    public void UpdateBallImages()
    {
        foreach (var image in ballImages)
        {
            image.gameObject.SetActive(false);
        }

        for(int i = 0;i < ballsCount; i++)
        {
            ballImages[i].gameObject.SetActive(true);
            ballImages[i].color = currentColor;
        }

    }

}
