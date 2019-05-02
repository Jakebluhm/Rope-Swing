using System.Collections;
using System.Threading.Tasks;
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

    private int camheight;
    private bool IsCoRutineRunning;
    private float bottomCamPos;
    private float delayTime;

    //Cam Vars 
    public float dampTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        camheight = 133;
        IsCoRutineRunning = false;
        startPos = Player.transform.position.x;
        Count100s = 0;
        bottomCamPos = Cam.transform.position.y;
        delayTime = 0.000001f;
    }

    // Update is called once per frame
    void Update()
    {

        float xpos = Player.transform.position.x;
        //Rect view = Cam.pixelRect;


         float temp = 0f;
        target = Player.transform ;
        Vector3 point = Cam.WorldToViewportPoint(target.position);
        Vector3 delta = target.position - Cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);



        /*if (IsCoRutineRunning)
         Debug.Log("Flag:" + IsCoRutineRunning + "   Cam.transform.position.y+ (camheight / 2)" +
                (Cam.transform.position.y + (camheight / 2)) + "   Player.transform.position.y + 20:   " + (Player.transform.position.y + 20));

         if (!IsCoRutineRunning && Cam.transform.position.y + (camheight / 2) < Player.transform.position.y + 15)
         {


             Vector3 Vec = new Vector3(xpos + 50, Cam.transform.position.y + 60, Cam.transform.position.z);


     StartCoroutine(WaitAndMove(delayTime, Cam.transform.position, Vec));

  }
         else if (!IsCoRutineRunning && (Cam.transform.position.y - (camheight / 2) > Player.transform.position.y - 15) && Cam.transform.position.y  > bottomCamPos+2)
         {
             if (Cam.transform.position.y - 60 < bottomCamPos)
             {
                 temp = bottomCamPos;
             }
             else
             {
                 temp = Cam.transform.position.y - 60;
             }

             Vector3 Vec = new Vector3(xpos + 50, temp, Cam.transform.position.z);

             if (!IsCoRutineRunning)
             {
                 StartCoroutine(WaitAndMove(delayTime, Cam.transform.position, Vec));
             }
         }
        else if (!IsCoRutineRunning  &&    !(Cam.transform.position.y + (camheight / 2) < Player.transform.position.y + 15)
           &&  !(Cam.transform.position.y - (camheight / 2) > Player.transform.position.y - 15))
         {
             Cam.transform.position = new Vector3(xpos + 50, Cam.transform.position.y, Cam.transform.position.z);
         }
         */



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



            //GameObject mace = Instantiate(Resources.Load("Mace", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;

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
                GameObject sky = Instantiate(Resources.Load("Sky_", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                // Modify the clone to your heart's content
                sky.transform.position = new Vector3(startPos + 290, 82.1f, 0);

                GameObject skyred = Instantiate(Resources.Load("Sky_Red", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                // Modify the clone to your heart's content
                skyred.transform.position = new Vector3(startPos + 290, 208.4f, 0);
                // GameObject mace = Instantiate(Resources.Load("Mace", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;

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

            //  Object prefab = AssetDatabase.LoadAssetAtPath("Assets/prefab/Block.prefab", typeof(GameObject));

            GameObject block = Instantiate(Resources.Load("Block", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            // Modify the clone to your heart's content
            block.transform.position = new Vector3(Player.transform.position.x + Random.Range(blockSpawnDistance - 5, blockSpawnDistance + 5), Random.Range(192-5, 192+5) , -5);

        }





    }
    IEnumerator WaitAndMove(float delayTime, Vector3 from, Vector3 to)
    {
        IsCoRutineRunning = true;


        yield return new WaitForSeconds(delayTime); // start at time X

        float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        while (Time.time - startTime <= .5)
        { // until one second passed


            Cam.transform.position = cameraControl.Lerp(from, to, Time.time - startTime, Player.transform.position.x); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }
        IsCoRutineRunning = false;
    }

    // Linearly interpolates between two vectors.

    public static Vector3 Lerp(Vector3 a, Vector3 b, float t, float x)

    {

        t = Mathf.Clamp01(t);

        return new Vector3(

            x,

            a.y + (b.y - a.y) * t,

            a.z + (b.z - a.z) * t

        );

    }

}









   
