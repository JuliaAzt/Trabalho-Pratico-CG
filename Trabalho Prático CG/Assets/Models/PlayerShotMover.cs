﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotMover : MonoBehaviour
{
    public float speed;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}
