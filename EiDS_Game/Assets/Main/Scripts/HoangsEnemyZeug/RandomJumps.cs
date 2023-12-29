using UnityEngine;

public class RandomJumps : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce = 4f;
    private Vector2 randomDirection;
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    // Methode für das Animationsevent
    public void TriggerRandomJump()
    {  
        rb.velocity = randomDirection * jumpForce;     
    }
    public void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    public void AdjustScale()
    {
        randomDirection = Random.insideUnitCircle.normalized;

        if (randomDirection.x < 0.1f && transform.localScale.x > 0 ||
            randomDirection.x > 0.1f && transform.localScale.x < 0)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

    }
}
