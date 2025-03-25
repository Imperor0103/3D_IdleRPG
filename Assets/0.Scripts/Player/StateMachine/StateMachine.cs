using Unity.VisualScripting;


public interface IState
{
    public void Enter();    // ���� ����
    public void Exit();     // ���� ��
    public void HandleInput();  // ���ο� �Է°��� ������ �̺�Ʈ�� �߰�, ����
    public void Update();   // ���� ������Ʈ
    public void PhysicsUpdate();    // ���� ������Ʈ 
}

// ���¸ӽ�
public abstract class StateMachine 
{
    protected IState currentState;

    // �Լ����� ���� ������ ���ϰ� base�� �ִ� ���� �ᵵ ����ϴ�
    public void ChangeState(IState state)
    {
        currentState?.Exit();   /// ���� ó������ ���� State�� IdleState
        currentState = state;
        currentState?.Enter();
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    // �����ֱ��Լ� �ƴϴ�
    public void Update()
    {
        currentState?.Update();
    }

    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
}