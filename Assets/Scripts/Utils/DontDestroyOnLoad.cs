using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField]
    private bool _destroy;

    void Awake()
    {
        if (!_destroy)
            DontDestroyOnLoad(transform.gameObject);
        else
            DestroyImmediate(this.gameObject);
    }
}
