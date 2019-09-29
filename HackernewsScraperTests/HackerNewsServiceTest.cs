using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HackernewsScraper;
using System.Collections.Generic;

namespace HackernewsScraperTests
{
    [TestClass]
    public class HackerNewsServiceTest
    {
        HttpClientCaller hcc;
        HackerNewsService hns;

        [TestInitialize]
        public void Init()
        {
            hcc = new HttpClientCaller();
            hns = new HackerNewsService(hcc);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetTopPostsIdsShouldReturn9TopPostsIds()
        {
            var topPostsIds = await hns.GetTopPostsIds(9);
            Assert.IsNotNull(topPostsIds);
            Assert.AreEqual(topPostsIds.Count, 9);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetTopPostsShouldReturnTopPosts()
        {
            var topPostsIds = await hns.GetTopPostsIds(2);
            Assert.IsNotNull(topPostsIds);
            Assert.AreEqual(topPostsIds.Count, 2);
            var topPosts = await hns.GetTopPosts(topPostsIds);
            Assert.IsNotNull(topPosts);
            Assert.AreEqual(topPosts.Count, 2);
            Assert.IsFalse(string.IsNullOrEmpty(topPosts[0].Title));
        }

        [TestMethod]
        public async System.Threading.Tasks.Task CheckPostInfoShouldReturnTrue()
        {
            HackernewsScraper.Model.Post post = new HackernewsScraper.Model.Post()
            {
                Kids = new List<int>() { 10, 2, 3 },
                Score = 50,
                Url = "bidulle",
                Title = "Test",
                By = "Truc"
            };
            Assert.IsTrue(hns.CheckPostInfos(post));
        }

        [TestMethod]
        public async System.Threading.Tasks.Task CheckPostInfosWithTitleEmptyShouldReturnFalse()
        {
            HackernewsScraper.Model.Post post = new HackernewsScraper.Model.Post()
            {
                By = "toto",
                Kids = new List<int>() { 10, 2, 3 },
                Score = 60,
                Url = "bidulle"
            };
            Assert.IsFalse(hns.CheckPostInfos(post));
        }

        [TestMethod]
        public async System.Threading.Tasks.Task CheckPostInfosWithAuthorEmptyShouldReturnFalse()
        {
            HackernewsScraper.Model.Post post = new HackernewsScraper.Model.Post()
            {
                Kids = new List<int>() { 10, 2, 3 },
                Score = 60,
                Url = "bidulle",
                Title = "Test"
            };
            Assert.IsFalse(hns.CheckPostInfos(post));
        }

        [TestMethod]
        public async System.Threading.Tasks.Task CheckPostInfosWithTitleMoreThan256ShouldReturnFalse()
        {
            HackernewsScraper.Model.Post post = new HackernewsScraper.Model.Post()
            {
                By = "toto",
                Kids = new List<int>() { 10, 2, 3 },
                Score = 60,
                Url = "bidulle",
                Title = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
            };
            Assert.IsTrue(post.Title.Length > 256);
            Assert.IsFalse(hns.CheckPostInfos(post));
        }

        [TestMethod]
        public async System.Threading.Tasks.Task CheckPostInfosWithAuthorMoreThan256ShouldReturnFalse()
        {
            HackernewsScraper.Model.Post post = new HackernewsScraper.Model.Post()
            {
                Kids = new List<int>() { 10, 2, 3 },
                Score = 60,
                Url = "bidulle",
                By = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                Title = "Test"
            };
            Assert.IsTrue(post.By.Length > 256);
            Assert.IsFalse(hns.CheckPostInfos(post));
        }

        [TestMethod]
        public async System.Threading.Tasks.Task CheckPostInfosWithPointsLessThan0ShouldReturnFalse()
        {
            HackernewsScraper.Model.Post post = new HackernewsScraper.Model.Post()
            {
                Kids = new List<int>() { 10, 2, 3 },
                Score = -1,
                Url = "bidulle",
                Title = "Test",
                By = "Truc"
            };
            Assert.IsFalse(hns.CheckPostInfos(post));
        }
    }
}
