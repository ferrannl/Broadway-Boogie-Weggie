using Broadway_Boogie_Weggie.ViewModels;
using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Commands
{
    public class PausePlayGalleryCommand : Command
    {
        public override void Execute(object parameter)
        {
            var main = ServiceLocator.Current.GetInstance<MainViewModel>();
            main.Running ^= true;
        }
    }
}
