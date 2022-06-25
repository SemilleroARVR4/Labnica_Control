using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actuator : MonoBehaviour
{
    [SerializeField]
    GameObject motor;

    public float velocidad = 10.0f;
    public float torque;
    public Rigidbody rb;
    public float turn = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > 3) 
        {
            torque = motor.GetComponent<Control_LQR>().u;
            //Debug.Log(90 + angulo);
            rb.AddTorque(new Vector3 (0f,0f,1f) * torque * turn);
            //Quaternion tar = Quaternion.Euler(90 + angulo, 90, -90);
            //transform.rotation = Quaternion.Slerp(transform.rotation, tar, velocidad * Time.deltaTime);
        }
    }
}
