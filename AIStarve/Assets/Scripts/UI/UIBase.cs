using UnityEngine;

/// <summary>
/// UI面板基类，所有UI面板需要继承此类
/// </summary>
public abstract class UIBase : MonoBehaviour
{
    /// <summary>
    /// 面板关联的GameObject引用
    /// </summary>
    public new GameObject gameObject { get; protected set; }

    /// <summary>
    /// 面板名称标识
    /// </summary>
    public string PanelName { get; internal set; }

    protected virtual void Awake()
    {
        gameObject = base.gameObject;
    }

    /// <summary>
    /// 面板创建时调用，用于初始化
    /// </summary>
    public virtual void OnCreate()
    {
        // 初始化逻辑
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 面板显示时调用
    /// </summary>
    public virtual void OnShow()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 面板隐藏时调用
    /// </summary>
    public virtual void OnHide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 面板销毁时调用，用于清理资源
    /// </summary>
    public virtual void OnDestroy()
    {
        // 清理逻辑
    }

    /// <summary>
    /// 接收来自其他面板或系统的消息
    /// </summary>
    /// <param name="message">消息内容</param>
    public virtual void ReceiveMessage(object message)
    {
        // 消息处理逻辑
    }
}