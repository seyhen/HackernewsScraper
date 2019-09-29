using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackernewsScraper.ViewModel
{
    /// <summary>
    /// View Model of an hacker news post
    /// </summary>
    public class PostVM
    {
        ///
        public string Title { get; set; }
        public string Uri { get; set; }
        public string Author { get; set; }
        public int Points { get; set; }
        public int Comments { get; set; }
        public int Rank { get; set; }

        public PostVM()
        {

        }

        public PostVM(Model.Post post, int rank)
        {
            Title = post.Title;
            Uri = post.Url;
            Author = post.By;
            Points = post.Score;
            Comments = post.Kids.Count();
            Rank = rank;
        }
    }
}
