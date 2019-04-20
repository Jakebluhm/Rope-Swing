using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameOverButton : MonoBehaviour
{
    public GameController game;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)  //checks that the scene index is the game over screen
        {
            game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
            //creates highscore text on screen
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
            score.transform.position = new Vector3(450, 355, -5);
            score.text = "score: " + DB.Score;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //checkForEnter();
    }

    public void restart()   //on game over scene, button press causes this method to run
    {
        DB.LvlIndex = 0;
        SceneManager.LoadScene(0);

    }

    public void upgrade()   //on game over scene, button press causes this method to run
    {
        DB.LvlIndex = 0;
        game.upgradeMenuActions();
        SceneManager.LoadScene(0);


    }

    public void mainMenuPlay()  //main menu play button causes this method to run
    {
        SceneManager.LoadScene(0);
    }

    public void gameOverMainMenu()
    {
        SceneManager.LoadScene(3);
    }
}
