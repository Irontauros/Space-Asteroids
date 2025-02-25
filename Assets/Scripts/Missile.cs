using UnityEngine;

public class Missile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 500.0f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector2 direction)
    {
        rb.AddForce(direction * this.speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
