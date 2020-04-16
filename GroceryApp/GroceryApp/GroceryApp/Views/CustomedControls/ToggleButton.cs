using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.Views.CustomedControls
{
    public class ToggleButton : ContentView
    {
        private ICommand _toggleCommand;
        private Image _toggleImage;

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(ToggleButton), null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(ToggleButton), null);

        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create("Checked", typeof(bool), typeof(ToggleButton), false, BindingMode.TwoWay,
                null, propertyChanged: OnCheckedChanged);

        public static readonly BindableProperty AnimateProperty =
            BindableProperty.Create("Animate", typeof(bool), typeof(ToggleButton), false);

        public static readonly BindableProperty CheckedImageProperty =
            BindableProperty.Create("CheckedImage", typeof(ImageSource), typeof(ToggleButton), null);

        public static readonly BindableProperty UnCheckedImageProperty =
            BindableProperty.Create("UnCheckedImage", typeof(ImageSource), typeof(ToggleButton), null);

        public ToggleButton()
        {
            Initialize();
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        public bool Animate
        {
            get { return (bool)GetValue(AnimateProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        public ImageSource CheckedImage
        {
            get { return (ImageSource)GetValue(CheckedImageProperty); }
            set { SetValue(CheckedImageProperty, value); }
        }

        public ImageSource UnCheckedImage
        {
            get { return (ImageSource)GetValue(UnCheckedImageProperty); }
            set { SetValue(UnCheckedImageProperty, value); }
        }

        public ICommand ToogleCommand
        {
            get
            {
                return _toggleCommand
                       ?? (_toggleCommand = new Command(() =>
                       {
                           

                           if (Command != null)
                           {
                               Command.Execute(CommandParameter);
                           }
                       }));
            }
        }

        private void Initialize()
        {
            _toggleImage = new Image();

            Animate = true;

            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ToogleCommand
            });

            _toggleImage.Source = UnCheckedImage;
            Content = _toggleImage;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            _toggleImage.Source = UnCheckedImage;
            Content = _toggleImage;
        }

        public async void animate()
        {
            //await this.ScaleTo(0.9, 50, Easing.Linear);
            //await Task.Delay(70);
            await this.ScaleTo(1.1, 50, Easing.Linear);
            await Task.Delay(70);
            await this.ScaleTo(1, 50, Easing.Linear);
        }

        private static void OnCheckedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var toggleButton = (ToggleButton)bindable;

            

            if (toggleButton.Checked)
            {
                toggleButton._toggleImage.Source = toggleButton.CheckedImage;
            }
            else
            {
                toggleButton._toggleImage.Source = toggleButton.UnCheckedImage;
            }

            toggleButton.Content = toggleButton._toggleImage;

            
        }
    }
}