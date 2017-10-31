using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader {

    static Dictionary<string, object> sceneArgsDic = new Dictionary<string, object>();

    static public void LoadScene(string sceneName, LoadSceneMode mode, object args)
    {
        // 読み込むシーンと引数をキャッシュ
        sceneArgsDic.Add(sceneName, args);

        // シーン読み込み, イベント追加
        SceneManager.LoadScene(sceneName, mode);
        SceneManager.sceneLoaded += OnLoadedScene;
    }

    static void OnLoadedScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        // キャッシュから引数取り出し
        var args = sceneArgsDic[scene.name];
        sceneArgsDic.Remove(scene.name);

        // GameObject取得
        var loadedSceneObj = scene.GetRootGameObjects().FirstOrDefault();

        // 引数渡す、初期化
        var sceneObj = loadedSceneObj.GetComponent<SceneBase>();
        sceneObj.Args = args;
    }
}
