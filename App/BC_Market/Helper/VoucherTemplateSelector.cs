using BC_Market.Models;
using BC_Market.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace BC_Market.VoucherTemplateSelector
{
    public class VoucherTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ValidVoucherTemplate { get; set; }
        public DataTemplate InvalidVoucherTemplate { get; set; }

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