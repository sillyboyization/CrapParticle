using System;
using System.Collections.Generic;
using System.Threading;

namespace Crap
{
    class Program
    {
        static Random rand;
        static Map map;
        static void Main(string[] args) 
        {
            rand = new Random();
            map = new Map(5, 17);
            
            for(int i = 0; i < 22; i++)
                map.AddParticle(new Particle(map.MAXHEIGHT + 1, (char)rand.Next(33, 127), rand.Next(0, 41)));

            while(true) 
            {
                map.Update();

                foreach (Particle p in map.Members) 
                {
                    int chance = rand.Next(4);
                    //p.existingTime++;
                    if (chance == 0) 
                    {
                        if (p.Pos.x < map.MAXWIDTH)
                            p.Pos.x++;
                    }

                    else if (chance == 1) 
                    {
                        if (p.Pos.x > 0)
                            p.Pos.x--;
                    }

                    else if (chance == 2) 
                    {
                        if (p.Pos.y < map.MAXHEIGHT)
                            p.Pos.y++;
                    }   

                    else if (chance == 3) 
                    {
                        if (p.Pos.y > 0)
                            p.Pos.y--;
                    }
                }
                Thread.Sleep(250);
            }    
        }
    }

    public class Coord2D 
    {
        public int x, y;
        public Coord2D(int x, int y) 
        {
            this.x = x;
            this.y = y;
        }
        public static bool Equals(Coord2D c1, Coord2D c2) 
        {
            return (c1.x == c2.x && c1.y == c2.y);
        }
    }

    public class Map 
    {
        public int MAXWIDTH, MAXHEIGHT;
        public List<Particle> Members = new List<Particle>();
        public Map(int MAXWIDTH, int MAXHEIGHT) 
        {
            this.MAXWIDTH = MAXWIDTH;
            this.MAXHEIGHT = MAXHEIGHT;
        }

        public void Update() 
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Particles Added: " + Members.Count.ToString());
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" Current Particle: " + Members[Members.Count - 1].rep + "\n");
            Console.ResetColor();
            for (int x = 0; x < MAXHEIGHT; x++)
            {
                for (int y = 0; y < MAXWIDTH; y++)
                {
                    int yy = y;
                    if (y > yy)
                        Console.WriteLine();

                    foreach (Particle p in Members) 
                    {
                        if (p.Pos.x == x && p.Pos.y == y) 
                        {
                            Console.Write(p.rep);
                            x++;
                        }
                        else 
                        {
                            Console.Write(" ");
                        }
                    }
                }
            }
        }

        public void AddParticle(Particle particle) 
        {
            Members.Add(particle);
        }

        public void RemoveParticle(Particle particle) 
        {
            Members.Remove(particle);
        }
    }

    public class Particle 
    {
        public Coord2D Pos; //Position
        public char rep; //ASCII representation

        #region un-used
        public int lifeTime; //max existing time
        public int existingTime; //current existing time
        #endregion

        public Particle(int range, char rep, int lifeTime) 
        {
            Random r = new Random();
            this.Pos = new Coord2D(r.Next(0, range), r.Next(0, range));
            this.rep = rep;
            this.lifeTime = lifeTime;
        }
    }
}