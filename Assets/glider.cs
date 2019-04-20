using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glider : MonoBehaviour
{

    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Player.transform.rotation = new Quaternion(0, 0, -90, 0);
        transform.position = Player.transform.position + new Vector3(0, 5, -5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
