﻿using MyLib.Wpf;
using System.Windows;
using System.Windows.Input;

namespace MyPad.Views.Components
{
    public class NumericUpDownEx : MahApps.Metro.Controls.NumericUpDown
    {
        public static readonly DependencyProperty ReverseOnMouseWheelProperty = Interactor.RegisterDependencyProperty();

        public bool ReverseOnMouseWheel
        {
            get => (bool)this.GetValue(ReverseOnMouseWheelProperty);
            set => this.SetValue(ReverseOnMouseWheelProperty, value);
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            if (this.ReverseOnMouseWheel)
                base.OnPreviewMouseWheel(new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta * -1) { RoutedEvent = MouseWheelEvent });
            else
                base.OnPreviewMouseWheel(e);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (this.ReverseOnMouseWheel)
            {
                switch (e.Key)
                {
                    case Key.Up:
                        base.OnPreviewKeyDown(new KeyEventArgs(e.KeyboardDevice, e.InputSource, e.Timestamp, Key.Down) { RoutedEvent = KeyDownEvent });
                        break;
                    case Key.Down:
                        base.OnPreviewKeyDown(new KeyEventArgs(e.KeyboardDevice, e.InputSource, e.Timestamp, Key.Up) { RoutedEvent = KeyUpEvent });
                        break;
                    default:
                        base.OnPreviewKeyDown(e);
                        break;
                }
            }
            else
            {
                base.OnPreviewKeyDown(e);
            }
        }
    }
}
