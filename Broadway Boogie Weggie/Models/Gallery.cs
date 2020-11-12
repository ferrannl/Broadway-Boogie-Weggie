using Broadway_Boogie_Weggie.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Models
{
    public class Gallery
    {
        public readonly static int WIDTH, HEIGHT = 800;
        public MainViewModel mainViewModel;

        public Gallery(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        public void SetupGallery()
        {

        }
    }
}
