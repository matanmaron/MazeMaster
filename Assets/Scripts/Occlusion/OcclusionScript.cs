using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionScript : MonoBehaviour
{
    [SerializeField] int RayAmount = 1500;
    [SerializeField] int RayDistance = 300;
    [SerializeField] float StayTime = 2;
    [SerializeField] Camera cam;
    [SerializeField] Vector2[] RPoints;

    void Start()
    {
        cam = gameObject.GetComponentInChildren<Camera>();
        RPoints = new Vector2[RayAmount];

        GetPoints();
    }

    void Update()
    {
        CastRay();
    }

    void GetPoints()
    {
        float x = 0;
        float y = 0;
        for (int i = 0; i < RayAmount; i++)
        {
            if (x>1)
            {
                x = 0;
                y += 1 / Mathf.Sqrt(RayAmount);
            }
            RPoints[i] = new Vector2(x, y);
            x += 1 / Mathf.Sqrt(RayAmount);
        }
    }

    void CastRay()
    {
        Ray ray;
        RaycastHit hit;
        OcclusionObjectScript occlusion;
        for (int i = 0; i < RayAmount; i++)
        {
            ray = cam.ViewportPointToRay(new Vector3(RPoints[i].x, RPoints[i].y, 0));
            if (Physics.Raycast(ray,out hit, RayDistance))
            {
                if (occlusion = hit.transform.GetComponent<OcclusionObjectScript>())
                {
                    occlusion.HitOcclude(StayTime);
                }
            }
        }
    }
}
