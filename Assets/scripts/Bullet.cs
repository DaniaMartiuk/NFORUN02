using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10F;
    private Vector3 direction;

    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
    }
}
