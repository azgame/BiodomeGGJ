using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{

    public  bool isRight;
    public bool isLeft;
    public Animator anim;

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
        anim = GetComponent<Animator>();
        m_rb = this.gameObject.GetComponent<Rigidbody>();
    }


     void Update()
    {
        
    }

    public void Move(Vector2 moveDir_, float dash_)
    {
        Vector3 dash = new Vector3(moveDir_.x * dash_ * m_dashSpeed, 0.0f, moveDir_.y * dash_ * m_dashSpeed);
        Vector3 move = new Vector3(moveDir_.x * m_speed, 0.0f, moveDir_.y * m_speed);
        Rotating(move);
        m_rb.velocity = move;
        m_rb.AddForce(dash, ForceMode.Impulse);
    }

    void Rotating(Vector3 move_)
    {

        if (move_.x != 0.0f || move_.z != 0.0f)
        {
            Vector3 lookAt = new Vector3(move_.x, 0.0f, move_.z).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt).normalized, 0.2f);
        }
    }


    public void  Flip(){



    }

}
