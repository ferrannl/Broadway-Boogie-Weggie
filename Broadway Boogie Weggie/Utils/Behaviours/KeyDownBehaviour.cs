using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Broadway_Boogie_Weggie.Utils.Behaviours
{
    public class KeyDownBehaviour : Behavior<UIElement>
    {
        public ICommand KeyDownCommand
        {
            get { return (ICommand)GetValue(KeyDownCommandProperty); }
            set { SetValue(KeyDownCommandProperty, value); }
        }

        public static readonly DependencyProperty KeyDownCommandProperty =
            DependencyProperty.Register("KeyDownCommand", typeof(ICommand), typeof(KeyDownBehaviour), new UIPropertyMetadata(null));


        protected override void OnAttached()
        {
            AssociatedObject.KeyDown += new KeyEventHandler(AssociatedObjectKeyDown);
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.KeyDown -= new KeyEventHandler(AssociatedObjectKeyDown);
            base.OnDetaching();
        }

        private void AssociatedObjectKeyDown(object sender, KeyEventArgs e)
        {
            if (KeyDownCommand != null)
            {
                KeyDownCommand.Execute(e.Key);
            }
        }
    }
}
