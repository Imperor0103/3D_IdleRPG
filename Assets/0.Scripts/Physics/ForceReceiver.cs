using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    // CharacterController에서 땅에 붙었는지 아닌지를 판단하는 변수를 가지고 있다
    [SerializeField] private CharacterController controller;

    private float verticalVelocity; // y속도

    public Vector3 Movement => Vector3.up * verticalVelocity;   // y방향 결정

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            // 땅에 닿았다면
            // y속도가 중력의 영향만 받는다
            // Physics.gravity.y는 일반적으로 -9.81f
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            // 지면에서 떨어지면
            // 중력은 계속 작용하므로, y속도는 점점 더 감소(낙하 속도 증가)
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }

    public void Jump(float jumpForce)
    {
        // 기존의 점프가속도 힘에 추가
        verticalVelocity += jumpForce;
    }
}