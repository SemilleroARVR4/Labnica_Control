using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control_LQR : MonoBehaviour
{
    [SerializeField]
    GameObject distanceSensor;

    public float r = 0f;
    public Slider setPoint;
    public float y = 0.5f;
    // esfuerzo de control 
    public float u = 0.00f;

    float x1next, x2next, x3next, x4next, x5next;
    float x1 = 0.0f, x2 = 0.0f, x3 = 0.0f, x4 = 0.0f, x5 = 0.0f;
    // sample time
    public float h = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Control_loop", 5, h);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void Control_loop()
    {
       // r = (setPoint.value)/10;
        //r = Remap(r, 0, 27.1f, 0, 1);
        Read_process_output();
        Calculate_control(r, y);
    }

    void Read_process_output()
    {
        y = distanceSensor.GetComponent<Sensor>().hit.distance;
        //Debug.Log(y);
        y = Remap(y, 0, 28.8f, -1, 1);
    }

    void Calculate_control(float r, float y)
    {

        r = r - 0f;
        y = y - 0f;

        //Debug.Log("X1: " + x1 + " X2: " + x2 + " X3: " + x3 + " X4: " + x4 + " X5: " + x5);
        // ecuacion de estados 
        x1next =  0.74f * x1 + 0.37f * x2 - 0.01f * r + 1.50f * y;
        x2next = -0.37f * x1 + 0.74f * x2 - 5.36f * y;
        x3next =  0.16f * x3 + 0.02f * r  -47.05f * y;
        x4next =  0.29f * x4 + 0.01f * r  -45.10f * y;
        x5next =  1.00f * x5 + 0.05f * r  - 0.05f * y;

        // ecuacion de salida 
        u = -0.45f * x1 + 0.71f * x2 -1.67f * x3 + 1.40f * x4;

        x1 = x1next;
        x2 = x2next;
        x3 = x3next;
        x4 = x4next;
        x5 = x5next;

        // Debug.Log(x1);
        u = (u * 180f) / 3.1416f;
        u = u > 25 ? 25 : u;
        u = u < -25 ? -25 : u;
        Debug.Log(u);

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

