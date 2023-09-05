using System;
using System.IO;

namespace SleepData
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            // path for data file
            string file = AppDomain.CurrentDomain.BaseDirectory + "data.txt";

            if (resp == "1")
            {
                // create data file

                // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine());

                // determine start end date
                DateTime today = DateTime.Now;
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek); // we want full weeks Sunday - Saturday
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7)); // subtract # of weeks from endDate to get startDate

                Random rnd = new Random();

                using (StreamWriter sw = new StreamWriter(file)) 
                {
                    while (dataDate < dataEndDate)
                    {
                        int[] hours = new int[7];
                        for (int i = 0; i < hours.Length; i++)
                        {
                            hours[i] = rnd.Next(4, 13); 
                        }

                        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");

                        dataDate = dataDate.AddDays(7);
                    }
                }
            }
            else if (resp == "2")
            {
                if (File.Exists(file))
                {
                    using (StreamReader sr = new StreamReader(file)) 
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();

                            string[] week = line.Split(',');
                            DateTime date = DateTime.Parse(week[0]);
                            int[] hours = Array.ConvertAll(week[1].Split('|'), int.Parse);

                            int total = 0;
                            for (int i = 0; i < hours.Length; i++)
                            {
                                total += hours[i];
                            }
                            double average = (double)total / hours.Length;

                            Console.WriteLine($"Week of {date:MMM}, {date:dd}, {date:yyyy}");
                            Console.WriteLine($"{"Su.",4}{"Mo.",4}{"Tu.",4}{"We.",4}{"Th.",4}{"Fr.",4}{"Sa.",4}{"Tot",4}{"Avg",4}");
                            Console.WriteLine($"{"--",4}{"--",4}{"--",4}{"--",4}{"--",4}{"--",4}{"--",4}{"---",4}{"---",4}");
                            Console.WriteLine($"{hours[0],4}{hours[1],4}{hours[2],4}{hours[3],4}{hours[4],4}{hours[5],4}{hours[6],4}{total,4}{average,4:F1}");
                            Console.WriteLine();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("File Error");
                }
            }
        }
    }
}
