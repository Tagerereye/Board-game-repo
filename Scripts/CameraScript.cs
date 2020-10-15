using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript instance;
    private void Awake()
    {
        instance = this;
    }
}
