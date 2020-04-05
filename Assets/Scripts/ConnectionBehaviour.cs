using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionBehaviour : MonoBehaviour
{

    public void CreateConnection(Vector3 A, Vector3 B)
    {
        //position
        Vector3 mid = new Vector3((A.x + B.x) / 2, (A.y + B.y) / 2, (A.z + B.z) / 2);
        this.transform.position = mid;
        //rotation
        this.transform.LookAt(A);
        this.transform.rotation *= Quaternion.Euler(-90, 0, 0);
        //scale
        float distance = Vector3.Distance(A, B);
        this.transform.localScale = new Vector3(0.1f, distance/2, 0.1f);
    }
}
