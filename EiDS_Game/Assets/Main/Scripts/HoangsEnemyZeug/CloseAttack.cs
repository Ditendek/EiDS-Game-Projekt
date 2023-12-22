using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttack : AttackMode
{
    Animator animator;
    public Transform attackPoint;
    PathFinding pathFinding;
    private void Start()
    {
        pathFinding = GetComponent<PathFinding>();

        animator = transform.Find("SkeletonGFX").GetComponent<Animator>();
    }
    public override void Attack()
    {
        //   Debug.Log("ICH GREIFE IM NAHKAMPF AN");
        
        if (pathFinding.target.x < transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        animator.SetTrigger("Slice");
       /* if (Vector3.Distance(attackPoint.position, PlayerMovement.PlayerPosition) <1f) //MEH LÖSUNG; RANGE NÖTIG
        {
            Debug.Log("Getroffen");
            // Physics2D.OverlapCircleAll(attackPoint.position, radius: 2, layerMask)
        }*/
          StartCoroutine(WaitForSlash());

    }
    IEnumerator WaitForSlash()
    {

        yield return new WaitForSeconds(0.7f);
        if (Vector3.Distance(attackPoint.position, PlayerMovement.PlayerPosition) <0.6f) //MEH LÖSUNG; RANGE NÖTIG
        {
            Debug.Log("Getroffen");
            // Physics2D.OverlapCircleAll(attackPoint.position, radius: 2, layerMask)
        }
    }
    private void OnDrawGizmos()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, 0.6f);//range
    }
}
