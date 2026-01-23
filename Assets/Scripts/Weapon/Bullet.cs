using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    public Vector3 Direction { get; set; }
    public float Damage { get; set; }

    void Update()
    {
        transform.Translate(Direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
