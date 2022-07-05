using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float _timeForHalfCycle;
    
    [SerializeField]
    private Vector3 _startPos, _endPos;

    private float timeElapsed, speed, totalHeight;

    private void Awake()
    {
        totalHeight = _endPos.y - _startPos.y;
        timeElapsed = (transform.localPosition.y - _startPos.y) / totalHeight;
        speed = 1 / _timeForHalfCycle;
    }

    private void FixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime * speed;
        Vector3 temp = _startPos;
        temp.y += timeElapsed * totalHeight;
        transform.localPosition= temp;

        if(timeElapsed < 0f || timeElapsed > 1f)
        {
            speed *= -1f;
        }
    }
}
