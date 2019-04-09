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
    private int Count50s;
    public int groundWidth = 294;
    public int cameraWidth = 228;
    public int cameraHelper = 0;
    public int boostSpawnChance = 4;    // boostSpawnChance / 10 to spawn a boost every x distance
    public int startup = 1;
    public int blockSpawnDistance = 150;
    
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

        if(startup == 1)
        {
            startup = 0;
            //  Object prefab = AssetDatabase.LoadAssetAtPath("Assets/prefab/Block.prefab", typeof(GameObject));
            GameObject block = Instantiate(Resources.Load("Block", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            // Modify the clone to your heart's content
            block.transform.position = new Vector3(Player.transform.position.x + Random.Range(blockSpawnDistance - 5, 170 + 5), Random.Range(187 - 5, 187 + 5), -5);

            GameObject sky = Instantiate(Resources.Load("Sky_", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            // Modify the clone to your heart's content
            sky.transform.position = new Vector3(startPos + 290, 82.1f, 0);

            GameObject mace = Instantiate(Resources.Load("Mace", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;

            //Count100s = 0;
            //Object prefab2 = AssetDatabase.LoadAssetAtPath("Assets/prefab/Ground.prefab", typeof(GameObject));
            GameObject ground = Instantiate(Resources.Load("Ground", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            // Modify the clone to your heart's content
            ground.transform.position = new Vector3(startPos + 295, 55.0f, 0);
        }


        if (startPos + 100 < Player.transform.position.x)
        //if(cameraHelper + cameraWidth < Player.transform.position.x)
        {
            cameraHelper = (int)Player.transform.position.x;
            startPos = Player.transform.position.x;
            Count100s = Count100s + 1;

            if ((Count100s % 2) == 0)
            {
                //Count100s = 0;
                //Object prefab1 = AssetDatabase.LoadAssetAtPath("Assets/prefab/Sky_.prefab", typeof(GameObject));
                GameObject sky = Instantiate(Resources.Load("Sky_",typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                // Modify the clone to your heart's content
                sky.transform.position = new Vector3(startPos + 290, 82.1f, 0);

                GameObject mace = Instantiate(Resources.Load("Mace", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;

                //Count100s = 0;
                //Object prefab2 = AssetDatabase.LoadAssetAtPath("Assets/prefab/Ground.prefab", typeof(GameObject));
                GameObject ground = Instantiate(Resources.Load("Ground", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                // Modify the clone to your heart's content
                ground.transform.position = new Vector3(startPos + 295, 55.0f, 0);

                if(Random.Range(0,10) > boostSpawnChance)
                {
                    GameObject PickUpItem = Instantiate(Resources.Load("Boost", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                }
               

            }
            if (Count100s == 1000)
            {
               // Object prefab3 = AssetDatabase.LoadAssetAtPath("Assets/prefab/Finish.prefab", typeof(GameObject));
                GameObject finishLine = Instantiate(Resources.Load("Finish", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                // Modify the clone to your heart's content
                finishLine.transform.position = new Vector3(startPos + 300, 82.1f, 0);
              
            }

              //  Object pefab = AssetDatabase.LoadAssetAtPath("Assets/prefab/Block.prefab", typeof(GameObject));
            GameObject block = Instantiate(Resources.Load("Block", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            // Modify the clone to your heart's content
            block.transform.position = new Vector3(Player.transform.position.x + Random.Range(blockSpawnDistance - 5, blockSpawnDistance + 5), Random.Range(192-5, 192+5) , -5);





        }

    }
}
