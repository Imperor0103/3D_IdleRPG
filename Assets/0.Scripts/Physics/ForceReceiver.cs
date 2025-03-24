using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    // CharacterController���� ���� �پ����� �ƴ����� �Ǵ��ϴ� ������ ������ �ִ�
    [SerializeField] private CharacterController controller;

    [SerializeField] private float drag = 0.3f; 
    private float verticalVelocity; // y�ӵ�

    // �����̵�: impact ����
    // y����: Vector3.up
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;   // ����, y���� ��� ���
    private Vector3 dampingVelocity;
    private Vector3 impact; // ���� ���� ������ ��


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

        // drag �ð����� ó����ġ���� ������ġ�� õõ�� �̵��Ѵ�
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    /// <summary>
    /// ���� �ִ� ������ impact�� ������ ���� �ʰ� 0�ʱ�ȭ
    /// </summary>
    public void Reset()
    {
        verticalVelocity = 0;
        impact = Vector3.zero;
    }



    // ���򿡼� �̵��Ҷ��� ȣ���Ѵ�
    public void AddForce(Vector3 force)
    {
        impact += force;
    }


    public void Jump(float jumpForce)
    {
        // ������ �������ӵ� ���� �߰�
        verticalVelocity += jumpForce;
    }
}