using System;
using Reactive.Bindings;
using ReactiveUI;
using System.Reactive.Linq;

namespace ReactiveWpfTest
{
    public class ShellViewModel : ReactiveObject
    {
        public ReactiveProperty<string> Name { get; } = new("");
        public ReactiveProperty<string> DisplayName { get; }

        public ShellViewModel()
        {
            DisplayName = Name.Sample(TimeSpan.FromMilliseconds(500)).ToReactiveProperty();
        }
    }
}
