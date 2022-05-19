using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TimePickerControl.CustomControl
{
    public class TimePickerTextBox : TextBox
    {
        static TimePickerTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimePickerTextBox), new FrameworkPropertyMetadata(typeof(TimePickerTextBox)));
        }
    }
}
