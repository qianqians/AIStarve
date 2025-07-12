using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneObjectSelect : UIBase
{
    public const string PrefabPath = "Perfabs/SceneObjectSelect";
    private RectTransform _rectTransform = null;

    public override void Start()
    {
        base.Start();
        var panel = UIManager.Instance.GetActivePanel("SceneObjectSelect");
        _rectTransform = panel?.GetComponent<RectTransform>();
        
        OnWindowSizeChanged();
    }

    private float _lastUpdateTime;
    private Vector2 _lastWindowSize;

    private void UpdatePanelPosition()
    {
        if (Time.time - _lastUpdateTime < 0.5f)
        {
            return;
        }

        Vector2 currentWindowSize = new Vector2(Screen.width, Screen.height);
        if (currentWindowSize == _lastWindowSize)
        {
            return;
        }

        if (_rectTransform != null)
        {
            float windowWidth = currentWindowSize.x;
            float windowHeight = currentWindowSize.y;
            
            // 设置面板在右上角
            _rectTransform.offsetMin = new Vector2(windowWidth/2 + 50, 50); // left, bottom
            _rectTransform.offsetMax = new Vector2(-50, -50); // right, top

            _lastWindowSize = currentWindowSize;
            _lastUpdateTime = Time.time;
        }
    }

    private Coroutine _resizeCoroutine;
    
    protected override void OnWindowSizeChanged()
    {
        if (_resizeCoroutine != null)
        {
            StopCoroutine(_resizeCoroutine);
        }
        
        _resizeCoroutine = StartCoroutine(DelayedUpdatePosition());
    }
    
    private IEnumerator DelayedUpdatePosition()
    {
        yield return new WaitForSeconds(0.5f);
        UpdatePanelPosition();
        _resizeCoroutine = null;
    }
}