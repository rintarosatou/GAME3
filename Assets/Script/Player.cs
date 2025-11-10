using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    private Animator animator;
    public Rigidbody _rb;
    [Header("Movement Setting")]
    public float moveSpeed = 5f;
    public float jumpPower;
    private void Awake()
    {
        Debug.Log("Awake");
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.W)) vertical += 1f;
        if (Input.GetKey(KeyCode.S)) vertical -= 1f;
        if (Input.GetKey(KeyCode.D)) horizontal += 1f;
        if (Input.GetKey(KeyCode.A)) horizontal -= 1f;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(transform.up * jumpPower);
        }

        Vector3 moveDir = new Vector3(horizontal, 0, vertical).normalized;

        bool isMoving = moveDir.magnitude > 0f;

        // アニメーション設定
        animator.SetBool("Run", isMoving);

        if (isMoving)
        {
            // 回転（進行方向を向く）
            transform.rotation = Quaternion.LookRotation(moveDir);

            // Rigidbodyで移動
            Vector3 newPos = _rb.position + moveDir * moveSpeed * Time.deltaTime;
            _rb.MovePosition(newPos);
        }
    }
}

