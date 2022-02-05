using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManejadorDatos : MonoBehaviour
{
    [SerializeField]
    private Control_law fuenteDatos;
    public Text SP;
    public Text Y;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SP.text = Remap(fuenteDatos.r,0,100,0,30).ToString();
        Y.text = Remap(fuenteDatos.y, 0, 100, 0, 30).ToString();
    }

    public float Remap(float x, float x1, float x2, float y1, float y2)
    {
        var m = (y2 - y1) / (x2 - x1);
        var c = y1 - m * x1; // point of interest: c is also equal to y2 - m * x2, though float math might lead to slightly different results.

        return m * x + c;
    }
}
