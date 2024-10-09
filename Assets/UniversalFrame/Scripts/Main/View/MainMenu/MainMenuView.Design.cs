using Framework.Extension;
using UnityEngine.UI;
using UnityEngine;


namespace Assets.UniversalFrame.Scripts.Main.View.MainMenu
{
   public partial class MainMenuView 
   {
        public Button My_Btn;

        public override void InitComponents()
        {
            My_Btn = ViewObj.FindChild<Button>("Bg/My_Btn");
        }

   }
}
