using Reactive.Bindings;
using ReactiveUI;

namespace ReactiveWpfColorList
{
    public class NumberModel : ReactiveObject
    {
        public ReactiveProperty<int> A { get; }
        public ReactiveProperty<int> B { get; }
        public ReactiveProperty<int> C { get; }

        public NumberModel(int a, int b, int c)
        {
            A = new ReactiveProperty<int>(a);
            B = new ReactiveProperty<int>(b);
            C = new ReactiveProperty<int>(c);
        }
    }
}
