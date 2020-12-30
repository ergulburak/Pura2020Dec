using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSync : MonoBehaviour
{
    [SerializeField]
    private GameObject muzzle;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = muzzle.transform.position;
    }
}