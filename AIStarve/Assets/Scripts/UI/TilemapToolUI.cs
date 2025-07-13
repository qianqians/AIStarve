using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tilemap工具UI控制器
/// </summary>
[RequireComponent(typeof(Canvas))]
public class TilemapToolUI : MonoBehaviour
{
    private Button _objectToolButton;
    private Button _groundToolButton;
    
    private void Awake()
    {
        // 创建工具按钮布局
        CreateToolButtons();
        
        // 默认选择对象工具
        TilemapItemHandler.Instance.SetItemType(TilemapItemType.ObjectsTilemapItem);
    }
    
    private void CreateToolButtons()
    {
        // 创建画布根对象
        var toolRoot = new GameObject("TilemapTools");
        toolRoot.transform.SetParent(transform, false);
        toolRoot.AddComponent<CanvasRenderer>();
        
        // 创建对象工具按钮
        _objectToolButton = CreateUIButton("Objects Tool", new Vector2(100, 50), new Vector2(20, -20));
        _objectToolButton.onClick.AddListener(() => 
        {
            TilemapItemHandler.Instance.SetItemType(TilemapItemType.ObjectsTilemapItem);
            UpdateButtonStates();
        });
        
        // 创建地面工具按钮
        _groundToolButton = CreateUIButton("Ground Tool", new Vector2(100, 50), new Vector2(20, -80));
        _groundToolButton.onClick.AddListener(() => 
        {
            TilemapItemHandler.Instance.SetItemType(TilemapItemType.GroundTilemapItem);
            UpdateButtonStates();
        });
        
        // 初始化按钮状态
        UpdateButtonStates();
    }
    
    private Button CreateUIButton(string buttonText, Vector2 size, Vector2 anchoredPosition)
    {
        // 创建按钮对象
        var buttonObj = new GameObject(buttonText);
        buttonObj.transform.SetParent(transform, false);
        
        // 添加矩形变换
        var rect = buttonObj.AddComponent<RectTransform>();
        rect.sizeDelta = size;
        rect.anchoredPosition = anchoredPosition;
        
        // 添加按钮组件
        var button = buttonObj.AddComponent<Button>();
        
        // 添加图像组件（自动添加）
        var image = buttonObj.AddComponent<Image>();
        image.color = new Color(0.8f, 0.8f, 0.8f);
        
        // 创建文本
        var textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform, false);
        
        var textRect = textObj.AddComponent<RectTransform>();
        textRect.sizeDelta = size;
        textRect.anchoredPosition = Vector2.zero;
        
        var text = textObj.AddComponent<Text>();
        text.text = buttonText;
        text.fontSize = 16;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.black;
        
        return button;
    }
    
    private void UpdateButtonStates()
    {
        if (_objectToolButton != null && _groundToolButton != null)
        {
            bool isObjectMode = TilemapItemHandler.Instance.CurrentItemType == TilemapItemType.ObjectsTilemapItem;
            _objectToolButton.interactable = !isObjectMode;
            _groundToolButton.interactable = isObjectMode;
        }
    }
}