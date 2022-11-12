using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private Vector3[] GetSphereDirections(int numDirections)
    {
        var pts = new Vector3[numDirections];
        var inc = Math.PI * (3 - Math.Sqrt(5));
        var off = 2f / numDirections;
 
        foreach (var k in Enumerable.Range(0, numDirections))
        {
            var y = k * off - 1 + (off / 2);
            var r = Math.Sqrt(1 - y * y);
            var phi = k * inc;
            var x = (float)(Math.Cos(phi) * r);
            var z = (float)(Math.Sin(phi) * r);
            pts[k] = new Vector3(x, y, z);
        }
 
        return pts;
    }

    // Update is called once per frame
    void Update()
    {
        Transform player = GameObject.Find("Player").transform;
        int maxSteps = 100;
        float sumDistance = 0;
        for (int i = 0; i < maxSteps; i++)
        {
            foreach (var direction in GetSphereDirections(maxSteps))
            {
                Debug.DrawRay(player.position, direction, Color.red);
                RaycastHit hit;
                if (Physics.Raycast(player.position, direction, out hit))
                {
                    sumDistance += hit.distance;
                    Debug.Log(player.transform);
                }
            }
        }
        
        // Calculate the average distance
        float averageDistance = sumDistance / maxSteps;
        
        // Print the average distance
        Debug.Log(averageDistance);
    }
}