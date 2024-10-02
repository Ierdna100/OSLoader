using UnityEngine;
using UnityEngine.SceneManagement;
using OSLoader;

public class MyMod : Mod
{
    public static TestMod instance;

    public override void InitializeMod()
    {
        // Add listener on sceneLoaded, which will get called whenever a scene changes
        // A proper system to wait until the loading screen is done loading will be implemented later
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == OSScene.Interior_PlayerTenement)
        {
            Debug.Log("Player entered their tenement!");
        }
    }
}