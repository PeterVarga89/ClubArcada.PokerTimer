using ClubArcada.BusinessObjects;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ClubArcada.PokerTimer.Win.Controls
{
    public partial class InfoCtrl : UserControl, INotifyPropertyChanged
    {
        # region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChange(Property property)
        {
            PropertyChanged.Raise(this, property.ToString());
        }

        private enum Property
        {
            NotSet = 0,
            Type,
            Message
        }

        # endregion

        private static void MessagePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            InfoCtrl uc1 = (InfoCtrl)dependencyObject;
            uc1.Message = (int)dependencyPropertyChangedEventArgs.NewValue;
            
        }
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(int), typeof(InfoCtrl), new PropertyMetadata(new PropertyChangedCallback(MessagePropertyChanged)));
        public int Message
        {
            get
            {
                return (int)GetValue(MessageProperty);
            }
            set
            {
                SetValue(MessageProperty, value);
            }

        }

        private static void TypePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            InfoCtrl uc1 = (InfoCtrl)dependencyObject;
            uc1.Type = (eInfoCtrlType)dependencyPropertyChangedEventArgs.NewValue;

        }
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(eInfoCtrlType), typeof(InfoCtrl), new PropertyMetadata(new PropertyChangedCallback(TypePropertyChanged)));
        public eInfoCtrlType Type
        {
            get
            {
                return (eInfoCtrlType)GetValue(TypeProperty);
            }
            set
            {
                SetValue(TypeProperty, value);
            }

        }

        public InfoCtrl()
        {
            InitializeComponent();
            DataContext = this;
        }


    }
}
