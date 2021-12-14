using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTIempos : MonoBehaviour
{
    public string[] dataArray;

    public float angulo;
    //public float distancia;
    public float distanciaUnity;
    public float distancia;


    private float distanciaUnityAnt;

    public GameObject mCubo;
    public GameObject mEsfera;
    public Text mTextDistanciaActual;
    public Text mTextAngulo;
    public Text mTextDistancia2;
    public float velocidad = 0.1f;
    float tiempo;
    float tiempo_anterior = 0;
    float h = 0f; //0.7
    bool _mEnvio = false;

    float tiempoPAnt = 0f;
    float tiempoPActual = 0;

    // Start is called before the first frame update
    void Start()
    {
        SerialManagerScript.WhenReceiveDataCall += ReceiveData;
        distanciaUnityAnt = ((float)Math.Round(mEsfera.transform.position.x, 2));
    }

    void ReceiveData(string incomingString)
    {
        tiempoPActual = Time.realtimeSinceStartup;
        tiempo = Time.realtimeSinceStartup;
        //Debug.Log(tiempoPActual - tiempoPAnt);
        tiempoPAnt = tiempoPActual;


        dataArray = incomingString.Split(new char[] { 'a' });
        if (dataArray.Length > 1)
        {
            Debug.Log(dataArray.Length);
            Debug.Log(dataArray[0] + " - " + dataArray[1]);
            float.TryParse(dataArray[0], out angulo);
            float.TryParse(dataArray[1], out distancia);

            angulo /= 100;
            distancia /= 100;
        }

        distanciaUnity = ((float)Math.Round(mEsfera.transform.position.x, 2));
        if ((tiempo - tiempo_anterior) > h && distanciaUnity != distanciaUnityAnt)
        {
            //distanciaUnity = ((float)Math.Round(mEsfera.transform.position.x, 2));
            //distanciaUnity = (mEsfera.transform.position.x);
            SerialManagerScript.SendInfo(distanciaUnity.ToString() + "\n");
            tiempo_anterior = tiempo;
            distanciaUnityAnt = distanciaUnity;
        }

        mTextAngulo.text = angulo.ToString();
        mTextDistanciaActual.text = distanciaUnity.ToString();
        mTextDistancia2.text = distancia.ToString();





        /*
        tiempo = Time.realtimeSinceStartup;
        distanciaUnity = ((float)Math.Round(mEsfera.transform.position.x, 2));
        if ((tiempo - tiempo_anterior) > h && distanciaUnity != distanciaUnityAnt && _mEnvio)
        {
            //distanciaUnity = ((float)Math.Round(mEsfera.transform.position.x, 2));
            //distanciaUnity = (mEsfera.transform.position.x);
            SerialManagerScript.SendInfo(distanciaUnity.ToString() + "\n");
            tiempo_anterior = tiempo;
            distanciaUnityAnt = distanciaUnity;
            _mEnvio = false;
        }
       
        mTextAngulo.text = angulo.ToString();
        mTextDistanciaActual.text = distanciaUnity.ToString();
        mTextDistancia2.text = distancia.ToString();*/
    }

    private void FixedUpdate()
    {
        Quaternion tar = Quaternion.Euler(angulo, 90, 0);
        mCubo.transform.rotation = Quaternion.Slerp(mCubo.transform.localRotation, tar, velocidad * Time.deltaTime);
    }
}
