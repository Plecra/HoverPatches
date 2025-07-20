using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssetBundleLoader : MonoBehaviour
{
    private AssetBundle loadedFileBundle;
    private AssetBundle loadedBundle;
    public string sceneName;

    public void LoadFileFromBundle(string folder)
    {
        StartCoroutine(LoadFilesAsync(folder));
    }

    public void LoadSceneFromBundle(string folder)
    {
        StartCoroutine(LoadSceneAsync(folder));
    }

    private IEnumerator LoadFilesAsync(string folder)
    {
        string text = Directory.GetFiles(Path.Combine(Path.Combine(Application.streamingAssetsPath, "MapMod"), folder)).FirstOrDefault((string f) => f.Contains("files"));
        if (string.IsNullOrEmpty(text))
        {
            ChatManager.AddNewComandText("No AssetBundle found in folder: " + folder);
            yield break;
        }
        if (loadedFileBundle != null)
        {
            Debug.Log("Unloading previous AssetBundle...");
            loadedFileBundle.Unload(unloadAllLoadedObjects: true);
            Resources.UnloadUnusedAssets();
        }
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(text);
        yield return bundleLoadRequest;
        loadedFileBundle = bundleLoadRequest.assetBundle;
        if (loadedFileBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            yield break;
        }
        ChatManager.AddNewComandText("loaded asset bundle " + Path.GetFileName(text));
        LoadSceneFromBundle(folder.ToString());
    }

    public void UnloadFileBundle()
    {
        if (loadedFileBundle != null)
        {
            loadedFileBundle.Unload(unloadAllLoadedObjects: true);
            Resources.UnloadUnusedAssets();
            loadedFileBundle = null;
            Debug.Log("AssetBundle Unloaded.");
        }
    }

    public static string[] GetMapCandidates()
    {
        string map_folder = Path.Combine(Application.streamingAssetsPath, "MapMod");
        try
        {
            return Directory.GetDirectories(map_folder);
        }
        catch (DirectoryNotFoundException _)
        {
            Debug.LogError("Directory does not exist: " + map_folder);
            return new string[] { };
        }
    }
    // The scene is likely to use elements from the larger `files` bundle,
    // make sure its loaded first!
    public IEnumerator LoadSceneAsync(string folder)
    {
        string text = Directory.GetFiles(Path.Combine(Path.Combine(Application.streamingAssetsPath, "MapMod"), folder)).FirstOrDefault((string f) => f.Contains("scene"));
        if (string.IsNullOrEmpty(text))
        {
            Debug.LogError("No scene AssetBundle found in folder: " + folder);
            yield break;
        }
        if (loadedBundle != null)
        {
            Debug.Log("Unloading previous AssetBundle...");
            yield return SceneManager.UnloadSceneAsync(sceneName);
            loadedBundle.Unload(unloadAllLoadedObjects: true);
            yield return Resources.UnloadUnusedAssets();
        }
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(text);
        yield return bundleLoadRequest;

        loadedBundle = bundleLoadRequest.assetBundle;    
        if (loadedBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            yield break;
        }
        ChatManager.AddNewComandText("loaded scene " + Path.GetFileName(text));
        string[] allScenePaths = loadedBundle.GetAllScenePaths();
        if (allScenePaths.Length == 0)
        {
            Debug.LogError("No scenes found in AssetBundle!");
            yield break;
        }
        sceneName = Path.GetFileNameWithoutExtension(allScenePaths[0]);
        Debug.Log("Loading scene: " + sceneName);
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadBundle()
    {
        if (loadedBundle != null)
        {
            SceneManager.UnloadScene(sceneName);
            loadedBundle.Unload(unloadAllLoadedObjects: true);
            Resources.UnloadUnusedAssets();
            loadedBundle = null;
            Debug.Log("AssetBundle Unloaded.");
        }
    }
}
