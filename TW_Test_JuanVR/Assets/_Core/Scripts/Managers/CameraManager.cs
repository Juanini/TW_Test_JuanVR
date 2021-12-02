using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Ins;

    public Camera cam;

    void Awake()
    {
        Ins = this;
    }
}
