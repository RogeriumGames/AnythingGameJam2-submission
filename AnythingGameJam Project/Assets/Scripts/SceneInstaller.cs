using UnityEditor;
using UnityEngine;
using Zenject;
public class PlayerInstaller : MonoInstaller
{

    [Tooltip("Tag do GameObject que representa o corpo do Player.")]
    public string playerBodyTag = "PlayerBody"; 
    [Tooltip("Tag do GameObject que representa a Câmera Principal.")]
    public string mainCameraTag = "MainCamera"; 

    public override void InstallBindings()
    {
        GameObject playerBodyGameObject = GameObject.FindWithTag(playerBodyTag);

        Container.Bind<Transform>()
                 .WithId("PlayerBody")
                 .FromInstance(playerBodyGameObject.transform); 

        GameObject mainCameraGameObject = GameObject.FindWithTag(mainCameraTag);

        Container.Bind<Transform>()
                 .WithId("MainCamera")
                 .FromInstance(mainCameraGameObject.transform);

        Debug.Log($"PlayerInstaller: configurou '{playerBodyTag}' e '{mainCameraTag}'");

    }
}