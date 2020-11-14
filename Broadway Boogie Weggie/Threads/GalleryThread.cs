using Broadway_Boogie_Weggie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Broadway_Boogie_Weggie.Threads
{
    public class GalleryThread
    {
        public Gallery Gallery;
        private Thread thread;

        public GalleryThread(Gallery gallery)
        {
            this.Gallery = gallery;
            thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }

        private void Run()
        {
            while (true)
            {
                Gallery.Tick();
                Gallery.Draw();
                System.Threading.Thread.Sleep(1000 / 60);
            }
        }

        public void Abort()
        {
            thread.Abort();
        }
    }
}
