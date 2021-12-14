using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveDataScript : MonoBehaviour
{
    public string data;
    public string[] dataArray;
    public int angulo;
    private int distancia;

    public GameObject mCubo;
    public GameObject mEsfera;
    public Text mTextDistanciaActual;
    public Text mTextAngulo;
    public Text mTextDistancia2;
    public float velocidad = 0.1f;
    float tiempo;
    float tiempo_anterior=0;
    float h = 200f;


    // Start is called before the first frame update
    void Start()
    {
        SerialManagerScript.WhenReceiveDataCall += ReceiveData;
    }

    void ReceiveData(string incomingString) 
    {
        //data = incomingString;
        dataArray = incomingString.Split(new char[] {'-'});  
        
        try {
            Debug.Log("Anuglo: " + dataArray[0]);
            Debug.Log("Direccion: " + dataArray[1]);
            angulo = int.Parse(dataArray[0]);
            distancia = int.Parse(dataArray[1]);
            angulo /= 100;
        } catch (Exception e) 
        { //Debug.Log("Error");

          }

    }


    private void Update()
    {
        tiempo = Time.realtimeSinceStartup;

        if ((tiempo - tiempo_anterior) > h)
        {
            mTextDistanciaActual.text = Math.Round(mEsfera.transform.position.x, 2).ToString();

            SerialManagerScript.SendInfo(Math.Round(mEsfera.transform.position.x, 2).ToString()); 
            
            tiempo_anterior = tiempo;
        }
    }

    private void FixedUpdate()
    {
        Quaternion tar =  Quaternion.Euler(angulo, 90, 0);   
        mCubo.transform.rotation = Quaternion.Slerp(mCubo.transform.localRotation, tar, velocidad * Time.deltaTime);
    }

    private void LateUpdate()
    {
        mTextAngulo.text = angulo.ToString();
        mTextDistancia2.text = distancia.ToString();
    }

}


