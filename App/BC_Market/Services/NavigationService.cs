using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Services
{
    public static class NavigationService
    {
        private static Frame _frame;

        public static void Initialize(Frame frame)
        {
            _frame = frame;
        }

        public static void Navigate(Type pageType , dynamic param)
        {
            _frame?.Navigate(pageType, param);
        }

        public static void GoBack()
        {
            if (_frame?.CanGoBack == true)
            {
                _frame.GoBack();
            }
        }
    }

}
