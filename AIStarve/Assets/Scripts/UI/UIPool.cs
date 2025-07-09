using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI面板对象池，管理UI实例的复用
/// </summary>
public class UIPool
{
    private Queue<UIBase> _pool = new();
    private string _panelName;
    private string _prefabPath;
    private int _maxSize;
    private Transform _poolRoot;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="panelName">面板名称</param>
    /// <param name="prefabPath">预制体资源路径</param>
    /// <param name="maxSize">对象池最大容量</param>
    public UIPool(string panelName, string prefabPath, int maxSize)
    {
        _panelName = panelName;
        _prefabPath = prefabPath;
        _maxSize = maxSize;
        _poolRoot = new GameObject($"[Pool]{panelName}").transform;
        _poolRoot.SetParent(UIManager.Instance.transform);
        WarmUp(1);
    }

    /// <summary>
    /// 从对象池获取UI实例
    /// </summary>
    /// <returns>可用的UI实例</returns>
    private Canvas _canvas;
    
    public UIBase Get()
    {
        if (_pool.Count == 0)
        {
            WarmUp(1);
        }

        var instance = _pool.Dequeue();
        if (_canvas == null)
        {
            _canvas = Object.FindObjectOfType<Canvas>();
            if (_canvas == null)
            {
                Debug.LogError("Cannot find Canvas in scene!");
                return null;
            }
        }
        
        instance.gameObject.SetActive(true);
        instance.transform.SetParent(_canvas.transform);
        instance.OnCreate();
        return instance;
    }

    /// <summary>
    /// 释放UI实例回对象池
    /// </summary>
    /// <param name="instance">要释放的UI实例</param>
    public void Release(UIBase instance)
    {
        if (_pool.Count < _maxSize)
        {
            instance.gameObject.SetActive(false);
            instance.gameObject.transform.SetParent(_poolRoot);
            instance.OnHide();
            _pool.Enqueue(instance);
        }
        else
        {
            Object.Destroy(instance.gameObject);
        }
    }

    /// <summary>
    /// 预热对象池，预先创建指定数量的实例
    /// </summary>
    /// <param name="count">要预热的实例数量</param>
    public void WarmUp(int count)
    {
        var canvas = Object.FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Cannot find Canvas in scene!");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            var prefab = Resources.Load<GameObject>(_prefabPath);
            var go = Object.Instantiate(prefab, canvas.transform);
            var ui = go.GetComponent<UIBase>();
            ui.PanelName = _panelName;
            go.transform.SetParent(_poolRoot);
            go.SetActive(false);
            _pool.Enqueue(ui);
        }
    }
}
