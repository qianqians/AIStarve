using System;
using UnityEngine.EventSystems;

/// <summary>
/// UI事件类型定义
/// </summary>
public static class UIEventType
{
    /// <summary>
    /// UI事件枚举
    /// </summary>
    public enum Event
    {
        /// <summary> 面板打开 </summary>
        PanelOpen,
        /// <summary> 面板关闭 </summary>
        PanelClose,
        /// <summary> 按钮点击 </summary>
        ButtonClick,
        /// <summary> 窗口大小变化 </summary>
        WindowResize,
        /// <summary> Tilemap工具变化 </summary>
        TilemapToolChanged,
        /// <summary> 数据加载完成 </summary>
        DataLoaded,
        /// <summary> 网络请求开始 </summary>
        NetworkRequestStart,
        /// <summary> 网络请求完成 </summary>
        NetworkRequestComplete,
        /// <summary> 游戏状态变化 </summary>
        GameStateChange,
        /// <summary> Tilemap工具变化结束 </summary>
        TilemapToolChangedEnd,
        /// <summary> Tilemap工具拖动中 </summary>
        TilemapToolDragging
    }

    /// <summary>
    /// UI事件参数基类
    /// </summary>
    public class EventArgs : System.EventArgs
    {
        public Event EventType { get; set; }
        public string PanelName { get; set; }
        public string PanelToOpen { get; set; } // 新增：指定要打开的面板名称
        public object Data { get; set; }
        
        /// <summary>
        /// Tilemap项类型（可选）
        /// </summary>
        public TilemapItemType ItemType { get; set; }
        
        /// <summary>
        /// 对应的Prefab路径（用于对象类型操作）
        /// </summary>
        public string PrefabPath { get; set; }
        
        /// <summary>
        /// 指针事件数据（用于拖动事件）
        /// </summary>
        public PointerEventData PointerData { get; set; }
    }
}