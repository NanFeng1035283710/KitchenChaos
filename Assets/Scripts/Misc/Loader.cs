using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 加载静态例  
/// 注明:静态类的所有成员都必须是静态成员
/// </summary>
public static class Loader
{
    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadScene,
    }

    /// <summary>
    /// 目标场景序号
    /// </summary>
    private static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadScene.ToString());
    }
    public static void LoaderCallback()
    {
        SceneManager.LoadScene(Scene.GameScene.ToString());
    }
}
