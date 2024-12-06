using UnityEngine;

public class Laser : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D p_collision)
    {
        GetComponent<SpriteRenderer>().color = new(255, 0, 0);
    }
}
