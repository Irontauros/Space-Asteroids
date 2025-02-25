using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 1.0f;
    public float turn_speed = 1.0f;
    private bool moving_up;
    private bool moving_down;
    private float turn;
    public Missile missilePrefab;
    public GameObject endGame;
    public int delay = 1;
    public GameObject explosionPrefab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moving_up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        moving_down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turn = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turn = -1.0f;
        }
        else
        {
            turn = 0.0f;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (moving_up)
        {
            rb.AddForce(this.transform.up * this.speed);
        }
        if (moving_down)
        {
            rb.AddForce(-this.transform.up * this.speed);
        }
        if (turn != 0.0f)
        {
            rb.AddTorque(turn * this.turn_speed);
        }
    }

    private void Shoot()
    {
        Missile missile = Instantiate(this.missilePrefab, this.transform.position, this.transform.rotation);
        missile.Fire(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;

            StartCoroutine(EndGameAfterDelay());
        }
    }

    private IEnumerator EndGameAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        if (endGame != null)
        {
            endGame.SetActive(true);
        }

        ScoreManager.Instance.ShowEndGameScores();

        Time.timeScale = 0;
    }
}
