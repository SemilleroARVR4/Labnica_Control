using UnityEngine;

public class ReceiveScript : MonoBehaviour
{
    public int result;


    // Start is called before the first frame update
    void Start()
    {
        ReallySlowTaskScript.WhenResultCall += OnReceiveResult;
    }

    private void OnReceiveResult(int threadResult) 
    {
        result = threadResult;
    }
}
