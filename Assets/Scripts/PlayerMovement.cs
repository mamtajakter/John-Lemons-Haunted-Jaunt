using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    //dot
    public Text alertText;
    public Transform game1;
    public float dotProduct;
    public Vector3 playerPosNorm;
    public Vector3 enemyPosNorm;

    //lerp
    public Transform fromPlayer;
    public Transform toTarget;
    public float fract;

    void Start ()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();

        alertText.text="";
    }

    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);
        
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);


        // dot
        playerPosNorm = gameObject.transform.position.normalized;
        enemyPosNorm = game1.transform.position.normalized;
        dotProduct = Vector3.Dot(playerPosNorm, enemyPosNorm);
        if (dotProduct>0.99)
        {
            alertText.text = "Watch out!";
        }
        else
        {
            alertText.text = "";
        }


        // lerp
        // transform.position = Vector3.Lerp(fromPlayer.position, toTarget.position,fract);
        toTarget.localScale = Vector3.Lerp(toTarget.localScale, toTarget.localScale*2,fract);
            
        
    }

    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }
}
