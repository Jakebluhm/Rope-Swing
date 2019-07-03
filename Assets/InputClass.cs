using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This class is to tell other classes which part of the screen is being clicked.
 * The goal is to have 4 quadrants of the screen to control different abilities
 */
public class InputClass : MonoBehaviour
{
    private float cameraStartPos;
    private float currCameraPos;
    private float cameraWidth;
    private int ClickFlag; // 0 left 1 right
    private float LastMousePosX;

    private float waitTime = 0.05f;
    private float timer = 0.0f;
    private float touchOffest;
    // Start is called before the first frame update
    void Start()
    {
        cameraStartPos = transform.position.x;
        cameraWidth = 1000;
        ClickFlag = -1;
        if (  (Application.platform == RuntimePlatform.IPhonePlayer) || SystemInfo.operatingSystem.Contains("Mac"))
        {
            touchOffest = 710f;
            Debug.Log("Running on iphone or mac");
        }
        else
        {
            touchOffest = 530f;
            Debug.Log("Running on pc");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           

            currCameraPos = transform.position.x;
             
           float touchPosition = (Input.mousePosition.x) - touchOffest + transform.position.x;
            float MiddleBoundry =  transform.position.x ; 
           if (touchPosition > MiddleBoundry)
            {
                ClickFlag = 1; // Right
                //Debug.Log("Diff: " + (touchPosition - MiddleBoundry) + "   Middle: " + MiddleBoundry + "    Touch Pos: " + touchPosition);
            }
            else
            {
                ClickFlag = 0; // Left

                //Debug.Log("Diff: " + (touchPosition - MiddleBoundry) + "   Middle: " + MiddleBoundry + "    Touch Pos: " + touchPosition);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ClickFlag = -1; // No Click
        } 
    }

    public int getInputFlag()
    {
        return ClickFlag;
    }
   
}
