using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Law : MonoBehaviour
{
    public Dinamica_Tanque Planta;

    public float y;
    public float r;

    private float h = 0.5f;

    private float x1 = 0.0f;
    private float x1next;
    private float e;

    public float u;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Control_loop", 0, h);
        r = 1.0f;
    }

    void Control_loop()
    {
        Read_process_output();
        Calculate_control(r, y);
    }

    void Read_process_output()
    {
        y = Planta.y;
    }

    void Calculate_control(float r, float y)
    {
        e = r - y;

        // ecuacion de estados 
        x1next = 1.00f * x1 + 0.50f * e;

        // ecuacion de salida 
        u = 0.26f * x1;

        x1 = x1next;
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
