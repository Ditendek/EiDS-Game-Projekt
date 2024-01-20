using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AimBeetle;

public class SummonTree : MonoBehaviour
{

    [SerializeField] private Transform pfTree;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<AimWithTree>().OnStab += stabTarget;
    }

    private void stabTarget(object sender, AimWithTree.OnStabEventArgs e)
    {
        Transform gameObject = Instantiate(pfTree, new Vector3(e.stabTargetPosition.x,e.stabTargetPosition.y-1.5f,e.stabTargetPosition.z), Quaternion.identity);
        Transform parent = GameObject.Find("Disposables").transform;
        gameObject.transform.parent = parent;
    }
}
