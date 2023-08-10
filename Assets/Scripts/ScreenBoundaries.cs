using UnityEngine;

public class ScreenBoundaries : MonoBehaviour
{
    private Vector2 _screenStartPoint;
    private float _newPositionX;
    private float _objectWidth;

    private void Start()
    {
        _screenStartPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        _objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    private void LateUpdate()
    {        
        _newPositionX = Mathf.Clamp(transform.position.x, _screenStartPoint.x + _objectWidth, -_screenStartPoint.x - _objectWidth);
        transform.position = new Vector2(_newPositionX, transform.position.y);
    }
}
