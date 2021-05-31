using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HW6
{
    abstract class Car
    {
        protected Car(int topspeed, string color)
        {
            TopSpeed = topspeed;
            Color = color;
        }

        public int TopSpeed { get; set; }
        public string Color { get; set; }

        public abstract float Drive(int finish);

        public static void GetWinner(Car car1, Car car2, int finish)
        {
            if (car1.Drive(finish) < car2.Drive(finish))
            {
                Console.WriteLine($"{car1.Color} {car1.GetType().Name} is the winner!\nFinished {finish} meters in {car1.Drive(finish)} seconds.");
            }
            else if(car1.Drive(finish) == car2.Drive(finish))
            {
                Console.WriteLine($"TIE!\nBoth finished {finish} meters in {car1.Drive(finish)} seconds.");
            }
            else
            {
                Console.WriteLine($"{car2.Color} {car2.GetType().Name} is the winner!\nFinished {finish} meters in {car2.Drive(finish)} seconds.");
            }
        }
    }

    class SportCar : Car
    {
        public SportCar(int topspeed, string color) : base(topspeed, color) { }

        public override float Drive(int finish)
        {
            Random rand = new Random();
            Thread.Sleep(1);
            int randSpeed = rand.Next(100, TopSpeed);
            return finish / randSpeed;
        }   
    }

    class Sedan : Car
    {
        public Sedan(int topspeed, string color) : base(topspeed, color) { }

        public override float Drive(int finish)
        {
            Random rand = new Random();
            Thread.Sleep(1);
            int randSpeed = rand.Next(50, TopSpeed);
            return finish / randSpeed;
        }
    }

    class Bus : Car
    {
        public Bus(int topspeed, string color) : base(topspeed, color) { }

        public override float Drive(int finish)
        {
            Random rand = new Random();
            Thread.Sleep(1);
            int randSpeed = rand.Next(20, TopSpeed);
            return finish / randSpeed;
        }
    }

    //task1
    /*
    class Program
    {
        static void Main(string[] args)
        {
            Car.GetWinner(new Bus(300, "blue"), new SportCar(240, "red"), 1000);
        }
    }
    */

    class Game
    {
        public Player p1 { get; set; }
        public Player p2 { get; set; }
        public List<Card> deck { get; set; }

        public Game(string Name1, string Name2)
        {
            InitializeDeck();
            MixDeck();
            MixDeck();
            InitializePlayers(Name1, Name2);
            Console.WriteLine("Game is ready!");
        }

        private void InitializeDeck()
        {
            deck = new List<Card>();
            for (int i = 6; i <= 10; i++){
            deck.Add(new Card("♣", new Tuple<int, string>(i, i.ToString())));
            deck.Add(new Card("♦", new Tuple<int, string>(i, i.ToString())));
            deck.Add(new Card("♥", new Tuple<int, string>(i, i.ToString())));
            deck.Add(new Card("♠", new Tuple<int, string>(i, i.ToString())));}
            deck.Add(new Card("♣", new Tuple<int, string>(11, "Jack")));
            deck.Add(new Card("♦", new Tuple<int, string>(11, "Jack")));
            deck.Add(new Card("♥", new Tuple<int, string>(11, "Jack")));
            deck.Add(new Card("♠", new Tuple<int, string>(11, "Jack")));
            deck.Add(new Card("♣", new Tuple<int, string>(12, "Queen")));
            deck.Add(new Card("♦", new Tuple<int, string>(12, "Queen")));
            deck.Add(new Card("♥", new Tuple<int, string>(12, "Queen")));
            deck.Add(new Card("♠", new Tuple<int, string>(12, "Queen")));
            deck.Add(new Card("♣", new Tuple<int, string>(13, "King")));
            deck.Add(new Card("♦", new Tuple<int, string>(13, "King")));
            deck.Add(new Card("♥", new Tuple<int, string>(13, "King")));
            deck.Add(new Card("♠", new Tuple<int, string>(13, "King")));
            deck.Add(new Card("♣", new Tuple<int, string>(14, "Ace")));
            deck.Add(new Card("♦", new Tuple<int, string>(14, "Ace")));
            deck.Add(new Card("♥", new Tuple<int, string>(14, "Ace")));
            deck.Add(new Card("♠", new Tuple<int, string>(14, "Ace")));
        }
        private void InitializePlayers(string Name1, string Name2)
        {
            List<Card> tmphand1 = new List<Card>();
            for (int i = 0; i < 18; i++)
            {
                tmphand1.Add(deck[0]);
                deck.RemoveAt(0);
            }
            p1 = new Player(Name1, tmphand1);
            List<Card> tmphand2 = new List<Card>();
            for (int i = 0; i < 18; i++)
            {
                tmphand2.Add(deck[0]);
                deck.RemoveAt(0);
            }
            p2 = new Player(Name2, tmphand2);
        }
        private void MixDeck()
        {
            Random rand = new Random();
            int ind1;
            int ind2;
            for (int i = 0; i < 50; i++)
            {
                Thread.Sleep(1);
                ind1 = rand.Next(36);
                Thread.Sleep(1);
                ind2 = rand.Next(36);
                Card tmp = deck[ind1];
                deck[ind1] = deck[ind2];
                deck[ind2] = tmp;
            }
        }

        public void Start()
        {
            Console.WriteLine("-------START-------");
            while(p1.Hand.Count!=0 && p2.Hand.Count != 0)
            {
                Console.Write("Player1: "); p1.Hand[0].PrintCard();
                Console.Write("Player2: "); p2.Hand[0].PrintCard();
                if (p1.Hand[0].Value.Item1 > p2.Hand[0].Value.Item1)
                {
                    Console.WriteLine("Player1 wins!");
                    p1.Hand.Add(p2.Hand[0]);
                    p2.Hand.RemoveAt(0);
                    p1.Hand.Add(p1.Hand[0]);
                    p1.Hand.RemoveAt(0);
                }
                else if (p1.Hand[0].Value.Item1 == p2.Hand[0].Value.Item1)
                {
                    Random rand = new Random();
                    if (rand.Next(10) % 2 == 0)
                    {
                        Console.WriteLine("Player1 wins!");
                        p1.Hand.Add(p2.Hand[0]);
                        p2.Hand.RemoveAt(0);
                        p1.Hand.Add(p1.Hand[0]);
                        p1.Hand.RemoveAt(0);
                    }
                    else
                    {
                        Console.WriteLine("Player2 wins!");
                        p2.Hand.Add(p1.Hand[0]);
                        p1.Hand.RemoveAt(0);
                        p2.Hand.Add(p2.Hand[0]);
                        p2.Hand.RemoveAt(0);
                    }
                }
                else
                {
                    Console.WriteLine("Player2 wins!");
                    p2.Hand.Add(p1.Hand[0]);
                    p1.Hand.RemoveAt(0);
                    p2.Hand.Add(p2.Hand[0]);
                    p2.Hand.RemoveAt(0);
                }
                Console.WriteLine($"Cards Player1: {p1.Hand.Count}");
                Console.WriteLine($"Cards Player2: {p2.Hand.Count}");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }
            if (p1.Hand.Count == 0) Console.WriteLine("The final WINNER is Player2!!!");
            else Console.WriteLine("The final WINNER is Player1!!!");
            Console.WriteLine("--------END--------");
        }

        public void PrintDeck()
        {
            foreach (var item in deck)
            {
                item.PrintCard();
            }
        }
    }

    class Player
    {
        public Player(string name, List<Card> hand)
        {
            Name = name;
            Hand = hand;
        }

        public string Name { get; set; }
        public List<Card> Hand { get; set; }

        public void PrintHand()
        {
            foreach (var item in Hand)
            {
                item.PrintCard();
            }
        }
    }

    class Card
    {
        public Card(string suit, Tuple<int, string> value)
        {
            Suit = suit;
            Value = value;
        }

        public void PrintCard()
        {
            Console.WriteLine($"{Suit} {Value.Item2}");
        }

        public string Suit { get; set; }
        public Tuple<int, string> Value { get; set; }
    }

    //task2
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game("Javid", "Vice");
            game.Start();
        }
    }
}
