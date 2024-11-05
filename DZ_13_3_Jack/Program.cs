using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace DZ_13_3_Jack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ImmutableArray<string> poem = ImmutableArray<string>.Empty;
            Part[] parts = { new Part1(), new Part2(), new Part3(), new Part4(), new Part5(), new Part6(), new Part7(), new Part8(), new Part9() };

            /* Заполнение поэмы */
            parts[0].AddPart(poem);

            for (int i = 1; i < parts.Length; ++i)
            {
                parts[i].AddPart(parts[i-1].Poem);
            }

            /* Проверка частей */
            Console.WriteLine($"Изначальная poem:\n{string.Join("\n", poem)}");
            for (int i = 0; i < parts.Length; ++i)
            {
                Console.WriteLine($"\nPoem в Part{i+1}:\n{string.Join("\n", parts[i].Poem)}");
            }
        }
    }
}
