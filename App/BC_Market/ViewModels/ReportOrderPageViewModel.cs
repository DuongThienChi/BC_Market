using BC_Market.BUS;
using BC_Market.Factory;
using BC_Market.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using OpenQA.Selenium.DevTools.V129.Page;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Diagnostics;
using PropertyChanged;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.Immutable;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.IO;
using System.Windows.Forms;
namespace BC_Market.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public partial class ReportOrderPageViewModel : ObservableObject
    {
        private IFactory<Order> _factory;
        private IBUS<Order> _bus;
        public ICommand GenerateReportCommand { get; set; }
        public ICommand SelectedFilterCommand { get; set; }
        public ICommand ExportExcelCommand { get; set; }
        public ObservableCollection<ISeries> Series { get; set; }
        public ObservableCollection<ICartesianAxis> XAxes { get; set; }
        public ObservableCollection<ICartesianAxis> YAxes { get; set; } = new ObservableCollection<ICartesianAxis>
        {
            new Axis
            {
                LabelsPaint = new SolidColorPaint(SKColors.Black),
            }
        };
        public ObservableCollection<string> Filters { get; set; } = new ObservableCollection<string>
        {
            "Quarter per Year",
            "Month per Year"
        };
        [ObservableProperty]
        private string selectedFilter;

        public Dictionary<string, float> groupedOrders { get; set; }

        public ReportOrderPageViewModel()
        {
            LoadData();
            GenerateReportCommand = new RelayCommand(GenerateReport);
            ExportExcelCommand = new RelayCommand(ExportExcel);
        }

        private void ExportExcel()
        {
            if (string.IsNullOrEmpty(SelectedFilter))
                return;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Save an Excel File"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = saveFileDialog.FileName;

                try
                {
                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add("Report");

                        worksheet.Cells[1, 1].Value = "Period";
                        worksheet.Cells[1, 2].Value = "Total Price";

                        var row = 2;
                        foreach (var order in groupedOrders)
                        {
                            worksheet.Cells[row, 1].Value = order.Key;
                            worksheet.Cells[row, 2].Value = order.Value;
                            row++;
                        }

                        // Format header
                        using (var range = worksheet.Cells[1, 1, 1, 2])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                        }

                        worksheet.Cells.AutoFitColumns();

                        var file = new FileInfo(filePath);
                        package.SaveAs(file);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        private void GenerateReport()
        {
            Debug.WriteLine(SelectedFilter);
            if (string.IsNullOrEmpty(SelectedFilter))
                return;

            var Labels = new string[] { "Q1", "Q2", "Q3", "Q4" };
            if (SelectedFilter == "Quarter per Year")
            {
                groupedOrders = ListOrder
                    .GroupBy(o => $"Q{((o.createAt.Month - 1) / 3) + 1} {o.createAt.Year}")
                    .ToDictionary(g => g.Key, g => g.Sum(o => o.totalPrice));
                Labels = new string[] { "Q1", "Q2", "Q3", "Q4" };
            }
            else if (SelectedFilter == "Month per Year")
            {
                groupedOrders = ListOrder
                     .OrderBy(o => o.createAt)
                    .GroupBy(o => $"{o.createAt:MMMM yyyy}")       
                    .ToDictionary(g => g.Key, g => g.Sum(o => o.totalPrice));
                Labels = ListOrder
                    .OrderBy(o => o.createAt)
                    .Select(o => $"{o.createAt:MMMM yyyy}")
                    .Distinct()
                    .ToArray();
            }

            Series = new ObservableCollection<ISeries>
            {
                new LineSeries<float>
                {
                    Values = groupedOrders.Values.ToArray()
                },
                new ColumnSeries<float>
                {
                    Values = groupedOrders.Values.ToArray()
                }
            };
            XAxes = new ObservableCollection<ICartesianAxis>
            {
                new Axis
                {

                    Labels = Labels,
                    LabelsPaint = new SolidColorPaint(SKColors.Black),
                }
            };

            OnPropertyChanged(nameof(Series));
            OnPropertyChanged(nameof(XAxes));
        }

        public ObservableCollection<Order> ListOrder { get; set; }

        void LoadData()
        {
            _factory = new OrderFactory();
            _bus = _factory.CreateBUS();
            var orders = _bus.Get(null);
            ListOrder = new ObservableCollection<Order>(orders);
            foreach (var order in ListOrder)
            {
                Dictionary<string, string> config = new Dictionary<string, string>();
                config.Add("OrderId", order.Id);
                order.Products = _bus.Get(config);
            }
            if (ListOrder != null)
            {
                SelectedFilter = "Quarter per Year";
                GenerateReport();
            }
        }
    }
}
