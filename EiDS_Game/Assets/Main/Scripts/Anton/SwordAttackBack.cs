using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackBack : MonoBehaviour
{
    private Animator anim;
    public float firerate;
    float nextfire;
    private bool damage = false;
    public SpriteRenderer spriteRenderer;
    public Collider2D swordCollider;
    public bool back;
    public SwordAttackFront swordfront;
    public SwordAttackLeft swordleft;
    public SwordAttackRight swordright;

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer.enabled = false;
        swordCollider.enabled = false;
        back = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool front = swordfront.front;
        bool left = swordleft.left;
        bool right = swordright.right;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!front && !left && !right)
            {
                ChangeAnimationState("Attack_Back_Sword");
            }  
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

    void SetTrue()
    {
        back = true;
    }

    void SetFalse()
    {
        back = false;
    }
}
