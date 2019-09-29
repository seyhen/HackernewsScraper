using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HackernewsScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                if (args[0] == "--posts")
                {
                    int postCount;
                    if (int.TryParse(args[1], out postCount))
                    {
                        if (postCount > 0 && postCount <= 100)
                        {
                            BeginTasks(postCount);
                        }
                        else
                        {
                            Console.WriteLine("n is not between 1 and 100");
                        }
                    }
                    else
                    {
                        Console.WriteLine("n is not a number");
                    }
                }
                else
                {
                    Console.WriteLine("The first argument is invalid");
                    Console.WriteLine("Usage: hackernews --posts n with  0 < n <= 100");
                }
            }
            else
            {
                Console.WriteLine("Usage: hackernews --posts n with  0 < n <= 100");
            }

            Console.ReadLine();
        }

        public static async Task BeginTasks(int postCount)
        {
            HttpClientCaller hcc = new HttpClientCaller();
            HackerNewsService hackerNewsService = new HackerNewsService(hcc);

            var topPostsIds = await hackerNewsService.GetTopPostsIds(postCount);
            var topPosts = await hackerNewsService.GetTopPosts(topPostsIds);

            var jsonTopPosts = new JavaScriptSerializer().Serialize(topPosts);
            Console.WriteLine(jsonTopPosts);
        }
    }
}
