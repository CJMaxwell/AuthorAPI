using DecagonAuthorTest.Models.Response;
using DecagonAuthorTest.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecagonAuthorTest
{
    class Program
    {
        public static UsernameResponse MakeApiCall()
        {


            var api = new RestActionHelper();
            var baseurl = "https://jsonmock.hackerrank.com/api/article_users/search?page=";
            var url = baseurl;

            var response = api.CallGetAction<UsernameResponse>(url);

            return response;
        }

        public static List<string> GetUsernames(int threshold)
        {
            var usernames = new List<string>();

            var response = MakeApiCall();

            if (response != null)
            {
                usernames = response.Data.Where(x => x.SubmissionCount >= threshold).Select(s => s.Username).ToList();
            }

            return usernames;
        }

        public static string GetUsernameWithHighestCommentCount()
        {
            var username = string.Empty;

            var response = MakeApiCall();

            if (response != null)
            {
                var maxCommentCount = response.Data.Max(x => x.CommentCount);
                username = response.Data.Where(x => x.CommentCount == maxCommentCount)
                                        .Select(s => s.Username).FirstOrDefault();
            }

            return username;
        }

        public static List<string> GetUsernamesSortedByRecordDate(int threshold)
        {
            var usernames = new List<string>();

            var response = MakeApiCall();

            if (response != null)
            {
                usernames = response.Data.Where(x => x.CreatedAt >= threshold).Select(s => s.Username).ToList();
            }

            return usernames;
        }

        static void Main(string[] args)
        {
           var res = GetUsernameWithHighestCommentCount();
            Console.WriteLine(res);
        }
    }
}
