using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control_law : MonoBehaviour
{
    [SerializeField]
    GameObject distanceSensor;

    public Slider SetPoint;
    public float y, r;
    // esfuerzo de control 
    public float u = 0.00f;
    // constantes del controlador con manejo desde el inspector
    /*
    public float kp = 1.00f;
    public float ki = 0.05f;
    public float kd = 0.01f;
    */
    float x1next, x2next, x3next, x4next, x5next;
    float x1 = 0.0f, x2 = 0.0f, x3 = 0.0f, x4 = 0.0f, x5 = 0.0f;
    // variables para almacenar error
    float eAcum = 0.00f;
    float eAnte = 0.00f;
    // sample time
    public float h = 0.01f;

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
        r = SetPoint.value;
        //r = Remap(r, 0, 17, 0, 100);
        Read_process_output();
        Calculate_control(r, y);
    }

    void Read_process_output()
    {
        y = distanceSensor.GetComponent<Sensor>().hit.distance;
        y = Remap(y, 0, 30, 0, 100);
    }

    void Calculate_control(float r, float y)
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
        // ecuacion de estados 
        x1next = 0.98f * x1 + 0.29f * x2 + 0.01f * x3 + 0.01f * x4 + -0.02f * x5 + 0.00f * r + -0.28f * y;
        x2next = -0.00f * x1 + 0.84f * x2 + -0.00f * x3 + 0.01f * x4 + -0.00f * x5 + 0.00f * r + 0.16f * y;
        x3next = -3.13f * x1 + 3.70f * x2 + 0.59f * x3 + 1.29f * x4 + -3.14f * x5 + 0.00f * r + -0.80f * y;
        x4next = -0.13f * x1 + -0.82f * x2 + -0.00f * x3 + 1.01f * x4 + -0.03f * x5 + 0.00f * r + 0.85f * y;
        x5next = 0.00f * x1 + 0.00f * x2 + 0.00f * x3 + 0.00f * x4 + 1.00f * x5 + 0.01f * r + -0.01f * y;

        // ecuacion de salida 
        u = -7.83f * x1 + 8.23f * x2 + -1.03f * x3 + 3.24f * x4 + -7.85f * x5;
        // actualizar estados
        x1 = x1next;
        x2 = x2next;
        x3 = x3next;
        x4 = x4next;
        x5 = x5next;
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
