using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSceneRoute : Button
{
    public int sceneId;
    public void MoveToScene()
    {
        _GLOBAL.LoadScene(sceneId);
    }
}
