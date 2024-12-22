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
    public class ReportProductViewModel : INotifyPropertyChanged
    {
        private IFactory<Order> _factory;
        private IBUS<Order> _bus;
        private ObservableCollection<KeyValuePair<string, long>> _listCateSale;
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

        public DateTime LatestOrderDate { get; set; }
        public DateTime firstDate { get; set; } = new DateTime(2023, 1, 1);
        public ICommand GenerateReportCommand { get; set; }

        public ReportProductViewModel()
        {
            LoadData();
            GenerateReportCommand = new RelayCommand(GenerateReport);
        }

        private void GenerateReport()
        {
            GenerateCircleProductReport();
            GenerateCircleCateReport();
            GenerateHorizontalReport();
        }

        private void GenerateHorizontalReport()
        {
            initHorizontalReport();
        }

        public IEnumerable<ISeries> CircleCateReportData { get; set; }
        private void GenerateCircleCateReport()
        {
            initCircleCateData();
        }

        public IEnumerable<ISeries> CircleProductReportData { get; set; }
        private void GenerateCircleProductReport()
        {
            initCircleProductData();
        }

        private Order getLatestOrders()
        {
            var orders = _bus.Get(new Dictionary<string, string>
            {
                { "latest", "1" }
            });
            return orders;
        }

        private IEnumerable<string> GenerateDateRange(DateTime startDate, DateTime endDate)
        {
            var dates = new List<string>();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                dates.Add(date.ToString("MM/dd/yyyy"));
            }
            return dates;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public LabelVisual CircleProductTitle { get; set; } =
        new LabelVisual
        {
            Text = "Percent of amount of product sold",
            TextSize = 25,
            Padding = new LiveChartsCore.Drawing.Padding(15),
        };
        private void initCircleProductData()
        {
            // init data
            ListProduct = _bus.Get(new Dictionary<string, string>
            {
                { "reportProduct", "reportProduct" },
                { "start", StartDate.ToString() },
                { "end", EndDate.ToString() }
            });

            if(ListProduct == null)
            {
                ListProduct = new ObservableCollection<KeyValuePair<string, int>>();
                ListProduct.Add(new KeyValuePair<string, int>("None", 0));
                return;
            }

            long total = ListProduct.Sum(p => p.Value);

            var tempListProduct = new List<KeyValuePair<string, int>>();

            for(int i = 0; i< Math.Min(20, ListProduct.Count); i++)
            {
                tempListProduct.Add(ListProduct[i]);
            }

            if (ListProduct.Count - tempListProduct.Count > 1)
                tempListProduct.Add(new KeyValuePair<string, int>("Others", (int)(total - tempListProduct.Sum(p => p.Value))));
            else if (ListProduct.Count - tempListProduct.Count == 1)
                tempListProduct.Add(new KeyValuePair<string, int>(ListProduct[ListProduct.Count-1].Key, ListProduct[ListProduct.Count - 1].Value));

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

        public LabelVisual CircleCateTitle { get; set; } =
        new LabelVisual
        {
            Text = "Percent of amount of category sold",
            TextSize = 25,
            Padding = new LiveChartsCore.Drawing.Padding(15)
        };
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

        public IEnumerable<ISeries> HorizontalCateReportSeries { get; set; }
        public ObservableCollection<ICartesianAxis> XAxes { get; set; }
        public ObservableCollection<ICartesianAxis> YAxes { get; set; } = new ObservableCollection<ICartesianAxis>
        {
            new Axis
            {
                LabelsPaint = new SolidColorPaint(SKColors.Black),
            }
        };

        public LabelVisual HorizontalTitle { get; set; } =
        new LabelVisual
        {
            Text = "Sale of each category",
            TextSize = 25,
            Padding = new LiveChartsCore.Drawing.Padding(15)
        };
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