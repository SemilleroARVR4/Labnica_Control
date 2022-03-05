using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actuator : MonoBehaviour
{
    [SerializeField]
    GameObject motor;

    float angulo = 0.0f;
    public float velocidad = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Initial rotation: " + transform.rotation.eulerAngles);
        //Debug.Log("Initial local rotation: " + transform.localRotation.eulerAngles);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        angulo = motor.GetComponent<Control_law>().u;
        Quaternion tar = Quaternion.Euler(0, 0, 90 + angulo);
        transform.rotation = Quaternion.Slerp(transform.rotation, tar, velocidad * Time.deltaTime);
    }
}
