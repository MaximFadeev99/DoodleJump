using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Player _player;

    private Vector3 _initialPosition;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void OnEnable()
    {
        Death.playerDied += ResetToBeginning;
    }

    private void OnDisable()
    {
        Death.playerDied -= ResetToBeginning;
    }

    private void Update()
    {
        if (_player.transform.position.y > transform.position.y) 
        {
            transform.position = new Vector3
                (transform.position.x, _player.transform.position.y, transform.position.z);
        }
    }

    private void ResetToBeginning()
    {      
        transform.position = _initialPosition;
    }
}
