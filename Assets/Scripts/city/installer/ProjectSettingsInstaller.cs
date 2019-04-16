using UnityEngine;
using Zenject;

namespace game.city.installer
{
    // We prefer to use ScriptableObjectInstaller for installers that contain game settings
    // There's no reason why you couldn't use a MonoInstaller here instead, however
    // using ScriptableObjectInstaller has advantages here that make it nice for settings:
    //
    // 1) You can change these values at runtime and have those changes persist across play
    //    sessions.  If it was a MonoInstaller then any changes would be lost when you hit stop
    // 2) You can easily create multiple ScriptableObject instances of this installer to test
    //    different customizations to settings.  For example, you might have different instances
    //    for each difficulty mode of your game, such as "Easy", "Hard", etc.
    // 3) If your settings are associated with a game object composition root, then using
    //    ScriptableObjectInstaller can be easier since there will only ever be one definitive
    //    instance for each setting.  Otherwise, you'd have to change the settings for each game
    //    object composition root separately at runtime
    //
    // Uncomment if you want to add alternative game settings
  
    [CreateAssetMenu(menuName = "Game/Game Settings")]
    public class ProjectSettingsInstaller : ScriptableObjectInstaller<ProjectSettingsInstaller>
    {
        public ProjectContextInstaller.Settings ProjectContextInstaller;
        
        public override void InstallBindings()
        {
            // Use IfNotBound to allow overriding for eg. from play mode tests
            Container.BindInstance(ProjectContextInstaller).IfNotBound();
        }
    }
}

