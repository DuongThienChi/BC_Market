using BC_Market.BUS;
using BC_Market.Factory;
using BC_Market.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using System.Diagnostics;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView.VisualElements;

namespace BC_Market.ViewModels
{
    /// <summary>
    /// ViewModel for generating and displaying product reports.
    /// </summary>
    public class ReportProductViewModel : INotifyPropertyChanged
    {
        private IFactory<Order> _factory;
        private IBUS<Order> _bus;
        private ObservableCollection<KeyValuePair<string, long>> _listCateSale;

        /// <summary>
        /// Gets or sets the list of category sales.
        /// </summary>
        public ObservableCollection<KeyValuePair<string, long>> ListCateSale
        {
            get => _listCateSale;
            set
            {
                if (_listCateSale != value)
                {
                    _listCateSale = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<KeyValuePair<string, int>> _ListCate;

        /// <summary>
        /// Gets or sets the list of categories.
        /// </summary>
        public ObservableCollection<KeyValuePair<string, int>> ListCate
        {
            get => _ListCate;
            set
            {
                if (_ListCate != value)
                {
                    _ListCate = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<KeyValuePair<string, int>> _listProduct;

        /// <summary>
        /// Gets or sets the list of products.
        /// </summary>
        public ObservableCollection<KeyValuePair<string, int>> ListProduct
        {
            get => _listProduct;
            set
            {
                if (_listProduct != value)
                {
                    _listProduct = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTimeOffset _startDate;

        /// <summary>
        /// Gets or sets the start date for the report.
        /// </summary>
        public DateTimeOffset StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTimeOffset _endDate;

        /// <summary>
        /// Gets or sets the end date for the report.
        /// </summary>
        public DateTimeOffset EndDate
        {
            get => _endDate;
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the latest order date.
        /// </summary>
        public DateTime LatestOrderDate { get; set; }

        /// <summary>
        /// Gets or sets the first date for the report.
        /// </summary>
        public DateTime firstDate { get; set; } = new DateTime(2023, 1, 1);

        /// <summary>
        /// Command to generate the report.
        /// </summary>
        public ICommand GenerateReportCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportProductViewModel"/> class.
        /// </summary>
        public ReportProductViewModel()
        {
            LoadData();
            GenerateReportCommand = new RelayCommand(GenerateReport);
        }

        /// <summary>
        /// Generates the report.
        /// </summary>
        private void GenerateReport()
        {
            GenerateCircleProductReport();
            GenerateCircleCateReport();
            GenerateHorizontalReport();
        }

        /// <summary>
        /// Generates the horizontal report.
        /// </summary>
        private void GenerateHorizontalReport()
        {
            initHorizontalReport();
        }

        /// <summary>
        /// Gets or sets the data for the circle category report.
        /// </summary>
        public IEnumerable<ISeries> CircleCateReportData { get; set; }

        /// <summary>
        /// Generates the circle category report.
        /// </summary>
        private void GenerateCircleCateReport()
        {
            initCircleCateData();
        }

        /// <summary>
        /// Gets or sets the data for the circle product report.
        /// </summary>
        public IEnumerable<ISeries> CircleProductReportData { get; set; }

        /// <summary>
        /// Generates the circle product report.
        /// </summary>
        private void GenerateCircleProductReport()
        {
            initCircleProductData();
        }

        /// <summary>
        /// Gets the latest orders.
        /// </summary>
        /// <returns>The latest order.</returns>
        private Order getLatestOrders()
        {
            var orders = _bus.Get(new Dictionary<string, string>
                {
                    { "latest", "1" }
                });
            return orders;
        }

        /// <summary>
        /// Generates a date range.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>A list of dates in the range.</returns>
        private IEnumerable<string> GenerateDateRange(DateTime startDate, DateTime endDate)
        {
            var dates = new List<string>();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                dates.Add(date.ToString("MM/dd/yyyy"));
            }
            return dates;
        }

        /// <summary>
        /// Event handler for property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gets or sets the title for the circle product report.
        /// </summary>
        public LabelVisual CircleProductTitle { get; set; } =
        new LabelVisual
        {
            Text = "Percent of amount of product sold",
            TextSize = 25,
            Padding = new LiveChartsCore.Drawing.Padding(15),
            Paint = new SolidColorPaint(SKColors.White)
        };

        /// <summary>
        /// Initializes the data for the circle product report.
        /// </summary>
        private void initCircleProductData()
        {
            // init data
            ListProduct = _bus.Get(new Dictionary<string, string>
                {
                    { "reportProduct", "reportProduct" },
                    { "start", StartDate.ToString() },
                    { "end", EndDate.ToString() }
                });

            if (ListProduct == null)
            {
                ListProduct = new ObservableCollection<KeyValuePair<string, int>>();
                ListProduct.Add(new KeyValuePair<string, int>("None", 0));
                return;
            }

            long total = ListProduct.Sum(p => p.Value);

            var tempListProduct = new List<KeyValuePair<string, int>>();

            for (int i = 0; i < Math.Min(20, ListProduct.Count); i++)
            {
                tempListProduct.Add(ListProduct[i]);
            }

            if (ListProduct.Count - tempListProduct.Count > 1)
                tempListProduct.Add(new KeyValuePair<string, int>("Others", (int)(total - tempListProduct.Sum(p => p.Value))));
            else if (ListProduct.Count - tempListProduct.Count == 1)
                tempListProduct.Add(new KeyValuePair<string, int>(ListProduct[ListProduct.Count - 1].Key, ListProduct[ListProduct.Count - 1].Value));

            var curData = new ObservableCollection<ISeries>();
            foreach (var product in tempListProduct)
            {
                decimal percentage = (decimal)(product.Value * 1.0 / total) * 100;
                decimal roundedPercentage = (decimal)Math.Round(percentage, 2);
                curData.Add(new PieSeries<decimal>
                {
                    Values = new List<decimal> { roundedPercentage },
                    Name = product.Key,
                    IsVisibleAtLegend = true,
                    DataLabelsFormatter =
                    point =>
                    {
                        var pv = point.Coordinate.PrimaryValue;
                        var sv = point.StackedValue!;

                        var a = "";
                        return a;
                    },
                    ToolTipLabelFormatter =
                    point =>
                    {
                        var pv = point.Coordinate.PrimaryValue;
                        var sv = point.StackedValue!;

                        var a = $"{sv.Share:P2}";
                        return a;
                    },
                    DataLabelsPaint = new SolidColorPaint(SKColors.White)
                    {
                        SKTypeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold)
                    }
                });
            }

            CircleProductReportData = curData;
            OnPropertyChanged(nameof(CircleProductReportData));
        }

        /// <summary>
        /// Gets or sets the title for the circle category report.
        /// </summary>
        public LabelVisual CircleCateTitle { get; set; } =
        new LabelVisual
        {
            Text = "Percent of amount of category sold",
            TextSize = 25,
            Padding = new LiveChartsCore.Drawing.Padding(15),
            Paint = new SolidColorPaint(SKColors.White)
        };

        /// <summary>
        /// Initializes the data for the circle category report.
        /// </summary>
        private void initCircleCateData()
        {
            // init data
            ListCate = _bus.Get(new Dictionary<string, string>
                {
                    { "reportCate", "reportCate" },
                    { "start", StartDate.ToString() },
                    { "end", EndDate.ToString() }
                });

            if (ListCate == null)
            {
                ListCate = new ObservableCollection<KeyValuePair<string, int>>();
                ListCate.Add(new KeyValuePair<string, int>("None", 0));
                return;
            }

            long total = ListCate.Sum(p => p.Value);

            var curData = new ObservableCollection<ISeries>();
            foreach (var cate in ListCate)
            {
                decimal percentage = (decimal)(cate.Value * 1.0 / total) * 100;
                decimal roundedPercentage = (decimal)Math.Round(percentage, 2);
                curData.Add(new PieSeries<decimal>
                {
                    Values = new List<decimal> { roundedPercentage },
                    Name = cate.Key,
                    IsVisibleAtLegend = true,
                    DataLabelsFormatter =
                    point =>
                    {
                        var pv = point.Coordinate.PrimaryValue;
                        var sv = point.StackedValue!;

                        var a = $"{sv.Share:P2}";
                        return a;
                    },
                    ToolTipLabelFormatter =
                    point =>
                    {
                        var pv = point.Coordinate.PrimaryValue;
                        var sv = point.StackedValue!;

                        var a = $"{sv.Share:P2}";
                        return a;
                    },
                    DataLabelsPaint = new SolidColorPaint(SKColors.White)
                    {
                        SKTypeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold)
                    }
                });
            }

            CircleCateReportData = curData;
            OnPropertyChanged(nameof(CircleCateReportData));
        }

        /// <summary>
        /// Gets or sets the data for the horizontal category report.
        /// </summary>
        public IEnumerable<ISeries> HorizontalCateReportSeries { get; set; }

        /// <summary>
        /// Gets or sets the X axes for the horizontal category report.
        /// </summary>
        public ObservableCollection<ICartesianAxis> XAxes { get; set; }

        /// <summary>
        /// Gets or sets the Y axes for the horizontal category report.
        /// </summary>
        public ObservableCollection<ICartesianAxis> YAxes { get; set; } = new ObservableCollection<ICartesianAxis>
            {
                new Axis
                {
                    LabelsPaint = new SolidColorPaint(SKColors.Black),
                }
            };

        /// <summary>
        /// Gets or sets the title for the horizontal category report.
        /// </summary>
        public LabelVisual HorizontalTitle { get; set; } =
        new LabelVisual
        {
            Text = "Sale of each category",
            TextSize = 25,
            Padding = new LiveChartsCore.Drawing.Padding(15),
            Paint = new SolidColorPaint(SKColors.White)
        };

        /// <summary>
        /// Initializes the data for the horizontal category report.
        /// </summary>
        private void initHorizontalReport()
        {
            // init data
            ListCateSale = _bus.Get(new Dictionary<string, string>
                {
                    { "reportCateSale", "reportCateSale" },
                    { "start", StartDate.ToString() },
                    { "end", EndDate.ToString() }
                });

            if (ListCateSale == null)
            {
                ListCateSale = new ObservableCollection<KeyValuePair<string, long>>();
                ListCateSale.Add(new KeyValuePair<string, long>("None", 0));
                return;
            }

            var curData = new ObservableCollection<ISeries>();
            List<long> tempList = new List<long>();
            string[] X_labels = new string[ListCateSale.Count];
            foreach (var cate in ListCateSale)
            {
                tempList.Add(cate.Value);
                X_labels[ListCateSale.IndexOf(cate)] = cate.Key;
            }
            curData.Add(new ColumnSeries<long>
            {
                Values = tempList
            });

            HorizontalCateReportSeries = curData;
            XAxes = new ObservableCollection<ICartesianAxis>
                {
                    new Axis
                    {
                        Labels = X_labels,
                        LabelsPaint = new SolidColorPaint(SKColors.Black),
                    }
                };

            OnPropertyChanged(nameof(HorizontalCateReportSeries));
            OnPropertyChanged(nameof(XAxes));
        }

        /// <summary>
        /// Loads the initial data for the reports.
        /// </summary>
        private void LoadData()
        {
            _factory = new OrderFactory();
            _bus = _factory.CreateBUS();
            LatestOrderDate = getLatestOrders().createAt;
            StartDate = LatestOrderDate;
            EndDate = StartDate;
            initCircleProductData();
            initCircleCateData();
            initHorizontalReport();
        }
    }
}