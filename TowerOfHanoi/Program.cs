using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfHanoi
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Hanoi();
            }
        }

        static void Hanoi()
        {
            List<Disc> tower1 = new List<Disc>();
            tower1.Add(new Disc(1, 3, ConsoleColor.Green));//bottom
            tower1.Add(new Disc(2, 2, ConsoleColor.White));//mid
            tower1.Add(new Disc(3, 1, ConsoleColor.Red));//top
            List<Disc> tower2 = new List<Disc>();
            List<Disc> tower3 = new List<Disc>();
            List<List<Disc>> towers = new List<List<Disc>>();
            towers.Add(tower1);
            towers.Add(tower2);
            towers.Add(tower3);

            bool keepPlaying = true;

            int numberOfMoves = 0;
            while (keepPlaying)
            {
                Console.Clear();
                DrawHanoi(towers);
                Console.Write("Move disc: ");
                char itemToMove = Console.ReadKey().KeyChar;
                Console.Write(" to tower: ");
                char moveTo = Console.ReadKey().KeyChar;
                UpdateHanoi(ref towers, itemToMove, moveTo, ref keepPlaying, ref numberOfMoves);
            }
        }

        static void UpdateHanoi(ref List<List<Disc>> towers, char itemToMove, char moveTo, ref bool keepPlaying, ref int numberOfMoves)
        {
            int itemToMoveParse;
            int moveToParse;
            Int32.TryParse(itemToMove.ToString(), out itemToMoveParse);
            Int32.TryParse(moveTo.ToString(), out moveToParse);
            if (Int32.TryParse(itemToMove.ToString(), out itemToMoveParse) && Int32.TryParse(moveTo.ToString(), out moveToParse))
            {
                Disc disc = getDisc(towers, itemToMoveParse);
                if (discFree(towers[findTower(towers, itemToMoveParse)-1],itemToMoveParse) && movePossible(towers,disc,moveToParse))
                {
                    numberOfMoves++;
                    MoveDisc(disc, ref towers, moveToParse);
                    if (towers[2].Count==3&&towers[1].Count==0&&towers[0].Count==0)
                    {
                        keepPlaying = false;
                        Console.Clear();
                        DrawHanoi(towers);
                        Console.WriteLine(Environment.NewLine+"You won in "+numberOfMoves.ToString()+" moves, congratulations!");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine(Environment.NewLine+"Illegal move");
                    Console.ReadKey();
                }
            }
        }
        
        static void MoveDisc(Disc disc, ref List<List<Disc>> towers, int moveTo)
        {
            towers[findTower(towers, disc.number) - 1].Remove(disc);
            towers[moveTo - 1].Add(disc);
        }

        static Disc getDisc(List<List<Disc>> towers, int discNumber)
        {
            foreach (List<Disc> tower in towers)
            {
                foreach (Disc disc in tower)
                {
                    if (disc.number==discNumber)
                    {
                        return disc;
                    }
                }
            }
            return new Disc(0,0,ConsoleColor.Black);
        }

        static bool movePossible(List<List<Disc>> towers, Disc disc, int moveToParse)
        {
            int i = 0;
            foreach (List<Disc> tower in towers)
            {
                i++;
                if (moveToParse==i)
                {
                    if (tower.Count == 0 || (tower.Count > 0 && tower.Last() != null && tower.Last().size > disc.size))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static bool discFree(List<Disc> tower, int discNumber)
        {
            if (tower.Last().number==discNumber)
            {
                return true;
            }
            return false;
        }

        static int findTower(List<List<Disc>> towers, int discNumber)
        {
            int t = 0;
            foreach (List<Disc> tower in towers)
            {
                t++;
                foreach (Disc d in tower)
                {
                    if (d.number==discNumber)
                    {
                        return t;
                    }
                }
            }
            return 1;
        }

        static void DrawHanoi(List<List<Disc>> towers)
        {
            Console.WriteLine("Move all discs to tower 3"+Environment.NewLine);
            for (int a=0;a<towers.Count;a++)
            {
                Console.WriteLine("Tower "+(a+1).ToString()+":");
                for (int i = towers[a].Count - 1; i >= 0; i--)
                {
                    Disc d = towers[a][i];
                    string discString = new string(' ', d.size) + d.number + new string(' ', d.size);

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(new string(' ', d.number));

                    Console.BackgroundColor = d.color;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(discString);
                    
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(new string(' ', d.number));

                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}