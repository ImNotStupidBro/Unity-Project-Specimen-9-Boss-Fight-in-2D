using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPillar : MonoBehaviour
{
    [SerializeField] private float 
        riseSpeed = 5f,
        lifetime = 3f;
    private SpriteRenderer[] sprites;
    private BoxCollider2D col;
    public GameObject dangerZonePrefab;
    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;

        StartCoroutine(SpawnBodyPillar());
    }

}
