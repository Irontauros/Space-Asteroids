using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    public float size = 1.0f;
    public float max = 1.5f;
    public float min = 0.5f;
    public float speed = 50.0f;
    public float lifeTime = 7.0f;
    public GameObject explosionPrefab;
    public int scoreValue = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        sr.sprite = sprites[Random.Range(0, sprites.Length)];
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;
        rb.mass = this.size * 2.0f;
    }

    public void Trajectory(Vector2 direction)
    {
        rb.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            if ((this.size * 0.5f) >= this.min)
            {
                Split();
                Split();
            }
            ScoreManager.Instance.AddScore(scoreValue);

            Destroy(this.gameObject);
        }
    }

    private void Split()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;
        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        half.speed = this.speed * 0.3f;
        half.Trajectory(Random.insideUnitCircle.normalized * this.speed);
    }
}
