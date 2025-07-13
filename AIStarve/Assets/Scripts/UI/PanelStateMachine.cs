using UnityEngine;
using System.Collections.Generic;

public class PanelStateMachine : MonoBehaviour
{
    public enum PanelState
    {
        None,
        MainMenu,
        GamePlay,
        SceneObjectSelect
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
    }

    public void ChangeState(PanelState newState)
    {
        if (_currentState == newState) return;

        // 退出当前状态
        switch (_currentState)
        {
            case PanelState.SceneObjectSelect:
                UIManager.Instance.CloseCurrentPanel();
                break;
        }

        _currentState = newState;

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

    public void OnButtonClick(string panelToOpen)
    {
        if (panelToOpen == nameof(SceneObjectSelect))
        {
            ChangeState(PanelState.SceneObjectSelect);
        }
    }
}