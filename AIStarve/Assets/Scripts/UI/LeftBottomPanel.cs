using UnityEngine;
using UnityEngine.UI;

public class LeftBottomPanel : UIBase
{
    public const string PrefabPath = "Perfabs/Panel"; // 直接放在Resources根目录
    private RectTransform _rectTransform;

    public override void Start()
    {
        base.Start();
        //_rectTransform = UIManager.Instance.GetPanelTransform(nameof(LeftBottomPanel));
        OnWindowSizeChanged();

    }
    public override void OnCreate()
    {
        base.OnCreate();
        
    }

    private void UpdatePanelPosition()
    {
        if (_rectTransform != null)
        {
            // 获取面板尺寸
            var panelSize = _rectTransform.sizeDelta;
            
            // 获取窗口尺寸
            float windowWidth = Screen.width;
            float windowHeight = Screen.height;
            
            // 计算左下角位置 (Y坐标为窗口高度减去面板高度)
            float posX = 0;
            float posY = windowHeight - 50;
            
            // 设置位置和锚点
            _rectTransform.anchorMin = new Vector2(0, 0);
            _rectTransform.anchorMax = new Vector2(0, 0);
            _rectTransform.anchoredPosition = new Vector2(posX, posY);
        }
    }
    protected override void OnWindowSizeChanged()
    {
        //UpdatePanelPosition();
    }

}