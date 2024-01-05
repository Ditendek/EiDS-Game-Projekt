using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    public float firerate;
    float nextfire;
    public SwitchWeapon weapon;

    //Swords
    public GameObject swordRight;
    public GameObject swordLeft;
    public GameObject swordFront;
    public GameObject swordBack;

    public Animator leftSword;
    public Animator rightSword;
    public Animator frontSword;
    public Animator backSword;


    void Start()
    {
        anim = GetComponent<Animator>();
        swordRight.SetActive(false);
        swordLeft.SetActive(false);
        swordFront.SetActive(false);
        swordBack.SetActive(false);

    }
    
    void Update()
    {
        bool weaponswitch = weapon.weaponswitch;

        if (!weaponswitch)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                ChangeAnimationState("Attack_Back_Bow");
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                ChangeAnimationState("Attack_Front_Bow");
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                ChangeAnimationState("Attack_Right_Bow");
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                ChangeAnimationState("Attack_Left_Bow");
            }
        }
        else if (weaponswitch)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                swordBack.SetActive(true);
                ChangeSwordAnimationState("Attack_Back_Sword", backSword);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                swordFront.SetActive(true);
                ChangeSwordAnimationState("Attack_Front_Sword", frontSword);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                swordRight.SetActive(true);
                ChangeSwordAnimationState("Attack_Right_Sword", rightSword);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                swordLeft.SetActive(true);
                ChangeSwordAnimationState("Attack_Left_Sword", leftSword);
            }
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (Time.time > nextfire)
        {
            nextfire = Time.time + firerate;
            anim.Play(newState, 0, 0);
        }
    }

    void ChangeSwordAnimationState(string newState, Animator anim)
    {
        if (Time.time > nextfire)
        {
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

}
