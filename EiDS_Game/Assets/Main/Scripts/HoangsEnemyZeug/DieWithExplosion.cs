using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DieWithExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject deadBoss;
    private BossAI parent;
    private Animator animator;
    NotMyTrailRenderer trailRenderer;
    private void Start()
    {
        parent = transform.parent.GetComponent<BossAI>();
        animator = GetComponent<Animator>();
        trailRenderer = GetComponent<NotMyTrailRenderer>();
    }
    // Update is called once per frame
    private void Update()
    {
        if (parent.GetHealth() <= 0)
        {
            Die();
        }
    }
 
public void Delete()
    {

        foreach (SpriteRenderer sprite in trailRenderer.GetCloneList())
        {
            Destroy(sprite);
        }
        Destroy(transform.parent.gameObject);
        Instantiate(deadBoss, transform.parent.position, Quaternion.identity);
    }
    private void Die()
    {
        animator.SetTrigger("death");
    }
}
