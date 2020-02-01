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

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 moveDir_)
    {
        Vector3 move = new Vector3(moveDir_.x * m_speed, 0.0f, moveDir_.y * m_speed);
        m_rb.velocity = move;
    }
}
