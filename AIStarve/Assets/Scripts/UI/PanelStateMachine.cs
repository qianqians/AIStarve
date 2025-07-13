using UnityEngine;
using System.Collections.Generic;

public class PanelStateMachine : MonoBehaviour
{
    public enum PanelState
    {
        None,
        MainMenu,
        GamePlay,
        SceneObjectSelect,
        ObjectSelect,
    }

    private static PanelStateMachine _instance;
    public static PanelStateMachine Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("PanelStateMachine instance is null!");
            }
            return _instance;
        }
    }

    private Dictionary<PanelState, System.Action> _stateHandlers = new();
    private PanelState _currentState = PanelState.None;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("Duplicate PanelStateMachine detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("PanelStateMachine initialized successfully.");
        // 初始化状态处理器
        _stateHandlers[PanelState.SceneObjectSelect] = HandleSceneObjectSelect;
        _stateHandlers[PanelState.ObjectSelect] = HandleObjectSelect;
    }

    public void ChangeState(PanelState newState)
    {
        if (_currentState == newState) return;

        // 退出当前状态
        switch (_currentState)
        {
            case PanelState.SceneObjectSelect:
               // UIManager.Instance.CloseCurrentPanel();//不需要关闭窗口
                break;
        }

        _currentState = newState;
        
        // 进入新状态
        switch (newState)
        {
            case PanelState.SceneObjectSelect:
                UIManager.Instance.ShowObjectSelection();
                break;
        }

        // 进入新状态
        if (_stateHandlers.TryGetValue(newState, out var handler))
        {
            handler?.Invoke();
        }
    }

    private void HandleSceneObjectSelect()
    {
        if (!UIManager.Instance.GetActivePanel(nameof(SceneObjectSelect)))
        {
            UIManager.Instance.RegisterPanel(
                nameof(SceneObjectSelect),
                SceneObjectSelect.PrefabPath,
                1
            );
            UIManager.Instance.ShowPanel<SceneObjectSelect>();
        }
    }
    private void HandleObjectSelect()
    {
        ChangeState(PanelState.ObjectSelect);
        UIManager.Instance.ShowObjectSelection();
    }
    public void OnButtonClick(string panelToOpen)
    {
        if (panelToOpen == nameof(SceneObjectSelect))
        {
            // 如果当前已经是SceneObjectSelect状态则关闭面板
            if (_currentState == PanelState.SceneObjectSelect)
            {
                ChangeState(PanelState.None);
                Debug.Log("Closing SceneObjectSelect panel");
            }
            else
            {
                ChangeState(PanelState.SceneObjectSelect);
                Debug.Log("Opening SceneObjectSelect panel");
            }
        }else if(panelToOpen == nameof(PanelState.ObjectSelect))
        {

        }
    }
}