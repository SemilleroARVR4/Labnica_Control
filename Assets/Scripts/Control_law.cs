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
    public float kp = 1.00f;
    public float ki = 0.05f;
    public float kd = 0.01f;
    // variables para almacenar error
    float eAcum = 0.00f;
    float eAnte = 0.00f;
    // sample time
    public float h = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Control_loop", 1, h);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void Control_loop()
    {
        r = SetPoint.value;
        r = Remap(r, 0, 30, 0, 100);
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
        float e = r - y;        
        eAcum += e * h;
        u = kp * e + ki * eAcum + kd * ((e - eAnte) / h);
        eAnte = e;
        // funciones que deberian desaparecer despues
        u = Constrain(u, -100, 100);
        u = Remap(u, -100, -45, 100, 45);
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
