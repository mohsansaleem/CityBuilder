using RSG;
using UnityEngine.SceneManagement;

namespace game.core
{
    public interface ISceneLoader
    {
        IPromise LoadScene(string sceneName);
        IPromise UnloadScene(string sceneName);
        IPromise UnloadScene(Scene sceneName);
    }
}
