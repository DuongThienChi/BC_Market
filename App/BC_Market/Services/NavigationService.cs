using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Services
{
    /// <summary>
    /// Provides navigation services for the application.
    /// </summary>
    public static class NavigationService
    {
        private static Frame _frame;

        /// <summary>
        /// Initializes the navigation service with the specified frame.
        /// </summary>
        /// <param name="frame">The frame to be used for navigation.</param>
        public static void Initialize(Frame frame)
        {
            _frame = frame;
        }

        /// <summary>
        /// Navigates to the specified page type with the given parameter.
        /// </summary>
        /// <param name="pageType">The type of the page to navigate to.</param>
        /// <param name="param">The parameter to pass to the page.</param>
        public static void Navigate(Type pageType, dynamic param)
        {
            _frame?.Navigate(pageType, param);
        }

        /// <summary>
        /// Navigates back to the previous page in the navigation stack.
        /// </summary>
        public static void GoBack()
        {
            if (_frame?.CanGoBack == true)
            {
                _frame.GoBack();
            }
        }
    }

}
