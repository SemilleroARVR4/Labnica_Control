using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sensor : MonoBehaviour
{
    public RaycastHit hit;
    public GameObject distancePanel;
    public TextMeshProUGUI distanceTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            distancePanel.transform.localPosition = new Vector3(-15 + hit.distance/2 , 0, 0);
            distanceTxt.text = "X = " + hit.distance.ToString();
            //Debug.Log("Did Hit ... Distance: " + hit.distance);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * hit.distance;
        Gizmos.DrawRay(transform.position, direction);
    }
}
