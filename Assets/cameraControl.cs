//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

public class cameraControl : MonoBehaviour
{

    /*Todo: 
    * 
    */

    /*Todo: Backwards scrolling
     * 
    */
    /*Todo: Add vertical scrolling?
     * 
    */

    public Camera Cam;
    public GameObject Player;
    static float startPos;
    private int Count100s;
    // Start is called before the first frame update
    void Start()
    {
        startPos = Player.transform.position.x;
        Count100s = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float xpos = Player.transform.position.x;
        Vector3 Vec = new Vector3(xpos + 50, Cam.transform.position.y, Cam.transform.position.z);
        Cam.transform.position = Vec;

        if (startPos + 100 < Player.transform.position.x)
        {
            startPos = Player.transform.position.x;
            Count100s = Count100s + 1;
            if ((Count100s % 2) == 0)
            {
                //Count100s = 0;
                //Object prefab1 = AssetDatabase.LoadAssetAtPath("Assets/prefab/Sky_.prefab", typeof(GameObject));
                GameObject sky = Instantiate(Resources.Load("Sky_",typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                // Modify the clone to your heart's content
                sky.transform.position = new Vector3(startPos + 300, 82.1f, 0);

                //Count100s = 0;
                //Object prefab2 = AssetDatabase.LoadAssetAtPath("Assets/prefab/Ground.prefab", typeof(GameObject));
                GameObject ground = Instantiate(Resources.Load("Ground", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                // Modify the clone to your heart's content
                ground.transform.position = new Vector3(startPos + 300, 82.1f, 0);
            }
            if (Count100s == 3)
            {
               // Object prefab3 = AssetDatabase.LoadAssetAtPath("Assets/prefab/Finish.prefab", typeof(GameObject));
                GameObject finishLine = Instantiate(Resources.Load("Finish", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                // Modify the clone to your heart's content
                finishLine.transform.position = new Vector3(startPos + 300, 82.1f, 0);
              
            }

              //  Object prefab = AssetDatabase.LoadAssetAtPath("Assets/prefab/Block.prefab", typeof(GameObject));
            GameObject block = Instantiate(Resources.Load("Block", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            // Modify the clone to your heart's content
            block.transform.position = new Vector3(startPos + 120, 167, -5);





        }

    }
}
