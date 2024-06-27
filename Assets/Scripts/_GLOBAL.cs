using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _GLOBAL : MonoBehaviour
{
    static public void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }
}
