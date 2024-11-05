namespace DZ4_Stack
{
    public static class StackExtensions
    {
        public static void Merge(this Stack s1, Stack s2)
        {
            string value;
            while (s2.Top != null)
            {
                value = s2.Pop();
                s1.Add(value);
            };
        }
    }
}
