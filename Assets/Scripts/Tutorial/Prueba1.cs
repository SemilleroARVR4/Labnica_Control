using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prueba1 : MonoBehaviour
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
    public float velocidad = 5f;


    // Start is called before the first frame update
    void Start()
    {
        SerialManagerScript.WhenReceiveDataCall += ReceiveData;
        distanciaUnityAnt = ((float)Math.Round(mEsfera.transform.position.x, 2));
    }

    void ReceiveData(string incomingString) 
    {

        distanciaUnity = ((float)Math.Round(mEsfera.transform.position.x, 2));

        dataArray = incomingString.Split(new char[] { 'a' });
        if (dataArray.Length  > 1) 
        {
           Debug.Log(dataArray[0] + " - " + dataArray[1]);
           float.TryParse(dataArray[0], out angulo);
           float.TryParse(dataArray[1], out distancia);

           angulo /= 100;
           distancia /= 100;
        }

        if (distanciaUnity != distanciaUnityAnt)
        {
            SerialManagerScript.SendInfo(distanciaUnity.ToString() + "\n");
            distanciaUnityAnt = distanciaUnity;
        }

        mTextAngulo.text = angulo.ToString();
        mTextDistanciaActual.text = distanciaUnity.ToString();
        mTextDistancia2.text = distancia.ToString();
    }

    private void FixedUpdate()
    {
       Quaternion tar = Quaternion.Euler(angulo, 90, 0);
       mCubo.transform.rotation = Quaternion.Slerp(mCubo.transform.localRotation, tar, velocidad * Time.deltaTime);
    }
}
