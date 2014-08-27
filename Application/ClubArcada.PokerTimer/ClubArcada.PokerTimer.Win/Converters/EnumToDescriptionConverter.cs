using ClubArcada.BusinessObjects;
using System;
using System.Windows.Data;

namespace ClubArcada.PokerTimer.Win.Converters
{
    public class EnumToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value is eInfoCtrlType)
                return value.ToEnum<eInfoCtrlType>().GetEnumDescription();

            throw new NullReferenceException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
