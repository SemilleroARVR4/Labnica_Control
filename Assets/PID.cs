using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PID : MonoBehaviour
{

    [SerializeField]
    GameObject sensorUltrasonido;

    float y = 0f;                //Distancia_Actual
    float r = 50f;               //Distancia_Deseada 
    public float u = 0f;                //Angulo

    float e = 0f;            //Error
    float eAcum = 0f;
    float eAnte = 0f;

    public float kp = 1f;//1              //Constante proporcional
    public float ki = 0.05f;//0.05
    public float kd = 0.01f;//0.01

    float tAnte = 0f;       //Tiempo Anterior
    float tActu = 0f;       //Tiempo Actual
    public float h = 0.01f;//200          //Tiempo de muestreo

    int longitud_Base = 30;     //Distancia maxima que puede alcanzar la esfera.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tActu = Time.time;
        if (tActu - tAnte > h)
        {
            tAnte = tActu;
            //Debug.Log(tAnte);
            Debug.Log("r = " + r);

            y = sensorUltrasonido.GetComponent<ultraSonido>().hit.distance;
            y = Remap(y, 0, 30, 0, 100);
            Debug.Log("y = " + y);

            e = r - y;
            Debug.Log("e = " + e);
            eAcum += e * h;

            u = kp * e + ki * eAcum + kd * ((e - eAnte) / h);

            u = Remap(u, -50, -15, 50, 15);
            //u = Constrain(u, -15, 15);
            Debug.Log("u = " + u);
        }
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
