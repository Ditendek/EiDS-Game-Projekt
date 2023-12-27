using UnityEngine;

public class DieWithExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject deadBoss;
    private Enemy parent;
    private Animator animator;
    NotMyTrailRenderer trailRenderer;
    private void Start()
    {
        parent = transform.parent.GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        trailRenderer = GetComponent<NotMyTrailRenderer>();
        if (parent == null) Debug.Log("RIP");
    }
    // Update is called once per frame
    private void Update()
    {
        if (parent.GetEnemyHealth() <= 0)
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
