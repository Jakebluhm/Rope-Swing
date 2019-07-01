
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//HOW TO debug Click Debug -> Attach Unity Debugger. Go to unity and press play.



/*
 * Rope class decieds what cloud is the closest and connects to it on click
 * 
*/


public class Rope : MonoBehaviour
{
    /*  */
    public HingeJoint2D currRope;
    /*  */
    public GameObject Player;
    /*  */
    public GameObject Hinge;
    /*  */
    private InputClass gameInput;
    /*  */
    private static int mouseState;
    /*  */
    public GameController game;
    /*  */
    private int index;
    /*  */
    private bool isConnected;
    /*  */
    private GameObject VisualRope;
    /*  */
    private float xOffset;
    /*  */
    private float yOffset;
    /*  */
    private bool firstConnection;
    /*  */
    private int kek;
    /*  */

    // Start is called before the first frame update
    void Start()
    {
        xOffset = 19.2f;
        yOffset = 67.9f;
        mouseState = -1;
        Player = GameObject.FindWithTag("Player");
        game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        isConnected = false;
        index = game.AddHinge(this);
        firstConnection = true;
        kek = 0;
        bool a = currRope.isActiveAndEnabled ;
        gameInput = GameObject.FindWithTag("MainCamera").GetComponent<InputClass>();
    }
    
    /* Update is called once per frame
    *
    */
    void Update()
    {
        /*Form the visable rope you see on screen if the mouse state is 1 (left click held down),
        * The index of the cloud that is being swung from is the same as the one in the game controller
        * (game.getConnected()), and the game controller has a flag set with (game.GetConnectedFlag()).
        */
        if (mouseState == 1 &&  index == game.getConnected() && game.GetConnectedFlag())
        {
            //transform.position gives the transform of the gameobject this script 
            // is connected to in the unity editor.
            Vector3 temp = (Player.transform.position + transform.position);
            Vector3 tempAng = (Player.transform.position - transform.position);

            //temp now holds the coordinates at the half way point between the point of
            // connection and the player, using the midpoint formula
            temp = temp * 0.5f;

        
            float angle = Mathf.Atan2(tempAng.y, tempAng.x) * Mathf.Rad2Deg;

            //Create/Update the ropes scale, posiition and angle.
            VisualRope.transform.position = new Vector3(temp.x, temp.y, -5);
            VisualRope.transform.localScale = new Vector3(.25f, tempAng.magnitude/ 10f, .25f);
            VisualRope.transform.eulerAngles = new Vector3(0,0, angle+ 90);
        }
        if (game.GetJumpClick() &&Input.GetMouseButtonDown(0) && game.IsHingeClosest(index) && !game.GetConnectedFlag()  )
        {
            if (Upgrades.Glider == 1)
            {
                if (gameInput.getInputFlag() == 0) // Only connect when left side of the screen is pressed
                {
                    Connect();
                }
            }
            if (Upgrades.Glider == 0) // Connect on any input when no upgrades are active.
            {
 
                    Connect();
                 
            }
        }
        if (Input.GetMouseButtonUp(0) )
        {
            game.SetConnectedFlag(false);
            currRope.connectedBody = null;
            mouseState = 0;
            Destroy(VisualRope);

        }
    }

    Vector3 CorrectedHingePosition()
    {
        return new Vector3(Hinge.transform.position.x - xOffset, Hinge.transform.position.y - yOffset,
            Hinge.transform.position.z);

    }

 void DrawLine(Vector3 start, Vector3 end, Color color, bool destroy) {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        myLine.GetComponent<LineRenderer>().sortingLayerName = "Default";
        myLine.GetComponent<LineRenderer>().sortingOrder = 1;
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Specular"));
        lr.SetColors(color, color);
        lr.SetWidth(0.5f, 0.5f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, .01f);
    }

    public void setFirstConnection(bool b)
    {
        this.firstConnection = b;
    }
    private void Connect() {
        if (mouseState != 1)
        {
            if (firstConnection)
            {
                firstConnection = false;
                game.incrementConnections();
            }
            VisualRope = Instantiate(Resources.Load("VisualRope", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            // Modify the clone to your heart's content
            Vector3 temp = (Player.transform.position + CorrectedHingePosition()) * 0.5f;
            VisualRope.transform.position = new Vector3(temp.x, temp.y, -5);

            game.setIsConnected(this);
            Vector2 vel = Player.GetComponent<Rigidbody2D>().velocity + (12 * Player.GetComponent<Rigidbody2D>().velocity.normalized);
            currRope.connectedBody = Player.GetComponent<Rigidbody2D>();
        }
        mouseState = 1;
    }
}