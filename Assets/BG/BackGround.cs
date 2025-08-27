using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;

public class BackGround : MonoBehaviour
{
    public Transform mainCam;
    public Transform MidBg;
    public Transform SideBG;
    public float length;
    // Update is called once per frame
    void Update()
    {
        if(mainCam.position.x>MidBg.position.x)
        {
            UpdateBackGroundPosition(Vector3.right);
        }
        else
        {
            UpdateBackGroundPosition(Vector3.left);
        }
    }

    void UpdateBackGroundPosition(Vector3 direction)
    {
        SideBG.position = MidBg.position + direction * length;
        Transform temp = MidBg;
        MidBg = SideBG;
        SideBG = temp;
    }
}
