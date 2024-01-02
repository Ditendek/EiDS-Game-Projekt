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
 
    //Gets called from Explosion animation
    public void Delete()
    {
        foreach (SpriteRenderer sprite in trailRenderer.GetCloneList())
        {
            Destroy(sprite.gameObject);
        }
        Destroy(transform.parent.gameObject);
        GameObject gameobject = Instantiate(deadBoss, transform.parent.position, Quaternion.identity);
        gameobject.transform.parent = transform.parent.parent.transform;
    }
    private void Die()
    {
        animator.SetTrigger("death");
    }
}
