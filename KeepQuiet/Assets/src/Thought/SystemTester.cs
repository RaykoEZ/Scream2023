using System.Collections.Generic;
using UnityEngine;

public class SystemTester : MonoBehaviour 
{
    [SerializeField] protected List<TestObject> m_tests = default;
    protected virtual void OnEnable()
    {
        foreach (var item in m_tests)
        {
            item?.Init();
        }
    }
    protected virtual void OnDisable()
    {
        foreach (var item in m_tests)
        {
            item?.Shutdown();
        }
    }
}
