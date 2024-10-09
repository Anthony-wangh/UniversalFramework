using Assets.UniversalFrame.Scripts.Main.View.MainMenu;
using DevFramework.Scripts.Common;
using Framework.Common;
using Framework.Core;
using Framework.Core.Message;
using UnityEngine;
/// <summary>
/// Ö÷Èë¿Ú
/// </summary>
public class Main
{
    [RuntimeInitializeOnLoadMethod]
    static void Start()
    {
        //AppConsole.Inst.Init();
        Facade.Inst.Init();
        ResourceManager.Init();
        AssetsBundleManager.Init();
        ModuleRegister.Init();
        Facade.Inst.GetManager<MessageManager>().Publish(new MainMenuViewOpenMessage() { IsOpen = true });
        //Subscribe<AppStartMessage>(OnGlobalEvent);
    }

    private void OnGlobalEvent(AppStartMessage obj)
    {
        if (obj.AppStart)
            Facade.Inst.GetManager<MessageManager>().Publish(new MainMenuViewOpenMessage() { IsOpen = true });
    }
}

public class AppStartMessage: IMessageBase
{
    public bool AppStart;
}