using UnityEngine;

public class Patrolling : MonoBehaviour
{
    private Vector2 _rightWaypoint;
    private Vector2 _leftWaypoint;
    private bool _isMovingRight = true;

    private void Update()
    {
        GoToWaypoint(_rightWaypoint, true);
        GoToWaypoint(_leftWaypoint, false);       
    }

    private void GoToWaypoint(Vector2 newPosition, bool isMovingRight) 
    {
        float patrollingSpeed = 0.5f;

        if ((Vector2) transform.position != newPosition && _isMovingRight == isMovingRight)
        {
            transform.position = Vector2.MoveTowards((Vector2)transform.position, newPosition, patrollingSpeed * Time.deltaTime);
        }
        else
        {
            _isMovingRight = !isMovingRight;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Platform>()) 
        {
            float objectWidth = collision.gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;

            _rightWaypoint = new Vector2 (collision.transform.position.x + (objectWidth * 0.7f), transform.position.y);
            _leftWaypoint = new Vector2 (collision.transform.position.x - (objectWidth * 0.7f), transform.position.y);
        }        
    }
}
