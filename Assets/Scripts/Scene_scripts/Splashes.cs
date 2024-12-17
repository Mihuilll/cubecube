using UnityEngine; 

public class Splashes : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePrefab; // Префаб кубика для эффекта

    [SerializeField]
    private float explosionForce = 5f; // Сила взрыва

    [SerializeField]
    private float timeDestroy = 1;

    [SerializeField]
    private int particleCount = 2; // Количество кубиков

    private void OnCollisionEnter(Collision collision)
    {
        CreateExplosion();
        Destroy(gameObject);
    }
    public void CreateExplosion()
    {
        for (int i = 0; i < particleCount; i++)
        {
            GameObject particle = Instantiate(particlePrefab, transform.position, Random.rotation);
            Rigidbody rb = particle.GetComponent<Rigidbody>();
            Vector3 randomDirection = Random.onUnitSphere;
            rb.AddForce(randomDirection * explosionForce, ForceMode.Impulse);
            Destroy(particle, timeDestroy);
        }
    }
    public void CreateExplosion(Vector3 position)
    {
        for (int i = 0; i < particleCount; i++)
        {
            GameObject particle = Instantiate(particlePrefab, position, Random.rotation);
            Rigidbody rb = particle.AddComponent<Rigidbody>();
            Vector3 randomDirection = Random.onUnitSphere;
            rb.AddForce(randomDirection * explosionForce, ForceMode.Impulse);
            Destroy(particle, timeDestroy);
        }
    }

}
