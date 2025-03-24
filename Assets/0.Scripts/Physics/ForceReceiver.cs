using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    // CharacterController���� ���� �پ����� �ƴ����� �Ǵ��ϴ� ������ ������ �ִ�
    [SerializeField] private CharacterController controller;

    private float verticalVelocity; // y�ӵ�

    public Vector3 Movement => Vector3.up * verticalVelocity;   // y���� ����

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            // ���� ��Ҵٸ�
            // y�ӵ��� �߷��� ���⸸ �޴´�
            // Physics.gravity.y�� �Ϲ������� -9.81f
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            // ���鿡�� ��������
            // �߷��� ��� �ۿ��ϹǷ�, y�ӵ��� ���� �� ����(���� �ӵ� ����)
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }

    public void Jump(float jumpForce)
    {
        // ������ �������ӵ� ���� �߰�
        verticalVelocity += jumpForce;
    }
}