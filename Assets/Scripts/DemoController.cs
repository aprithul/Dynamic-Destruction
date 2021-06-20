using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoController : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit_info;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_ray, out hit_info, 100, 1 << LayerMask.NameToLayer("Destructible"), QueryTriggerInteraction.Ignore))
            {
                hit_info.collider.GetComponent<DestroyedPieceController>().cause_damage(_ray.direction * 10);
            }
        }
    }
}
