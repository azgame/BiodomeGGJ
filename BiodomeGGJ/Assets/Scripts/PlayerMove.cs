using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    // Components
    Rigidbody m_rb;

    // Exposed
    [Range(1, 20)]
    public float m_speed;

    [Range(1, 20)]
    public float m_dashSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = this.gameObject.GetComponent<Rigidbody>();
    }

    public void Move(Vector2 moveDir_, float dash_)
    {
        Vector3 dash = new Vector3(moveDir_.x * dash_ * m_dashSpeed, 0.0f, moveDir_.y * dash_ * m_dashSpeed);
        Vector3 move = new Vector3(moveDir_.x * m_speed, 0.0f, moveDir_.y * m_speed);
        m_rb.velocity = move;
        m_rb.AddForce(dash, ForceMode.Impulse);
    }
}
