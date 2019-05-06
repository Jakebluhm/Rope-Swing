using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUpItem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject thisItem;
    public GameController game;
    public GameObject Player;
    public Camera cam;
    private int itemIndex;
    private int itemX;
    private int itemY;
    private int itemZ;
    private int centerScreenY = 65;

    void Start()
    {
       
        Player = GameObject.FindWithTag("Player");
        game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        itemIndex = game.AddItem(this);
        cam = Camera.current;
        itemZ = -5;
        itemX = (int)Player.transform.position.x + Random.Range(150, 160);  //creates a random x value for the spawn of the enemy
        itemY = Random.Range(centerScreenY - 35, centerScreenY + 15);  //creates a random y value for the spawn of the enemy
        Vector3 itemPosition = new Vector3(itemX, itemY, itemZ);
        thisItem.transform.position = itemPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(true) // Add else if's for different items
            {
                Player.GetComponent<Rigidbody2D>().velocity = Player.GetComponent<Rigidbody2D>().velocity +
                                                             (70 * Player.GetComponent<Rigidbody2D>().velocity.normalized);

                
            }
            //else if()
            thisItem.SetActive(false);
            Destroy(thisItem);
        }
    }


}
