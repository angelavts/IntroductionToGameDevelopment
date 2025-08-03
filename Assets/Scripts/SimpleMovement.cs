using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float speed = 5f;
    
    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        // Movimiento horizontal
        if (Input.GetKey(KeyCode.A))
            moveX = -1f;
        else if (Input.GetKey(KeyCode.D))
            moveX = 1f;

        // Movimiento vertical
        if (Input.GetKey(KeyCode.W))
            moveY = 1f;
        else if (Input.GetKey(KeyCode.S))
            moveY = -1f;

        // Movimiento final
        Vector3 movement = new Vector3(moveX, moveY, 0f) * (speed * Time.deltaTime);
        transform.position += movement;
    }
}
