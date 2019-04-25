
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//HOW TO debug Click Debug -> Attach Unity Debugger. Go to unity and press play.



/*Todo: Animations are included in assets on unity, Look up toutorial on how to
 * animate character so itll look like its moving
 * Maybe animate when rope is getting grabbed and when its let go
*/
/*Todo: Make rope physics better.
 * Already added speed and shorten rope every new connection made
 * to make it so its not so hard to swing.
* 
*/
public class Rope : MonoBehaviour
{
    public RelativeJoint2D currRope;
    public GameObject Player;
    public GameObject Hinge;
    private static int mouseState;
    public GameController game;
    private int index;
    private bool isConnected;
    private GameObject VisualRope;
    private float xOffset;
    private float yOffset;
    private bool firstConnection;
    private int kek;

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
    }//
    //
    // Update is called once per frame
    void Update()
    {


        if (mouseState == 1 &&  index == game.getConnected() && game.GetConnectedFlag())
        {
            Vector3 temp = (Player.transform.position + CorrectedHingePosition());
            Vector3 tempAng = (Player.transform.position - CorrectedHingePosition());
            temp = temp * 0.5f;
            float angle = Mathf.Atan2(tempAng.y, tempAng.x) * Mathf.Rad2Deg;

            //Debug.Log("angle:" + angle +"     Mag  " + temp.magnitude + "  //10");

           

            
            VisualRope.transform.position = new Vector3(temp.x, temp.y, -5);
            VisualRope.transform.localScale = new Vector3(.25f, tempAng.magnitude/ 10f, .25f);



           VisualRope.transform.eulerAngles = new Vector3(0,0, angle+ 90);

            //    Vector3 tempVec = Hinge.transform.position + (Vector3)Hinge.GetComponent<SpringJoint2D>().anchor;
            //     DrawLine(tempVec, Player.transform.position, new Color(250, 0, 0), false);
        }
        if (game.GetJumpClick() &&Input.GetMouseButtonDown(0) && game.IsHingeClosest(index) && !game.GetConnectedFlag())
        {

                if (mouseState != 1)
                {
                //Debug.Log(firstConnection);
                    if (firstConnection) 
                    {
                        firstConnection = false;
                        game.incrementScore();
                    }
                //Object ropePrefab = AssetDatabase.LoadAssetAtPath("Assets/prefab/VisualRope.prefab", typeof(GameObject));
                VisualRope = Instantiate(Resources.Load("VisualRope", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                    // Modify the clone to your heart's content
                    Vector3 temp = (Player.transform.position + CorrectedHingePosition()) * 0.5f;
                    VisualRope.transform.position = new Vector3(temp.x , temp.y, -5);
                //Debug.Log(temp);


                    game.setIsConnected(this);
                    currRope.connectedBody = Player.GetComponent<Rigidbody2D>();
               
                    Player.GetComponent<Rigidbody2D>().velocity = Player.GetComponent<Rigidbody2D>().velocity + (12 * Player.GetComponent<Rigidbody2D>().velocity.normalized);
                    //currRope.distance = currRope.distance - 5;

                }
                mouseState = 1;
          //  }
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
}