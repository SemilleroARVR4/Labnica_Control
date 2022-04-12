using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinamica_Tanque : MonoBehaviour
{
    public int U;
    public int K;
    public int Tao;
    public float y;
    private float t;

    public GameObject liquido;
    //Los parametros m_EscalaLiq y m_PosicionLiq son los que ayudan a generar la animaci�n de un tanque con liquido.
    private Vector3 m_EscalaLiq;
    private Vector3 m_PosicionLiq;
    private float m_Tama�oTanque = 10.0f;

    // Update is called once per frame
    void Update()
    {
        t = Time.time;
        m_EscalaLiq = liquido.transform.localScale;
        m_PosicionLiq = liquido.transform.localPosition;


        y = (U * K - U * K * Mathf.Exp(-t/Tao)) / 10;
        print(y);
        //float escalado = Remap(m_EscalaLiq.y,0f, 10f, 0f, 100f);


        ManejoContenido(y);
    }

    private void ManejoContenido(float escalado)
    {
        if (m_EscalaLiq.y <= m_Tama�oTanque || m_EscalaLiq.y == 0)
        {
            liquido.transform.localScale = new Vector3(m_EscalaLiq.x, m_EscalaLiq.y + escalado, m_EscalaLiq.z);
            liquido.transform.localPosition = new Vector3(m_PosicionLiq.x, m_PosicionLiq.y + escalado , m_PosicionLiq.z);
        }
    }


    public float Remap(float x, float x1, float x2, float y1, float y2)
    {
        var m = (y2 - y1) / (x2 - x1);
        var c = y1 - m * x1; // point of interest: c is also equal to y2 - m * x2, though float math might lead to slightly different results.

        return m * x + c;
    }



}
