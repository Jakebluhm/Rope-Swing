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
    private  float [,] LiftCoefficentData;
    private bool GlidePressed;
    // Start is called before the first frame update
    void Start()
    {
        LiftCoefficentData = new float[53, 2] { 
            { -45f, -0.8f},
            { -43f, -0.81f},
            { -41f, -0.82f},
            { -39f, -0.83f},
            { -37f, -0.83f},
            { -35f, -0.82f},
            { -33f, -0.81f},
            { -31f, -0.80f},

            { -29f, -0.80f},
            { -27f, -0.73f},
            { -25f, -0.67f},
            { -23f, -0.63f},
            { -21f, -0.6f},
            { -19f, -0.8f},
            { -17f, -0.8f},
            { -15f, -0.8f},

            { -13f, -0.8f},
            { -11f, -0.8f},
            { -9f, -0.8f},
            { -7f, -0.8f},
            { -5f, -0.8f},
            { -3f, -0.8f},
            { -1f, -0.8f},
            { 1f, -0.8f},
            { 3f, -0.8f},
            { 5f, -0.8f},
            { 7f, -0.8f},
            { 9f, -0.8f},
            { 11f, -0.8f},
            { 13f, -0.8f},
            { 15f, -0.8f},
            { 17f, -0.8f},
            { 19f, -0.8f},
            { 21f, -0.8f},
            { 23f, -0.8f},
            { 25f, -0.8f},
            { 27f, -0.8f},
            { 29f, -0.8f},
            { 31f, -0.8f},
            { 33f, -0.8f},
            { 35f, -0.8f},
            { 37f, -0.8f},
            { 39f, -0.8f},
            { 41f, -0.8f},
            { 43f, -0.8f},
            { 45f, -0.8f},
            { 47f, -0.8f},
            { 49f, -0.8f},
            { 51f, -0.8f},
            { 53f, -0.8f},
            { 55f, -0.8f},
            { 57f, -0.8f},
            { 59f, -0.8f},

        };
        float startAngle = -90f;
        Player = GameObject.FindWithTag("Player");
        //Player.transform.rotation = new Quaternion(0, 0, -90, 0);
        Glider.transform.position = Player.transform.position + new Vector3(0, 5, 0);
        Player.transform.eulerAngles = new Vector3(0, 0, startAngle);
        Glider.transform.eulerAngles = new Vector3(65, 0, 90 + startAngle);
        //Player.GetComponent<Rigidbody2D>().gravityScale = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        bool firstPress = false;
        float tilt = 1f;
        float drag = 0.1f;
        if (Input.GetKey("a"))
        {
            //Player.GetComponent<Rigidbody2D>().drag = drag;
            setOnPressVelocity(1);
            TiltUpPressed = true;
            Glider.transform.position = Player.transform.position + new Vector3(0, 5, 0);

            Player.transform.eulerAngles = Player.transform.eulerAngles + new Vector3(0, 0, tilt);
            Glider.transform.eulerAngles = Glider.transform.eulerAngles + new Vector3(0, 0, tilt);


            setNewPlayerVelocity();
        }
        else if (Input.GetKey("s"))
        {
            //Player.GetComponent<Rigidbody2D>().drag = drag;
            setOnPressVelocity(2);
            GlidePressed = true;
            Glider.transform.position = Player.transform.position + new Vector3(0, 5, 0);


            setNewPlayerVelocity();
        }
        else if (Input.GetKey("d"))
        {
            //Player.GetComponent<Rigidbody2D>().drag = drag;
            setOnPressVelocity(0);
            TiltDownPressed = true;
            Glider.transform.position = Player.transform.position + new Vector3(0, 5, 0);

            Player.transform.eulerAngles = Player.transform.eulerAngles + new Vector3(0, 0, -1 * tilt);
            Glider.transform.eulerAngles = Glider.transform.eulerAngles + new Vector3(0, 0, -1 * tilt);

            setNewPlayerVelocity();

        }
        else
        {

            //Player.GetComponent<Rigidbody2D>().drag = 0f;
            GlidePressed = false;
            TiltDownPressed = false;
            TiltUpPressed = false;
            Glider.transform.position = Player.transform.position + new Vector3(0, 0, 10);
        }

    }
    public bool GliderActive()
    {
        return GlidePressed | TiltDownPressed | TiltUpPressed;
    }
    void setNewPlayerVelocity()
    {
        float Area = 0.065f;
        float airDensity = 0.6f;//1.225f; //kg/m^3
        float CurrVelo = onPressVelocity.magnitude;
        float yCompVelocity = onPressVelocity.y;
        float xCompVelocity = onPressVelocity.x;
        float tiltInAngles = (Player.transform.eulerAngles.z - 275); // 0 is vertically up  90 is horiz.  180 is vertically down
        //tiltInAngles = tiltInAngles - 90f;
        float tiltInRads =0.0174533f * tiltInAngles;
        float liftCoefficent;

        if(tiltInAngles < 0)
            liftCoefficent = 2 * Mathf.PI * (-1f*tiltInRads);
        else
            liftCoefficent = 2 * Mathf.PI * tiltInRads; //Decending glider Should get this
        float dragCoefficent = 1.28f * Mathf.Sin(tiltInRads);//
        float Lift = liftCoefficent * ((CurrVelo * CurrVelo * airDensity) / 2) * Area;
        float Drag = dragCoefficent * ((CurrVelo * CurrVelo * airDensity) / 2) * Area;
        float weight = 9.8f * Player.GetComponent<Rigidbody2D>().mass;
        float stallSpeed = Mathf.Sqrt((2f * weight * 9.8f) / (airDensity * Area* (2 * Mathf.PI * 0.785398f)));

        float VeritcalLift = Lift * Mathf.Cos(tiltInRads);
        float HorizontalLift = Lift * Mathf.Sin(tiltInRads);
        float VerticalDrag = Drag * Mathf.Sin(tiltInRads); 
        float HorizontalDrag = Drag * Mathf.Cos(tiltInRads);

        float balenceVertical = VeritcalLift + VerticalDrag - weight;
        float balenceHorizontal = HorizontalLift - HorizontalDrag;
        /*if(VeritcalLift + VerticalDrag > weight)
        {
            VeritcalLift = (VeritcalLift + VerticalDrag) - weight;
        }*/

        if (Player.GetComponent<Rigidbody2D>().velocity.magnitude > stallSpeed)
        {
            if (tiltInAngles < 0)
            {
                //layer.GetComponent<Rigidbody2D>().AddForce(new Vector2(HorizontalLift - HorizontalDrag
                //, VeritcalLift + VerticalDrag));
                Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(HorizontalLift, VeritcalLift));
                Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f * HorizontalDrag, VerticalDrag));

                balenceVertical = VeritcalLift + VerticalDrag - weight;
                balenceHorizontal = HorizontalLift - HorizontalDrag;
            }
            else
            {
                Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f*HorizontalLift - HorizontalDrag
                , VeritcalLift - VerticalDrag));

                balenceVertical = VeritcalLift - VerticalDrag - weight;
                balenceHorizontal = -1f*HorizontalLift - HorizontalDrag;
            }
        }
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
    private float getLiftCoefficent(int tilt)
    {
        float ret = -3f;

        return ret;
    }
}
