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

    public float delayTime;
    public Vector3 posA;
    public Vector3 posB;


  


    public Camera Cam;
    public GameObject Player;
    static float startPos;
    private int Count100s;
    private int camheight;
    private bool IsCoRutineRunning;
    private float bottomCamPos;
    // Start is called before the first frame update
    void Start()
    {
        camheight = 133;
        IsCoRutineRunning = false;
        startPos = Player.transform.position.x;
        Count100s = 0;
        delayTime = 0.005f;
        bottomCamPos = Cam.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = 0f;
        float xpos = Player.transform.position.x;
        //Rect view = Cam.pixelRect;


        Debug.Log("Flag:" + IsCoRutineRunning);


        if (!IsCoRutineRunning && Cam.transform.position.y + (camheight / 2) < Player.transform.position.y + 20)
        {


            Vector3 Vec = new Vector3(xpos + 50, Cam.transform.position.y + 20, Cam.transform.position.z);

            if (!IsCoRutineRunning)
            {
                StartCoroutine(WaitAndMove(delayTime, Cam.transform.position, Vec));
            }
        }
        else if (!IsCoRutineRunning && (Cam.transform.position.y - (camheight / 2) > Player.transform.position.y - 20) && Cam.transform.position.y  > bottomCamPos)
        {
            if (Cam.transform.position.y - 20 < bottomCamPos)
            {
                temp = bottomCamPos;
            }
            else
            {
                temp = Cam.transform.position.y - 20;
            }

            Vector3 Vec = new Vector3(xpos + 50, temp, Cam.transform.position.z);

            if (!IsCoRutineRunning)
            {
                StartCoroutine(WaitAndMove(delayTime, Cam.transform.position, Vec));
            }
        }
       /* else if (!IsCoRutineRunning)
        {
            Cam.transform.position = new Vector3(xpos + 50, Cam.transform.position.y, Cam.transform.position.z);
        }*/




        if (startPos + 100 < Player.transform.position.x)
        {
            startPos = Player.transform.position.x;
            Count100s = Count100s + 1;
            if ((Count100s % 2) == 0)
            {
                //Count100s = 0;
                //Object prefab1 = AssetDatabase.LoadAssetAtPath("Assets/prefab/Sky_.prefab", typeof(GameObject));
                GameObject sky = Instantiate(Resources.Load("Sky_", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
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
