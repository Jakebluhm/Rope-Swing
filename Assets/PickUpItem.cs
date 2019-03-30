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
    private int itemIndex;

    void Start()
    {

        Player = GameObject.FindWithTag("Player");
        game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        itemIndex = game.AddItem(this);
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
                                                             (80 * Player.GetComponent<Rigidbody2D>().velocity.normalized);

                
            }
            //else if()
            thisItem.SetActive(false);
            Destroy(thisItem);
        }
    }


}
