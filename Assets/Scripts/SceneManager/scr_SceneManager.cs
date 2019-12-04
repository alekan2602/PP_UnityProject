using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_SceneManager : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void ChangeSceneTo(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}
