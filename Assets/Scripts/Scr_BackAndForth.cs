using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BackAndForth : MonoBehaviour
{
     public float xMinAngle = 7.5f;
     public float xMaxAngle = 7.5f;
     public float xWobbleSpeed = 1f;
     public float yMinAngle = 170f;
     public float yMaxAngle = 190f;
     public float yWobbleSpeed = 1f;
     public float zMinAngle = 0f;
     public float zMaxAngle = 0f;
     public float zWobbleSpeed = 1f;
     
     void Update () {
         float rX = Mathf.SmoothStep(xMinAngle,xMaxAngle,Mathf.PingPong(Time.time*xWobbleSpeed,1));
         float rY = Mathf.SmoothStep(yMinAngle,yMaxAngle,Mathf.PingPong(Time.time*yWobbleSpeed,1));
         float rZ = Mathf.SmoothStep(zMinAngle,zMaxAngle,Mathf.PingPong(Time.time*zWobbleSpeed,1));
         transform.rotation = Quaternion.Euler(rX,rY,rZ);
     }
}
