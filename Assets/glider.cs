using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glider : MonoBehaviour
{

    public GameObject Player;
    public GameObject Glider;
    private Vector2 onPressVelocity;
    private bool TiltDownPressed;
    private bool TiltUpPressed;

    private bool GlidePressed;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        //Player.transform.rotation = new Quaternion(0, 0, -90, 0);
        Glider.transform.position = Player.transform.position + new Vector3(0, 5, 0);
        Player.transform.eulerAngles = new Vector3(0, 0, -70);

        //Player.GetComponent<Rigidbody2D>().gravityScale = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        bool firstPress = false;
        float tilt = 1f;
        float drag = 0.5f;
        if (Input.GetKey("a"))
        {
            Player.GetComponent<Rigidbody2D>().drag = drag;
            setOnPressVelocity(1);
            TiltUpPressed = true;
            Glider.transform.position = Player.transform.position + new Vector3(0, 5, 0);

            Player.transform.eulerAngles = Player.transform.eulerAngles + new Vector3(0, 0, tilt);
            Glider.transform.eulerAngles = Glider.transform.eulerAngles + new Vector3(0, 0, tilt);


            setNewPlayerVelocity();
        }
        else if (Input.GetKey("s"))
        {
            Player.GetComponent<Rigidbody2D>().drag = drag;
            setOnPressVelocity(2);
            GlidePressed = true;
            Glider.transform.position = Player.transform.position + new Vector3(0, 5, 0);


            setNewPlayerVelocity();
        }
        else if (Input.GetKey("d"))
        {
            Player.GetComponent<Rigidbody2D>().drag = drag;
            setOnPressVelocity(0);
            TiltDownPressed = true;
            Glider.transform.position = Player.transform.position + new Vector3(0, 5, 0);

            Player.transform.eulerAngles = Player.transform.eulerAngles + new Vector3(0, 0, -1 * tilt);
            Glider.transform.eulerAngles = Glider.transform.eulerAngles + new Vector3(0, 0, -1 * tilt);

            setNewPlayerVelocity();

        }
        else
        {

            Player.GetComponent<Rigidbody2D>().drag = 0f;
            GlidePressed = false;
            TiltDownPressed = false;
            TiltUpPressed = false;
            Glider.transform.position = Player.transform.position + new Vector3(0, 0, 10);
        }

    }
    void setNewPlayerVelocity()
    {
        float Area = 1f;
        float airDensity = 1.225f; //kg/m^3
        Vector2 newVelocity;
        float CurrVelo = onPressVelocity.magnitude;
        float yCompVelocity = onPressVelocity.y;
        float xCompVelocity = onPressVelocity.x;
        float tiltInAngles = (Player.transform.eulerAngles.z - 185f) ; // 0 is vertically up  90 is horiz.  180 is vertically down
        float tiltInRads = Mathf.Deg2Rad * tiltInAngles;
        float velocityWeight;
        tiltInAngles = tiltInAngles - 90f;
        float liftCoefficent = 2 * Mathf.PI * tiltInRads;
        float Lift = liftCoefficent *
            ((CurrVelo * CurrVelo * airDensity) / 2) * Area;
        float VeritcalLift = Lift * Mathf.Cos(tiltInRads);
        float HorizontalLift = Lift * Mathf.Sin(tiltInRads);
        Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.01f*HorizontalLift
            , 0.01f * VeritcalLift)); 
       /* if (tiltInAngles > 90) //Tilted Up
        {
            //velocityWeight = 1 - (tiltInAngles / 180f);
            //float newX = xCompVelocity - Mathf.Abs((yCompVelocity * velocityWeight));
            //float newY = yCompVelocity + Mathf.Abs((velocityWeight * yCompVelocity));

            //newVelocity = new Vector2(newX, newY);
            
            Debug.Log(velocityWeight);
        }
        else if (tiltInAngles < 90)//Tilted Down
        {
            //velocityWeight = ((tiltInAngles - 90) / 180f) + 0.5f;
            //float newX = xCompVelocity + Mathf.Abs((xCompVelocity * velocityWeight));
            //float newY = yCompVelocity - Mathf.Abs((velocityWeight * xCompVelocity));
            //newVelocity = new Vector2(newX, newY);
            Debug.Log(velocityWeight);
        }
        else
        {
            //velocityWeight = 1 - (tiltInAngles / 180f);
            //float newX = onPressVelocity.x - Mathf.Abs(velocityWeight * yCompVelocity);
            //float newY = onPressVelocity.y - velocityWeight * yCompVelocity;
            
            //newVelocity = new Vector2(newX, newY);

        }
        //Player.GetComponent<Rigidbody2D>().velocity = newVelocity;
        */

    }
    void setOnPressVelocity(int tilt) //tilt up - 1      tilt down - 0    glide - 2
    {
        if(tilt == 2 && !GlidePressed)
        {
            Player.transform.eulerAngles = Player.transform.eulerAngles + new Vector3(0, 0, tilt);
            Glider.transform.eulerAngles = Glider.transform.eulerAngles + new Vector3(0, 0, tilt);

        }
        if (!TiltDownPressed && !TiltUpPressed && !GlidePressed)
        {
            onPressVelocity = Player.GetComponent<Rigidbody2D>().velocity;
        }
       
    }
}
