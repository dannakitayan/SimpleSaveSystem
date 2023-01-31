using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Node
{
    public override void LoadAllContent()
    {
        base.LoadAllContent();
        LoadState("position", (parameter) => gameObject.transform.position = (Vector3)parameter);
        LoadState("active", (parameter) => gameObject.SetActive((bool)parameter));
    }

    public override void SaveAllContent()
    {
        base.SaveAllContent();
        SaveState("position", gameObject.transform.position);
    }
}
