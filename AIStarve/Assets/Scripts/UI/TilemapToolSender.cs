using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Tilemap工具类型发送器（拖动模式）
/// </summary>
public class TilemapToolSender : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private TilemapItemType _itemType = TilemapItemType.ObjectsTilemapItem;
    
    [SerializeField]
    private string _prefabPath = "Perfabs/TilePalette";

    private Vector2 _dragStartPos;
    private const float DragThreshold = 10f;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragStartPos = eventData.position;
        
        // 立即发送TilemapToolChanged事件
        var args = new UIEventType.EventArgs
        {
            EventType = UIEventType.Event.TilemapToolChanged,
            ItemType = _itemType,
            PrefabPath = _prefabPath,
            PointerData = eventData
        };
        UIManager.Instance.TriggerEvent(UIEventType.Event.TilemapToolChanged, args);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 转发拖动事件到UIManager
        var args = new UIEventType.EventArgs
        {
            EventType = UIEventType.Event.TilemapToolDragging,
            ItemType = _itemType,
            PrefabPath = _prefabPath,
            PointerData = eventData
        };
        UIManager.Instance.TriggerEvent(UIEventType.Event.TilemapToolDragging, args);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 发送TilemapToolChangedEnd事件
        var endArgs = new UIEventType.EventArgs
        {
            EventType = UIEventType.Event.TilemapToolChangedEnd,
            ItemType = _itemType,
            PrefabPath = _prefabPath
        };
        UIManager.Instance.TriggerEvent(UIEventType.Event.TilemapToolChangedEnd, endArgs);
    }

    private void Awake()
    {
        // 移除原有的点击事件处理
        var button = GetComponent<Button>();
        if (button != null)
        {
            Destroy(button);
        }
        
        // 确保有Collider用于接收拖动事件
        if (GetComponent<Collider2D>() == null && GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
    }
}