using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    public GameObject dust_cloud;
    public static VFXController Instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<VFXController>();
            if (!_instance)
                Debug.LogError("No VFXController in scene");

            return _instance;
        }
    }
    private static VFXController _instance;
    
    
    public void spawn_dust_cloud(Vector3 position)
    {
        Instantiate(dust_cloud, position, Quaternion.identity);
    }
}