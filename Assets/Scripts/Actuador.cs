using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actuador : MonoBehaviour
{
    [SerializeField]
    GameObject motor;

    float angulo = 0f;
    public float velocidad = 25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        angulo = motor.GetComponent<PID>().u;
        Quaternion tar = Quaternion.Euler(0, 0, angulo);
        transform.rotation = Quaternion.Slerp(transform.localRotation, tar, velocidad * Time.deltaTime);
    }
}
