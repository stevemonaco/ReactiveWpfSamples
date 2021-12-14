using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Data;
using System.Windows.Threading;
using DynamicData;
using DynamicData.Aggregation;
using Reactive.Bindings;
using ReactiveUI;

namespace ReactiveWpfColorList
{
    public class ShellViewModel : ReactiveObject
    {
        public ReactiveProperty<double> AverageA { get; } = new(15);
        public ReactiveProperty<double> AverageB { get; } = new(15);
        public ReactiveProperty<double> AverageC { get; } = new(15);

        private readonly ReadOnlyObservableCollection<NumberModel> _models;
        public ReadOnlyObservableCollection<NumberModel> Models { get => _models; }

        private readonly SourceList<NumberModel> _modelSource = new();
        private readonly DispatcherTimer _timer = new();
        private readonly Random _random = new();

        public CollectionViewSource SortedModelSource { get; }

        public ShellViewModel()
        {
            _modelSource.Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _models)
                .Subscribe();

            _modelSource.Connect()
                .Avg(x => x.A.Value)
                .Subscribe(x => { AverageA.Value = x; });

            _modelSource.Connect()
                .Avg(x => x.B.Value)
                .Subscribe(x => { AverageB.Value = x; });

            _modelSource.Connect()
                .Avg(x => x.C.Value)
                .Subscribe(x => { AverageC.Value = x; });

            SortedModelSource = new CollectionViewSource();
            SortedModelSource.Source = Models;
            SortedModelSource.SortDescriptions.Add(new SortDescription("C.Value", ListSortDirection.Descending));

            _timer.Interval = TimeSpan.FromMilliseconds(200);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            int a = _random.Next(31);
            int b = _random.Next(31);
            int c = _random.Next(31);

            _modelSource.Add(new NumberModel(a, b, c));
        }
    }
}
