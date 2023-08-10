using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    [SerializeField] private UnityEvent _pickedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>()) 
        {
            _pickedUp.Invoke();
            Destroy(gameObject);
        }
    }
}
