using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI管理系统主控制器，负责UI面板的注册、显示、隐藏和消息传递
/// </summary>
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    private Dictionary<string, UIPool> _panelPools = new();
    private Dictionary<string, UIBase> _activePanels = new();
    private Stack<UIBase> _panelStack = new();

    // 事件处理器字典
    private Dictionary<UIEventType.Event, Action<UIEventType.EventArgs>> _eventHandlers =
        new Dictionary<UIEventType.Event, Action<UIEventType.EventArgs>>();

    /// <summary>
    /// 注册UI事件处理器
    /// </summary>
    public void RegisterEvent(UIEventType.Event eventType, Action<UIEventType.EventArgs> handler)
    {
        if (_eventHandlers.ContainsKey(eventType))
        {
            _eventHandlers[eventType] += handler;
        }
        else
        {
            _eventHandlers[eventType] = handler;
        }
    }

    /// <summary>
    /// 取消注册UI事件处理器
    /// </summary>
    public void UnregisterEvent(UIEventType.Event eventType, Action<UIEventType.EventArgs> handler)
    {
        if (_eventHandlers.ContainsKey(eventType))
        {
            _eventHandlers[eventType] -= handler;
        }
    }

    /// <summary>
    /// 触发UI事件
    /// </summary>
    public void TriggerEvent(UIEventType.Event eventType, UIEventType.EventArgs args = null)
    {
        if (args == null)
        {
            args = new UIEventType.EventArgs();
        }
        args.EventType = eventType;

        // 处理按钮点击事件时检查是否需要打开新面板
        if (eventType == UIEventType.Event.ButtonClick && !string.IsNullOrEmpty(args.PanelToOpen))
        {
            if (args.PanelToOpen == nameof(SceneObjectSelect))
            {
                // 添加空值保护
                if (PanelStateMachine.Instance != null)
                {
                    PanelStateMachine.Instance.OnButtonClick(args.PanelToOpen);
                }
                else
                {
                    Debug.LogError("PanelStateMachine instance is null!");
                }
            }
            else
            {
                ShowPanel(args.PanelToOpen);
            }
        }

        if (_eventHandlers.TryGetValue(eventType, out var handler))
        {
            handler?.Invoke(args);
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 注册UI面板
    /// </summary>
    /// <param name="panelName">面板唯一标识名</param>
    /// <param name="prefabPath">预制体资源路径</param>
    /// <param name="poolSize">对象池初始大小</param>
    public void RegisterPanel(string panelName, string prefabPath, int poolSize = 1)
    {
        if (!_panelPools.ContainsKey(panelName))
        {
            _panelPools[panelName] = new UIPool(panelName, prefabPath, poolSize);
        }
    }

    /// <summary>
    /// 从对象池获取面板实例
    /// </summary>
    /// <param name="panelName">要获取的面板名称</param>
    /// <returns>面板实例，如果未注册返回null</returns>
    public UIBase GetPanel(string panelName)
    {
        if (_panelPools.TryGetValue(panelName, out var pool))
        {
            return pool.Get();
        }
        return null;
    }

    /// <summary>
    /// 获取已激活的面板实例
    /// </summary>
    /// <param name="panelName">面板名称</param>
    /// <returns>已激活的面板实例，如果未注册或没有激活实例返回null</returns>
    public UIBase GetActivePanel(string panelName)
    {
        if (_panelPools.TryGetValue(panelName, out var pool))
        {
            return pool.GetFromActive();
        }
        return null;
    }

    /// <summary>
    /// 释放面板实例回对象池
    /// </summary>
    /// <param name="panelName">面板名称</param>
    /// <param name="panel">要释放的面板实例</param>
    public void ReleasePanel(string panelName, UIBase panel)
    {
        if (_panelPools.TryGetValue(panelName, out var pool))
        {
            pool.Release(panel);
        }
    }

    /// <summary>
    /// 显示指定面板
    /// </summary>
    /// <param name="panelName">要显示的面板名称</param>
    public void ShowPanel(string panelName)
    {
        var panel = GetPanel(panelName);
        if (panel != null)
        {
            if (_panelStack.Count > 0)
            {
                _panelStack.Peek().OnHide();
                TriggerEvent(UIEventType.Event.PanelClose,
                    new UIEventType.EventArgs { PanelName = _panelStack.Peek().PanelName });
            }
            
            _panelStack.Push(panel);
            _activePanels[panelName] = panel;
            panel.OnShow();
            TriggerEvent(UIEventType.Event.PanelOpen,
                new UIEventType.EventArgs { PanelName = panelName });
        }
    }

    /// <summary>
    /// 通过类型显示面板
    /// </summary>
    /// <typeparam name="T">面板类型</typeparam>
    public void ShowPanel<T>() where T : UIBase
    {
        ShowPanel(typeof(T).Name);
    }

    /// <summary>
    /// 关闭当前显示的面板
    /// </summary>
    public void CloseCurrentPanel()
    {
        if (_panelStack.Count > 0)
        {
            var panel = _panelStack.Pop();
            panel.OnHide();
            TriggerEvent(UIEventType.Event.PanelClose,
                new UIEventType.EventArgs { PanelName = panel.PanelName });
            ReleasePanel(panel.PanelName, panel);
            _activePanels.Remove(panel.PanelName);

            if (_panelStack.Count > 0)
            {
                _panelStack.Peek().OnShow();
            }
        }
    }
    /// <summary>
    /// 获取指定面板的RectTransform
    /// </summary>
    /// <param name="panelName">面板名称</param>
    /// <returns>面板的RectTransform，如果不存在返回null</returns>
    public RectTransform GetPanelTransform(string panelName)
    {
        if (_activePanels.TryGetValue(panelName, out var panel))
        {
            return panel.GetComponent<RectTransform>();
        }
        return null;
    }
}

