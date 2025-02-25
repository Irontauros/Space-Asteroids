using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float rate = 2.0f;
    public int amount = 1;
    public Asteroid asteroidPrefab;
    public float distance = 15.0f;
    public float trajectory = 15.0f;

    private float currentRate;
    private float currentSpeedMultiplier = 1.0f;
    private const float rateIncrement = 0.05f;
    private const float speedIncrement = 0.1f;

    void Start()
    {
        currentRate = rate;
        InvokeRepeating(nameof(Spawning), currentRate, currentRate);
    }

    private void Spawning()
    {
        for (int i = 0; i < this.amount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.distance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;
            float variance = Random.Range(-this.trajectory, this.trajectory);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.min, asteroid.max);
            asteroid.speed *= currentSpeedMultiplier;
            asteroid.Trajectory(rotation * -spawnDirection);
        }
    }

    public void IncreaseSpawnRateAndSpeed()
    {
        currentRate = Mathf.Max(0.1f, currentRate - rateIncrement);
        currentSpeedMultiplier += speedIncrement;

        CancelInvoke(nameof(Spawning));
        InvokeRepeating(nameof(Spawning), currentRate, currentRate);
    }
}
