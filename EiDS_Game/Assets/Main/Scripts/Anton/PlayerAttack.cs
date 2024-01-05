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

    public SetBackFalse _back;
    public SetFrontFalse _front;
    public SetRightFalse _right;
    public SetLeftFalse _left;

    public float firerate2;
    float nextfire2;

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
        bool front = _front.front;
        bool left = _left.left;
        bool right = _right.right;
        bool back = _back.back;

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
            if (Input.GetKey(KeyCode.UpArrow) && !front && !left && !right)
            {
                ChangeSwordAnimationState("Attack_Back_Sword", backSword, swordBack);
            }
            else if (Input.GetKey(KeyCode.DownArrow) && !back && !left && !right)
            {
                ChangeSwordAnimationState("Attack_Front_Sword", frontSword, swordFront);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !front && !left && !back)
            {
                ChangeSwordAnimationState("Attack_Right_Sword", rightSword, swordRight);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && !front && !back && !right)
            {
                ChangeSwordAnimationState("Attack_Left_Sword", leftSword, swordLeft);
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

    void ChangeSwordAnimationState(string newState, Animator anim, GameObject _object)
    {
        if (Time.time > nextfire2)
        {
            _object.SetActive(true);
            nextfire2 = Time.time + firerate2;
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
