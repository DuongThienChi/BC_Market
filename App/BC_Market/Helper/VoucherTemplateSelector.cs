using BC_Market.Models;
using BC_Market.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace BC_Market.VoucherTemplateSelector
{
    /// <summary>
    /// Selects a data template for a voucher based on its validity.
    /// </summary>
    public class VoucherTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the data template for a valid voucher.
        /// </summary>
        public DataTemplate ValidVoucherTemplate { get; set; }

        /// <summary>
        /// Gets or sets the data template for an invalid voucher.
        /// </summary>
        public DataTemplate InvalidVoucherTemplate { get; set; }

        /// <summary>
        /// Selects the appropriate data template based on the voucher's validity.
        /// </summary>
        /// <param name="item">The data item to select the template for.</param>
        /// <param name="container">The container in which the template is to be displayed.</param>
        /// <returns>The selected data template.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the selected template is null.</exception>
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var voucher = item as Voucher;
            var viewModel = (container as FrameworkElement)?.DataContext as ShopperOrderViewModel;

            if (voucher != null && viewModel != null)
            {
                var selectedTemplate = voucher.isCondition(viewModel.Total) ? ValidVoucherTemplate : InvalidVoucherTemplate;
                if (selectedTemplate == null)
                {
                    throw new InvalidOperationException("Selected template is null. Ensure that ValidVoucherTemplate and InvalidVoucherTemplate are correctly assigned.");
                }
                return selectedTemplate;
            }

            return base.SelectTemplateCore(item, container);
        }
    }
}