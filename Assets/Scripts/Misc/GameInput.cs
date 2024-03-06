using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 游戏输入
/// </summary>
public class GameInput : Singleton<GameInput>
{
    /// <summary>
    /// 交互事件
    /// </summary>
    public event EventHandler OnInteractAction;
    /// <summary>
    /// 特殊交互事件
    /// </summary>
    public event EventHandler OnInteractAlternateAction;
    /// <summary>
    /// 暂停事件
    /// </summary>
    public event EventHandler OnPauseAction;

    private const string PLAYER_PREFS_BINDINGS = "PlayerPrefsBindings";
    public enum Binding
    {
        Move_Up, Move_Down, Move_Left, Move_Right,
    }
    private PlayerInputActions playerInputActions;
    new private void Awake()
    {
        base.Awake();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed; ;
        playerInputActions.Player.Pause.performed += Pause_performed;

        //如果保存了快捷键
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            //设置快捷键
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));

        }
    }
  new  private void OnDestroy()
    {
        base.OnDestroy();
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed; ;
        playerInputActions.Player.Pause.performed -= Pause_performed;
        playerInputActions.Dispose();
    }
    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    internal Vector2 GetMovementVectorNoramlized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
    public string GetBinbingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
        }

    }

    /// <summary>
    /// 重定向绑定按键
    /// </summary>
    public void RebinBinding(Binding binding, Action action)
    {
        playerInputActions.Player.Disable();
        int binDingIndex=0;
        switch (binding)
        {
            case Binding.Move_Up:
                binDingIndex = 1;
                break;
            case Binding.Move_Down:
                binDingIndex = 2;
                break;
            case Binding.Move_Left:
                binDingIndex = 3;
                break;
            case Binding.Move_Right:
                binDingIndex = 4;
                break;
            default:
                break;
        }
        playerInputActions.Player.Move.PerformInteractiveRebinding(binDingIndex).OnComplete(
      callback =>
      {
          callback.Dispose();
          action.Invoke();
          PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
          PlayerPrefs.Save();
          playerInputActions.Player.Enable();
      }
      ).Start();
    }

}
