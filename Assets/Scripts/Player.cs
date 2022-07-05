using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private Vector3 _center;

    [SerializeField]
    private float _startSpeed;

    [SerializeField]
    private AudioClip _moveClip, _loseClip, _winClip;

    private float speed,startRadius;

    private void Awake()
    {
        speed = _startSpeed;
        startRadius = Vector3.Distance(transform.position, _center);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            speed *= -1f;
            AudioManager.Instance.PlaySound(_moveClip);
        }
    }

    private void FixedUpdate()
    {
        Vector3 down = transform.position - _center;
        Vector3 forward = Vector3.Cross(down, Vector3.forward).normalized;
        Vector3 temp = transform.position;
        temp += speed * Time.fixedDeltaTime * forward;
        float currentRadius = Vector3.Distance(temp, _center);
        if (currentRadius > startRadius)
        {
            float extraRadius = currentRadius - startRadius;
            Vector3 offset = (temp - _center).normalized;
            temp -=  extraRadius *offset;
        }
        transform.position = temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.Tags.SCORE))
        {
            GameManager.Instance.UpdateScore();
            AudioManager.Instance.PlaySound(_winClip);
            return;
        }
        if (collision.CompareTag(Constants.Tags.OBSTACLE))
        {
            GameManager.Instance.EndGame();
            AudioManager.Instance.PlaySound(_loseClip);
            var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            Destroy(gameObject);
            return;
        }
    }


}