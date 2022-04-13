using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinamica_Tanque : MonoBehaviour
{
    private float U;
    public int K;
    public int Tao;
    public float y;
    private float t;

    public GameObject liquido;
    public Control_Law codigo_Control;
    public GameObject particula_liquido;

    //Los parametros m_EscalaLiq y m_PosicionLiq son los que ayudan a generar la animación de un tanque con liquido.
    private Vector3 m_EscalaLiq;
    private Vector3 m_PosicionLiq;
    private float m_TamañoTanque = 10.0f;

    // Update is called once per frame
    void Update()
    {
        t = Time.time;
        m_EscalaLiq = liquido.transform.localScale;
        m_PosicionLiq = liquido.transform.localPosition;
        U = codigo_Control.u;

        y = (U * K - U * K * Mathf.Exp(-t/Tao));

        Manejo_Contenido(y);
        Imprimir_Datos();
    }

    private void Manejo_Contenido(float escalado)
    {
        if (m_EscalaLiq.y <= m_TamañoTanque || m_EscalaLiq.y == 0)
        {
            liquido.transform.localScale = new Vector3(m_EscalaLiq.x, escalado, m_EscalaLiq.z);
            liquido.transform.localPosition = new Vector3(m_PosicionLiq.x, -6 + escalado , m_PosicionLiq.z);
        }
    }

    public void Imprimir_Datos()
    {
        print("U =" + U + " Y = " + y);
    }


    public float Remap(float x, float x1, float x2, float y1, float y2)
    {
        var m = (y2 - y1) / (x2 - x1);
        var c = y1 - m * x1; // point of interest: c is also equal to y2 - m * x2, though float math might lead to slightly different results.

        return m * x + c;
    }

}
