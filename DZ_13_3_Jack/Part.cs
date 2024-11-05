using System.Collections.Immutable;

namespace DZ_13_3_Jack
{
    public class Part
    {
        public virtual List<string> newPart { get { return new List<string>(); } }
        public ImmutableArray<string> Poem { get; set; }
        public Part()
        {
            Poem = new ImmutableArray<string>();
        }
        public void AddPart(ImmutableArray<string> part)
        {
            Poem = part.AddRange(newPart);
        }
    }
    public class Part1 : Part
    {
        override public List<string> newPart { 
            get { return new List<string> {
                "Вот дом,",
                "Который построил Джек." 
                }; 
            } 
        }
    }
    public class Part2 : Part
    {
        override public List<string> newPart { 
            get { return new List<string> { 
                "А это пшеница,",
                "Которая в темном чулане хранится",
                "В доме,",
                "Который построил Джек." 
                }; 
            } 
        }
    }
    public class Part3 : Part
    {
        override public List<string> newPart { 
            get { return new List<string> {
                "А это веселая птица-синица,",
                "Которая часто ворует пшеницу,",
                "Которая в темном чулане хранится",
                "В доме,",
                "Который построил Джек." 
                }; 
            } 
        }
    }
    public class Part4 : Part
    {
        override public List<string> newPart { 
            get { return new List<string> {
                "Вот кот,",
                "Который пугает и ловит синицу,",
                "Которая часто ворует пшеницу,",
                "Которая в темном чулане хранится",
                "В доме,",
                "Который построил Джек." 
                }; 
            } 
        }
    }

    public class Part5 : Part
    {
        override public List<string> newPart
        {
            get
            {
                return new List<string> {
                "Вот пес без хвоста,",
                "Который за шиворот треплет кота,",
                "Который пугает и ловит синицу,",
                "Которая часто ворует пшеницу,",
                "Которая в темном чулане хранится",
                "В доме,",
                "Который построил Джек."
                };
            }
        }
    }
    public class Part6 : Part
    {
        override public List<string> newPart
        {
            get
            {
                return new List<string> {
                "А это корова безрогая,",
                "Лягнувшая старого пса без хвоста,",
                "Который за шиворот треплет кота,",
                "Который пугает и ловит синицу,",
                "Которая часто ворует пшеницу,",
                "Которая в темном чулане хранится",
                "В доме,",
                "Который построил Джек."
                };
            }
        }
    }
    public class Part7 : Part
    {
        override public List<string> newPart
        {
            get
            {
                return new List<string> {
                "А это старушка, седая и строгая,",
                "Которая доит корову безрогую,",
                "Лягнувшую старого пса без хвоста,",
                "Который за шиворот треплет кота,",
                "Который пугает и ловит синицу,",
                "Которая часто ворует пшеницу,",
                "Которая в темном чулане хранится",
                "В доме,",
                "Который построил Джек."
                };
            }
        }
    }
    public class Part8 : Part
    {
        override public List<string> newPart
        {
            get
            {
                return new List<string> {
                "А это ленивый и толстый пастух,",
                "Который бранится с коровницей строгою,",
                "Которая доит корову безрогую,",
                "Лягнувшую старого пса без хвоста,",
                "Который за шиворот треплет кота,",
                "Который пугает и ловит синицу,",
                "Которая часто ворует пшеницу,",
                "Которая в темном чулане хранится",
                "В доме,",
                "Который построил Джек."
                };
            }
        }
    }
    public class Part9 : Part
    {
        override public List<string> newPart
        {
            get
            {
                return new List<string> {
                "Вот два петуха,",
                "Которые будят того пастуха,",
                "Который бранится с коровницей строгою,",
                "Которая доит корову безрогую,",
                "Лягнувшую старого пса без хвоста,",
                "Который за шиворот треплет кота,",
                "Который пугает и ловит синицу,",
                "Которая часто ворует пшеницу,",
                "Которая в темном чулане хранится",
                "В доме,",
                "Который построил Джек."
                };
            }
        }
    }
}
