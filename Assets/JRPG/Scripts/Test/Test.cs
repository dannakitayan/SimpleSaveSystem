using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Node
{
    public override void LoadAllContent()
    {
        base.LoadAllContent();
        LoadState<Vector3>("position", (value) => gameObject.transform.position = value);
        LoadState<bool>("active", (value) => gameObject.SetActive(value));
    }

    public override void SaveAllContent()
    {
        base.SaveAllContent();
        SaveState("position", gameObject.transform.position);
    }
}
