using System;
using UnityEngine;

public enum NodeState
{
    Static,
    Dynamic
}

public class Node : MonoBehaviour
{
    public NodeState State;

    public void SaveState(string parameter, object value)
    {
        Game.AddContent($"{gameObject.name}.{parameter}", value);
    }

    public void LoadState(string name, Action<object> callback)
    {
        var parameter = Game.GetContent($"{gameObject.name}.{name}");
        if (parameter != null) callback.Invoke(parameter);
    }

    public virtual void LoadAllContent()
    {
        if (State == NodeState.Static) return;
    }

    public virtual void SaveAllContent()
    {
        if (State == NodeState.Static) return;
    }

    void Awake()
    {
        Game.onLoadAllContent += LoadAllContent;
        Game.onSaveAllContent += SaveAllContent;
    }

    void OnDestroy()
    {
        Game.onLoadAllContent -= LoadAllContent;
        Game.onSaveAllContent -= SaveAllContent;
    }

    private void OnDisable()
    {
        if (State == NodeState.Static) return;
        SaveState("active", gameObject.activeInHierarchy);
    }
}
