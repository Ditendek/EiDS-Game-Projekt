using System.Collections;
using UnityEngine;

public class SummonThunder : AttackMode
{
    [SerializeField] private Transform pfThunder;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    private Color attackColor = new Color(138f / 255f, 62f / 255f, 246f / 255f);

    void Start()
    {
      spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public override void Attack()
    {
        StartCoroutine(AttackAnimation());
        Instantiate(pfThunder, PlayerMovement.PlayerPosition, Quaternion.identity);        
    }
    IEnumerator AttackAnimation()
    {
        spriteRenderer.color = attackColor;
        yield return new WaitForSeconds(0.2f);       
        spriteRenderer.color = Color.white;
    }
}
