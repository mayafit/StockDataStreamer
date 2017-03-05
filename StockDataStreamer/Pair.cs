namespace StockDataStreamer
{
    public class Pair<T, S>
    {
        public T First { get; private set; }
        public S Second { get; private set; }

        public Pair(T first , S second)
        {
            this.First = first;
            this.Second = second;
        }
        public override string ToString()
        {
            return Second.ToString();
        }
    }
}
