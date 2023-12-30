using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 3f;  
    public float rotation = 0f;
    public float speed = 1f;
   
  //  private float rotationSpeedSprite = 100f;
    private Transform spriteTransform;

    private Vector2 spawnPoint;
    private float timer = 0f;



    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
        spriteTransform = transform.GetChild(0);

    }


    // Update is called once per frame
    void Update()
    {

      //  spriteTransform.Rotate(Vector3.forward, rotationSpeedSprite * Time.deltaTime);
        if (timer > bulletLife) Destroy(this.gameObject);
        timer += Time.deltaTime;
        transform.position = Movement(timer);
    }


    private Vector2 Movement(float timer)
    {
        // Moves right according to the bullet's rotation
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            return;
        }
        Destroy(gameObject);
    }

}
