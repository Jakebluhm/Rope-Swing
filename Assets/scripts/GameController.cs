using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;


/*Todo: 
* 
*/

/*Todo: Add level end code
 * once a spot is reached end that level
*/

/*Todo: Some power ups that can be picked up
* 
*/

/*Todo: Menu system
 * btw each level have option to purchase upgrades
 * 
 * upgrade ideas:
 * 
 * 
 */

public class GameController : MonoBehaviour
{
    public List<Rope> Hinges;
    public List<PickUpItem> Items;
    public int isConnected;
    public GameObject Camera;
    public GameObject Player;
    public bool Connected;
    Vector3 StartingCameraPos;

    private float xOffset;
    private float yOffset;

    public bool JumpClick;
    // Start is called before the first frame update
    void Awake()
    {
        xOffset = 19.2f;
        yOffset = 67.9f;
        Hinges = new List<Rope>();
        Items = new List<PickUpItem>();
        Player = GameObject.FindWithTag("Player");
        Camera = GameObject.FindWithTag("MainCamera");
        Connected = false;
        StartingCameraPos = Camera.transform.position;
        JumpClick = false;
        Player.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
        Player.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionY;
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

    //Returns index
    public int AddItem(PickUpItem I)
    {
        Items.Add(I);

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
        Vector2 HingeVector = new Vector2(CorrectedHingePosition(index).x, CorrectedHingePosition(index).y);

        distanceInQuestion = Vector2.Distance(PlayerVector, HingeVector);

        for (int x = 0; x < Hinges.Count; x++)
        {
            if (x != index)
            {
                NextVector = new Vector2(CorrectedHingePosition(x).x, CorrectedHingePosition(x).y);

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

    void Update()
    {
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


        if (StartingCameraPos.y - 65 > Player.transform.position.y)
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
            Player.transform.position = new Vector3(-25,127,-5);
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);     

            // string PlayerLocation = "Assets/prefab/Player.prefab";
            //ToDo Reinstanciate player.  Google how to instanciate prefabs. 
            //The player can be foound in prefab folder in unity and already has starting position.
        }
    }
}