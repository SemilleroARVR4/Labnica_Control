using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control_LQR : MonoBehaviour
{
    [SerializeField]
    GameObject distanceSensor;

    public float r = 0.5f;
    public Slider setPoint;
    public float y = 0.0f;
    // esfuerzo de control 
    public float u = 0.00f;
    public float y2 = 0.00f;

    float x1next, x2next, x3next, x4next, x5next;
    float x1 = 0.0f, x2 = 0.0f, x3 = 0.0f, x4 = 0.0f, x5 = 0.0f;
    // sample time
    public float h = 0.05f;

    // constantes del controlador con manejo desde el inspector
    /*
    public float kp = 1.00f;
    public float ki = 0.05f;
    public float kd = 0.01f;
    */


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Control_loop", 0, h);
    }

    void Control_loop()
    {
       // r = (setPoint.value)/10;
        //r = Remap(r, 0, 27.1f, 0, 1);
        Read_process_output();
        Calculate_control(r, y , y2);
    }

    void Read_process_output()
    {
        y = distanceSensor.GetComponent<Sensor>().hit.distance;
        //Debug.Log(y);
        y = Remap(y, 0, 28.8f, -1, 1);
    }

    void Calculate_control(float r, float y, float y2)
    {
        /*
        float e = r - y;        
        eAcum += e * h;
        u = kp * e + ki * eAcum + kd * ((e - eAnte) / h);
        eAnte = e;
        // funciones que deberian desaparecer despues
        u = Constrain(u, -100, 100);
        u = Remap(u, -100, -45, 100, 45);
        */
        r  = r - 0f;
        y  = y - 0f;
        y2 = y2 - 0f;

        //Debug.Log("X1: " + x1 + " X2: " + x2 + " X3: " + x3 + " X4: " + x4 + " X5: " + x5);
        // ecuacion de estados 
        x1next = 0.9913f * x1 + 0f * x2 +    0f * x3 + 0f * x4 + 0f * x5 + -0.0008932f * r + 0.1118f * y + 0.008784f * y2;
        x2next = 0f * x1 + 0.5728f * x2 + 0f * x3 + 0f * x4 + 0 * x5 + 0.002502f * r + -1.258f * y + 0.8431f * y2;
        x3next = 0f * x1 + 0f * x2 + 0.03393f * x3 + 0f * x4 + 0f * x5 + 0.0001671f * r + -4.572f * y + 3.316f * y2;
        x4next = 0f * x1 + 0f * x2 + 0f * x3 + 0.009809f * x4 + 0f * x5 + -3.804e-08f * r + 0.05136f * y + -5.067f * y2;
        x5next = 0f * x1 + 0f * x2 + 0f * x3 + 0f * x4 + 1f * x5 + 0.06257f * r + 0f * y + -0.06257f * y2;

        // ecuacion de salida 
        u = -0.007176f * x1 + -4.034f * x2 + 2.629f * x3 + -0.00162f * x4 + -0.004423f * x5;

        x1 = x1next;
        x2 = x2next;
        x3 = x3next;
        x4 = x4next;
        x5 = x5next;

        // Debug.Log(x1);
        // u = (u * 180f) / 3.1416f;
        //Debug.Log((u * 180f) / 3.1416f);
        u = u > 25 ? 25 : u;
        u = u < -25 ? -25 : u;
        //Debug.Log(u);

    }

    public float Remap(float x, float x1, float x2, float y1, float y2)
    {
        var m = (y2 - y1) / (x2 - x1);
        var c = y1 - m * x1; // point of interest: c is also equal to y2 - m * x2, though float math might lead to slightly different results.

        return m * x + c;
    }

    public float Constrain(float value, float limInf, float limSup)
    {
        if (value > limSup)
            value = limSup;
        else if (value < limInf)
            value = limInf;

        return value;
    }
}