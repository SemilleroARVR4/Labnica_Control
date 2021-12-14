using UnityEngine;
using System.Threading;

public class ReallySlowTaskScript : MonoBehaviour
{
    public int result;
    public int timesToCompute;
    public int numberMultuply;

    public delegate void DataEvent(int result);
    public static event DataEvent WhenResultCall;

    private bool abort = false;
    private Thread secondaryThread;

    private void Start()
    {
        secondaryThread = new Thread(SlowTask);
        secondaryThread.Start();
    }

    void SlowTask() 
    {
        while (true) 
        {
            if (abort) 
            {
                secondaryThread.Abort();
                break;
            }

            for (int i = 0; i< timesToCompute; i++) 
            {
                result = numberMultuply * 2;
            }

            if (WhenResultCall != null) 
            {
                WhenResultCall(result);
            }

        }
    }

    private void OnApplicationQuit()
    {
        abort = true;
    }



}
