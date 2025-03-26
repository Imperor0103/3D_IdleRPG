using System.Resources;
using System.Runtime.InteropServices;
using UnityEditor.EditorTools;
using UnityEngine;

public interface IManager
{
    void Init();
    void Clear();
}

public class Managers : Singleton<Managers>
{
    // 매니저를 들고있는다
    [field: SerializeField] private UIManager ui = new UIManager();

}
