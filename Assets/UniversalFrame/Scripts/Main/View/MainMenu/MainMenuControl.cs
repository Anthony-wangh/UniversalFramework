using Framework.Core;
using Framework.Managers;


namespace Assets.UniversalFrame.Scripts.Main.View.MainMenu
{
   public class MainMenuControl : ControlBase
   {
        private ViewManager _manager;
        protected override void OnInit()
        {
            _manager = GetManager<ViewManager>();
        }

        protected override void OnSubscribe()
        {
            Subscribe<MainMenuViewOpenMessage>(OnEventTrigger);
        }

        private void OnEventTrigger(MainMenuViewOpenMessage eventMessage)
        {
            if(eventMessage.IsOpen)
               _manager.OpenView<MainMenuView>();
            else
               _manager.CloseView<MainMenuView>();
        }

       
   }
}
