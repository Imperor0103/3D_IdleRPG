using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    // CharacterController에서 땅에 붙었는지 아닌지를 판단하는 변수를 가지고 있다
    [SerializeField] private CharacterController controller;

    [SerializeField] private float drag = 0.3f; 
    private float verticalVelocity; // y속도

    // 수평이동: impact 방향
    // y방향: Vector3.up
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;   // 수평, y방향 모두 고려
    private Vector3 dampingVelocity;
    private Vector3 impact; // 수평 으로 나가는 힘


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

        // drag 시간동안 처음위치에서 나중위치로 천천히 이동한다
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    /// <summary>
    /// 남아 있는 값들이 impact에 영향을 주지 않게 0초기화
    /// </summary>
    public void Reset()
    {
        verticalVelocity = 0;
        impact = Vector3.zero;
    }



    // 수평에서 이동할때만 호출한다
    public void AddForce(Vector3 force)
    {
        impact += force;
    }


    public void Jump(float jumpForce)
    {
        // 기존의 점프가속도 힘에 추가
        verticalVelocity += jumpForce;
    }
}