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
    public float h = 1f;

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
        y = Remap(y, 0, 30.01978f, -1, 1);
    }

    void Calculate_control(float r, float y)
    {

        r = r - 0f;
        y = y - 0f;

        Debug.Log("X1: " + x1 + " X2: " + x2 + " X3: " + x3 + " X4: " + x4 + " X5: " + x5);
        // ecuacion de estados 
        x1next = 0.99f * x1 + 0.92f * x2 + 0.01f * x3 + 0.00f * x4 + -0.00f * x5 + 0.00f * r + -0.91f * y;
        x2next = -0.00f * x1 + 0.33f * x2 + -0.00f * x3 + 0.01f * x4 + -0.00f * x5 + 0.00f * r + 0.67f * y;
        x3next = -2.19f * x1 + 1.96f * x2 + 0.61f * x3 + 0.65f * x4 + -0.32f * x5 + 0.00f * r + -1.18f * y;
        x4next = -0.12f * x1 + -3.48f * x2 + -0.00f * x3 + 1.01f * x4 + -0.00f * x5 + 0.00f * r + 3.48f * y;
        x5next = 0.00f * x1 + 0.00f * x2 + 0.00f * x3 + 0.00f * x4 + 1.00f * x5 + 0.01f * r + -0.01f * y;

        // ecuacion de salida 
        u = -(-5.48f * x1 + 2.94f * x2 + -0.97f * x3 + 1.64f * x4 + -0.79f * x5);

        //Debug.Log(u);
        // actualizar estados
        //if (Time.time > 3f) 
        //{
            x1 = x1next;
            x2 = x2next;
            x3 = x3next;
            x4 = x4next;
            x5 = x5next;
        //}
        // Debug.Log(x1);
        //u = (x1 * 180f) / 3.1416f;
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
