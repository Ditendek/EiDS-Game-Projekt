using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackLeft : MonoBehaviour
{
    private Animator anim;
    public float firerate;
    float nextfire;
    private bool damage = false;
    public SpriteRenderer spriteRenderer;
    public Collider2D swordCollider;

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer.enabled = false;
        swordCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeAnimationState("Attack_Left_Sword");
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (Time.time > nextfire)
        {
            spriteRenderer.enabled = true;
            swordCollider.enabled = true;
            nextfire = Time.time + firerate;
            anim.Play(newState, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(10);
        }
    }

    void swordInvisible()
    {
        spriteRenderer.enabled = false;
        swordCollider.enabled = false;
    }
}
