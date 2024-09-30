using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OSLoader;
using System.IO;
using System.Linq;
using System.Reflection;

public class AssetBundleDebugger : MonoBehaviour
{
    public MenuPrefabs menuPrefabs;

    [ContextMenu("WOOOOOOO")]
    private void LoadAssetBundle()
    {
        var assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "loader"));
        menuPrefabs = assetBundle.LoadAllAssets<MenuPrefabs>().SingleOrDefault();

        Instantiate(menuPrefabs.modEntry);
    }
}
