using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using System;
using UnityEngine.UI;

using UnityEngine.Experimental.UIElements;

using UnityEngine.SceneManagement;
using System.IO;

using Microsoft.VisualBasic;

 

public class GameController : MonoBehaviour
{
    public List<Rope> Hinges;
    public List<PickUpItem> Items;
    public List<Enemies> EnemyList;
    public int isConnected;
    public GameObject Camera;
    public GameObject Player;
    public bool Connected;
    public SceneSwitch sceneSwitcher;
    private float xOffset;
    private float yOffset;
    public Text score;
    public float scoreCount;
    public int connectionCount;
    public float highestHeight;
    public float topSpeed;
    public float distance;
    private bool fellOffFlag;

    string highScoreFilePath = " Data\\HighScore.csv";


    //public DB db = new DB();



    Vector3 StartingCameraPos;


    public bool JumpClick;

    // Start is called before the first frame update
    void Awake()
    {
       string tempHiScore =  DataSaver.loadData<string>("HighScore");
        DB.HighScore = float.Parse(tempHiScore);
            

        /*string datapath;
        Debug.Log("Thiiiiiis: "+ Application.persistentDataPath);
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Debug.Log("app is running on iphone");
        }
        else
        {
            Debug.Log("Application.platform didnt work. systeminfo.os output is :"+SystemInfo.operatingSystem);
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer   )
        {
            datapath = Application.dataPath + "/HighScore.csv";
            Debug.Log("iphone " + datapath);
        }
        else if(SystemInfo.operatingSystem.Contains("Mac"))
        {
            datapath = Application.dataPath + "/Data/HighScore.csv";
            Debug.Log("mac " + datapath);
        }
        else
        {
            datapath = highScoreFilePath;
            Debug.Log("Pc " +datapath);
        }
        //reading in highscore from csv file 
         var reader = new StreamReader(new MemoryStream(( Resources.Load("HighScore") as TextAsset).bytes));
        List<string> searchList = new List<String>();
        while (!reader.EndOfStream)
        {
            Int32.TryParse(reader.ReadLine(), out var dummy); 
            //var line = reader.ReadLine();
            //reader.
            // searchList.Add(line);
        }*/

        Debug.Log("Succsessfully read High score Data ");
        xOffset = 19.2f;
        yOffset = 67.9f;
        sceneSwitcher = new SceneSwitch();
        Hinges = new List<Rope>();
        Items = new List<PickUpItem>();
        EnemyList = new List<Enemies>();
        Player = GameObject.FindWithTag("Player");
        Camera = GameObject.FindWithTag("MainCamera");
        Connected = false;
        JumpClick = false;
        scoreCount = 0;
        connectionCount = 0;
        distance = 0;
        highestHeight = 0;
        topSpeed = 0;
        
        fellOffFlag = false;
        initScore();


        Player.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
        Player.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionY;
    }

    void Update()
    {
        checkForEnter();
        updateScore();
        // Debug.Log("fellOffFlag: " + fellOffFlag + ", first");
        //garbageMan();
        scoreCount = calculateScore();
        if (StartingCameraPos.y - 92 > Player.transform.position.y)
        {
            print("Fell OFF");

            //  Object prefab = AssetDatabase.LoadAssetAtPath("Assets/prefab/Player.prefab", typeof(GameObject));
            //  Player = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            // Modify the clone to your heart's content
            respawnPlayer();





            // string PlayerLocation = "Assets/prefab/Player.prefab";
            //ToDo Reinstanciate player.  Google how to instanciate prefabs. 
            //The player can be foound in prefab folder in unity and already has starting position.
        }
        
         
          //Wait for first click
        if(Input.GetMouseButtonDown(0))
        {
            if (!JumpClick)
            {
                this.JumpClick = true;
                Player.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                Player.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            }
        }


       /* if (StartingCameraPos.y - 65 > Player.transform.position.y)
        {
            this.JumpClick = false;

            Player.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
            Player.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionY;
            print("Fell OFF");
            //Destroy(Player);

            //  Object prefab = AssetDatabase.LoadAssetAtPath("Assets/prefab/Player.prefab", typeof(GameObject));
            //  Player = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            // Modify the clone to your heart's content

            //Camera.transform.position= StartingCameraPos;
            Player.transform.position = new Vector3(-25, 127, -5);
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }*/

          
    }

    public void checkForEnter()
    {
        if (Input.GetKeyDown("space"))
        {
            if(sceneSwitcher.getSceneIndex() == 2)
            {
                sceneSwitcher.switchScenes(0);
            }
            
        }

        StartingCameraPos = Camera.transform.position;
        //JumpClick = false;
        //Player.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
        //Player.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionY;  
    }

    public bool GetJumpClick()
    {
        return this.JumpClick;
    }
    public void SetJumpClick(bool b)
    {
        this.JumpClick = b;

    }

    //Returns index
    public int AddHinge(Rope H)
    {
        Hinges.Add(H);
        return Hinges.Count - 1;
    }

    public void incrementScore()    //used to calculate score
    {
        scoreCount++;
    }

    public void incrementConnections()  //used to calculate score
    {
        connectionCount++;
    }

    public void setTopSpeed(Vector2 ts)   //used to calculate score
    {
        if(ts.magnitude > topSpeed)
        {
            topSpeed = ts.magnitude;
        }
        
    }

    public void setHighestHeight(float h)   //used to calculate score
    {
        if(h > highestHeight)
        {
            highestHeight = h;
        }

    }

    public void setDistance(float d)
    {
        if(d > distance)
        {
            distance = d;
        }
    }

    public float calculateScore()
    {
        return (float)connectionCount + topSpeed/10 + highestHeight + distance/100;
    }

    public bool getfellOffFlag()   
    {
        return fellOffFlag;
    }

    public void setfellOffFlag(bool flag)
    {
        fellOffFlag = flag;
    }

    public void initScore()
    {
        Font arial;
        arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

        // Create Canvas GameObject.
        GameObject canvasGO = new GameObject();
        canvasGO.name = "Canvas";
        canvasGO.AddComponent<Canvas>();
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Get canvas from the GameObject.
        Canvas canvas;
        canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Create the Text GameObject.
        GameObject textGO = new GameObject();
        textGO.transform.parent = canvasGO.transform;
        textGO.AddComponent<Text>();

        // Set Text component properties.
        score = textGO.GetComponent<Text>();
        score.font = arial;
        score.fontSize = 24;
        score.alignment = TextAnchor.MiddleCenter;
        score.color = Color.black;

        // Provide Text position and size using RectTransform.

        score.transform.position = new Vector3(850, 520, -5);

        //On Screen score for iphone
        //score.transform.position = new Vector3(1350, 820, -5);
    }

    public void updateScore()
    {
        score.text = "score: " + (int)scoreCount;
        //Debug.Log(score.text + scoreCount)
    }

    //Returns index
    public int AddItem(PickUpItem I)
    {
        Items.Add(I);

        return Hinges.Count - 1;
    }

    public int AddEnemy(Enemies E)
    {
        EnemyList.Add(E);

        return Hinges.Count - 1;
    }

    public void setIsConnected(Rope rope)
    {
        Connected = true;
        this.isConnected = Hinges.IndexOf(rope);
    }

    public void SetConnectedFlag(bool s)
    {
        this.Connected = s;
    }

    public bool GetConnectedFlag()
    {
        return this.Connected;
    }

    public int getConnected()
    {
        return isConnected;
    }

    public bool IsHingeClosest(int index)
    {
        bool ret = true;
        float currDistance;
        float distanceInQuestion;
        Vector2 NextVector;
        Vector2 PlayerVector = new Vector2(Player.transform.position.x, Player.transform.position.y);
        Vector2 HingeVector = new Vector2(Hinges[index].transform.position.x, Hinges[index].transform.position.y);

        distanceInQuestion = Vector2.Distance(PlayerVector, HingeVector);

        for (int x = 0; x < Hinges.Count; x++)
        {
            if (x != index)
            {
                NextVector = new Vector2(Hinges[x].transform.position.x, Hinges[x].transform.position.y);

                currDistance = Vector2.Distance(PlayerVector, NextVector);

                if (distanceInQuestion < currDistance)
                {
                    continue;
                }
                else
                {
                    ret = false;
                    break;
                }

            }
        }

        return ret;
    }

    Vector3 CorrectedHingePosition(int index)
    {
        return new Vector3(Hinges[index].Hinge.transform.position.x - xOffset, Hinges[index].Hinge.transform.position.y - yOffset,
            Hinges[index].Hinge.transform.position.z);

    }

    

    public void respawnPlayer()
    {

        //Player.transform.position = new Vector3(-25, 127, -5);
        //Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        DB.Score = calculateScore();
        if(DB.Score > DB.HighScore)
        {
            //saves highscore to csv file
            DB.HighScore = scoreCount;
            var csv = new System.Text.StringBuilder();
            var highScoreString = DB.HighScore.ToString();
            var newLine = string.Format(highScoreString);
             csv.AppendLine(newLine);

            DataSaver.saveData<string>(csv.ToString(), "HighScore");
             //File.WriteAllText("Data\\HighScore.csv", csv.ToString());

        }
        scoreCount = 0;
        //Destroy(Player);
        fellOffFlag = true;
        for(int i = 0; i < Hinges.Count; i++)
        {
            Hinges[i].setFirstConnection(true);
        }
        DB.LvlIndex = 2;
        sceneSwitcher.switchScenes(2);
        //SceneManager.LoadScene(2);
    }

    private void garbageMan()
    {
        if (EnemyList.Count > 10)
        {
            for (var i = 0; i < EnemyList.Count - 10; i++)
            {
                Destroy(EnemyList[i]);
            }
        }

    }
    private void OnLevelWasLoaded(int level)
    {
        upgradeMenuActions();
    }
    public void upgradeMenuActions()
    {
        if (Upgrades.Glider == 1)
        {
            GameObject gldr = Instantiate(Resources.Load("Glider", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject; 
                
                }
    }
}

