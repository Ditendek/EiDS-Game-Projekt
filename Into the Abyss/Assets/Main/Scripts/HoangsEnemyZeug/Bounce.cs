using UnityEngine;

public class Bounce : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool bouncing;
    private int amountOfBounces = 0;
    public int maxBounces;
    public float minSpeed;
    public float maxSpeed;
    private float speed;

    private Vector3 lastVelocity;
    private Vector3 direction;
    [SerializeField] public NotMyTrailRenderer trailRenderer;

    void Awake()
    {
        animator = transform.Find("BOSS_GFX").GetComponent<Animator>();
        bouncing = false;
        rb = GetComponent<Rigidbody2D>();   
    }

     void FixedUpdate()
    {
        lastVelocity = rb.velocity; 
        if(bouncing)
        {
            rb.velocity = rb.velocity.normalized * Mathf.Clamp(speed, minSpeed, maxSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!bouncing) return;    

               
        
        amountOfBounces++;
        speed = lastVelocity.magnitude;
        direction = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);
       
   
        rb.velocity += other.contacts[0].normal * 0.01f;
        rb.velocity = direction.normalized * Mathf.Clamp(speed, minSpeed, maxSpeed);
    }
    public void StartBouncing()
    {
        trailRenderer.ClonesPerSecond = 30;
        animator.SetTrigger("roll");
        rb.bodyType = RigidbodyType2D.Dynamic;
        SetBounceAmount(0);
        Vector2 randSpeed = Random.insideUnitCircle.normalized * Mathf.Clamp(speed, minSpeed, maxSpeed); ;
        rb.AddForce(randSpeed);
        rb.velocity = rb.velocity.normalized * minSpeed;
        trailRenderer.EnableTrail();
    }

    public void SetBounceAmount(int amount)
    {
        amountOfBounces = amount;
    }
    public void SetBounceMode(bool mode)
    {
        bouncing = mode;
    }

    public int GetBounceAmount() 
    { 
        return amountOfBounces; 
    }
    public int GetMaxBounceAmount()
    {
        return maxBounces;
    }

}
