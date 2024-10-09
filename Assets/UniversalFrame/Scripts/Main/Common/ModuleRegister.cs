
using Assets.Scripts.Runtime.Common;
using Assets.UniversalFrame.Scripts.Main.View.MainMenu;
using Framework.Core;
using Framework.Managers;

public class ModuleRegister
{
    public static void Init()
    {
        var viewmanager = new ViewManager();
        viewmanager.InitCanvas(UIRoot.Inst.Canvas.gameObject, 0);
        Facade.Inst.Bind(viewmanager);
        Facade.Inst.Bind(new TemplateManager());
        Facade.Inst.Register<MainMenuControl>();
    }
}
