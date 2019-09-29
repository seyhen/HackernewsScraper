using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackernewsScraper
{
    public class HackerNewsService
    {
        private HttpClientCaller HttpClientCaller;
        public HackerNewsService(HttpClientCaller httpClientCaller)
        {
            HttpClientCaller = httpClientCaller;
        }

        /// <summary>
        /// Get top posts from hacker news by ids
        /// </summary>
        /// <param name="topPostIds">List of top post ids</param>
        /// <returns>Task of list of hacker news posts</returns>
        public async Task<IList<ViewModel.PostVM>> GetTopPosts(IList<int> topPostIds)
        {
            List<ViewModel.PostVM> topPosts = new List<ViewModel.PostVM>();
            int index = 1;
            foreach (var topPostId in topPostIds)
            {
                var res = await HttpClientCaller.GetResponse<Model.Post>($"https://hacker-news.firebaseio.com/v0/item/{topPostId}.json?print=pretty");
                if (CheckPostInfos(res))
                {
                    topPosts.Add(new ViewModel.PostVM(res, index));
                }
                index++;
            }

            return topPosts;
        }

        /// <summary>
        /// Check if the post informations are valid
        /// </summary>
        /// <param name="post">A post</param>
        /// <returns>Boolean check if the post informations are all valid</returns>
        public bool CheckPostInfos(Model.Post post)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(post.By) || string.IsNullOrEmpty(post.Title))
            {
                isValid = false;
            }
            else
            {
                if (post.By.Length > 256 || post.Title.Length > 256)
                {
                    isValid = false;
                }
            }
            if (post.Score < 0)
                isValid = false;

            return isValid;
        }

        /// <summary>
        /// Get top posts ids from hacker news limited by postCount
        /// </summary>
        /// <param name="postCount">Number of posts to retrieve</param>
        /// <returns>Task of list of hacker news ids</returns>
        public async Task<IList<int>> GetTopPostsIds(int postCount)
        {
            IList<int> topPostsIds = null;
            topPostsIds = await HttpClientCaller.GetResponse<IList<int>>("https://hacker-news.firebaseio.com/v0/topstories.json");
            topPostsIds = topPostsIds.Take(postCount).ToList();

            return topPostsIds;
        }
    }
}
