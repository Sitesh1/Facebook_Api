using Facebook;
using InstaSharp;
using System;
using RestSharp;
using System.Web;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FacebookAds;
using System.IO;
using FacebookAds.Object;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Crmf;
using System.Net.Http.Headers;
using Org.BouncyCastle.Cms;


using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using System.Threading;
using System.Diagnostics;
using Google.Apis.Upload;
using Org.BouncyCastle.Crypto.Tls;
using static System.Net.Mime.MediaTypeNames;
using Google.Apis.Drive.v3.Data;

namespace Facebook_Integration
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            Program.SaveFile();
          
            string accessToken = "";  //your access token
            //GetFbCommentList();
            //CommentOnPost();
            //CountFbFollowers();
            //CommentCount();
            //LikesCountofPost(accessToken);
            //InstaLikesCount();
            //FacebookLikesCount(accessToken);
            //FbShareList(accessToken);
            //FbCommentList();   //it working fine to get all comment and pick one random.
            //FbCommentListByPostId();
            //FbMessageList(accessToken);
            //FbMessageReply(accessToken);
            //FbReply(accessToken);
            //FacebookSharerName(accessToken);
            //LoginWithFacebook();
            //GetPostId(accessToken);
        }
        public static void PostOnFacebook()
        {
            try
            {
                //dynamic result = fbClient.Post("me/feed", parameters);
                //var postId = result.id; // Retrieve the ID of the newly created post
                //Console.WriteLine("Post shared successfully. Post ID: " + postId);
                //Console.ReadLine();

                var client = new FacebookClient(""); //your access token
                var parameters = new Dictionary<string, object>
                {
                   { "message", "Hallo I am Sitesh" },
                   { "link", "https://example.com" },  // Optional: include a link
                   // Other optional parameters can be added here, such as "picture", "description", etc.
                };

                dynamic result = client.Post("me/feed", parameters);
                string postId = result.id;

            }
            catch (FacebookOAuthException ex)
            {
                // Handle exception
                Console.WriteLine("Error sharing post: " + ex.Message);
                Console.ReadLine();
            }
        }
        public static void GetFbCommentList()
        {
            try
            {
                string folderPath = @"D:\FacebookIntegration\";
                Directory.CreateDirectory(folderPath);
                string commentMessage = "";
                //Access Token canbe expired so please enter valid token after regenerate.
                var client = new FacebookClient("");  //your access token
                string pageId = "102847089502067"; //3536027056616903 ,//102847089502067
                dynamic posts = client.Get($"{pageId}/posts");

                List<string> searchResults = new List<string>();
                int count = 0;
                foreach (dynamic post in posts.data)
                {
                    string postId = post.id;
                    dynamic comments = client.Get($"{postId}/comments");

                    foreach (dynamic comment in comments.data)
                    {
                        count++;
                        string commentId = comment.id;
                        commentMessage = count + " " + comment.message;
                        searchResults.Add(commentMessage);
                        // Access other properties as needed
                        Console.WriteLine("Comments:- " + commentMessage);
                    }

                    using (StreamWriter writer = new StreamWriter(folderPath + "" + "comment_list.txt"))
                    {
                        foreach (string result in searchResults)
                        {
                            writer.WriteLine(result);
                        }
                    }
                }

                Console.WriteLine("Comments:- "+ commentMessage);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
        public static void CommentOnPost()
        {
            string postId= "118313477940943";
            string pageId = "102847089502067";
            string accessToken= "";  //your access token
            string message = "Thank You So Much";

            try
            {
                var client = new FacebookClient(accessToken);
                var parameters = new Dictionary<string, object>
                    {
                         { "message", "hi! this is my status message" },
                         { "place",postId}
                    };
                client.Post("me/feed", parameters);
                var fbClient = new FacebookClient();


                fbClient.AccessToken = accessToken;
                fbClient.Post($"/{postId}/comments", parameters);

                dynamic result1 = client.Post($"{postId}/comments", parameters);
                Console.WriteLine("The comment has been posted successfully.");
                Console.ReadLine();
            }
            catch (FacebookApiException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            

        }
        public static void CountFbFollowers()
        {
            try
            {
                var client = new FacebookClient("");  //your access token
                var parameters = new Dictionary<string, object>
            {
               { "fields", "fan_count" } // Retrieve only the fan_count field (follower count)
            };

                var result = client.Get("102847089502067", parameters) as JsonObject;//insid:- 17841460125021255 ,fbid:- 102847089502067
                int followerCount = Convert.ToInt32(result["fan_count"]);
                Console.WriteLine("Your follower count is:-" + followerCount);
                Console.ReadLine();
            }
            catch (FacebookApiException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
           
        }
        public static void InstaCountFollowers()
        {
            try
            {
                var client = new FacebookClient("");  //your access token
                var parameters = new Dictionary<string, object>
            {
               { "fields", "followers_count" } // Retrieve only the fan_count field (follower count) use (follow_count) for follow by you count get.
            };

                var result = client.Get("17841460125021255", parameters) as JsonObject;//insid:- 17841460125021255 ,fbid:- 102847089502067
                int followerCount = Convert.ToInt32(result["followers_count"]);
                Console.WriteLine("Your Insta follower count is:-" + followerCount);
                Console.ReadLine();
            }
            catch (FacebookApiException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }
        public static void InstaLikesCount()
        {
            try
            {
                var accessToken = "";  //your access token
                var postId = "384262082655927";

                var fb = new FacebookClient(accessToken);

                var parameters = new Dictionary<string, object>
                {
                   { "fields", "like_count" }
                };

                dynamic result = fb.Get(postId, parameters);

                int likeCount = result.like_count;

                Console.WriteLine($"Likes count: {likeCount}");

                Console.ReadLine();

            }
            catch (FacebookApiException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }
        public static void FacebookLikesCount(string accessToken)
        {
            try
            {
                //var accessToken = ""; //your access token
               
                // Replace with the post ID of the Facebook page post
                string postId = "102847089502067_118289507943340";

                // Create a new FacebookClient with the access token
                var client = new FacebookClient(accessToken);

                // Make a GET request to retrieve the likes of the post
                dynamic result = client.Get($"{postId}/likes");

                // Extract the data from the response
                var data = result.data;

                // Iterate through the like list
                foreach (var item in data)
                {
                    var id = item.id;
                    var name = item.name;
                    Console.WriteLine($"ID: {id}, Name: {name}");
                }

                Console.ReadLine();

            }
            catch (FacebookApiException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }
        public static void CommentCount()
        {
            try
            {
                var client = new FacebookClient(""); //your access token
                var postId = "118289507943340"; // Replace with the actual post ID
                dynamic result = client.Get($"{postId}/comments");
                dynamic comments = result.data;
                int commentCount = comments.Count;
                Console.WriteLine("Your Comment count of this post is:-" + commentCount);
                Console.ReadLine();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

            
        }
        public static void LikesCountofPost(string accessToken)
        {
            try
            {
                string postId = "102847089502067_128385860267038";
                //string accessToken = "YOUR_FACEBOOK_ACCESS_TOKEN";

                string apiUrl = $"https://graph.facebook.com/v14.0/{postId}?fields=likes&access_token={accessToken}";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                    bool hasNextPage = true;
                    int count = 0;
                    string nextPageUrl = apiUrl;
                    Console.WriteLine("Likers:");
                    while (hasNextPage)
                    {
                        count++;    
                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = response.Content.ReadAsStringAsync().Result;
                            dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);
                            var likes = data.likes.data;
                            foreach (var like in likes)
                            {
                                string likerid = like.id;
                                string likerName = like.name;
                                Console.WriteLine("Id: "+likerid);
                                Console.WriteLine("Name: "+likerName);
                            }
                            if (data.likes.paging != null && data.likes.paging.next != null)
                            {
                                nextPageUrl = data.likes.paging.next;
                            }
                            else
                            {
                                hasNextPage = false;
                            }
                          
                        }
                        else
                        {
                            Console.WriteLine($"Failed to retrieve likes. Status code: {response.StatusCode}");
                            Console.ReadLine();
                            return;
                        }
                        Console.WriteLine("Total likes: "+count);
                        Console.ReadLine();

                    }
                }

                Console.ReadLine();
                // string url = $"https://graph.facebook.com/v12.0/{id}?fields=posts{{likes{{count}}}}&access_token={accessToken}";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }


        }
        //Working
        public static void FbShareList(string accessToken)
        {
            try
            {
                string folderPath = @"D:\FacebookIntegration\";
                Directory.CreateDirectory(folderPath);
                string pageId = "102847089502067";
               // string accessToken = "";  //your access token

                var fb = new FacebookClient(accessToken);

                var parameters = new Dictionary<string, object>

                {
                     { "fields", "sharedposts" }
                };

                dynamic result = fb.Get($"{pageId}/posts", parameters);

               // List<string> searchResults = new List<string>();
                Dictionary<Int32,string> searchResults = new Dictionary<Int32, string>();

                if (result != null)
                {
                    if (result.data != null)
                    {
                        int count = 0;
                        foreach (var post in result.data)
                        {
                            if (post.sharedposts != null)
                            {
                               
                                foreach (var sharedPost in post.sharedposts.data)
                                {
                                    count++;
                                    var sharedPostId = sharedPost.id;
                                    var sharedPostMessage = sharedPost.message;
                                    var sharerName = FacebookSharerName(accessToken);
                                    searchResults.Add(count, "Id:-" + sharedPostId+ " Message:-" + sharedPostMessage +" Shared By:-"+sharerName);
                                    // searchResults.Add("Message:-"+sharedPostMessage);
                                    //Console.WriteLine(searchResults);
                                    //Console.WriteLine($"Shared Post Message: {sharedPostMessage}");
                                }
                            }
                            using (StreamWriter writer = new StreamWriter(folderPath + "" + "shared_list.txt"))
                            {
                                foreach (var result1 in searchResults)
                                {
                                    writer.WriteLine(result1);
                                }
                            }
                        }
                        Console.WriteLine("Your total count of sharepost is=" + searchResults.Count()) ;
                        Console.WriteLine("Please enter random shared number");
                        var chk=Console.ReadLine();
                        if (searchResults.ContainsKey(Convert.ToInt32(chk)))
                        {
                            foreach (var result2 in searchResults)
                            {
                                if (result2.Key == Convert.ToInt32(chk))
                                {
                                    Console.WriteLine("Match found! Value: " + result2.Value);
                                }
                            }
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Key not found in the dictionary.");
                        }

                        Console.ReadLine();

                    }
                }
                else
                {
                    Console.WriteLine("Unable to retrieve shared posts.");
                    Console.ReadLine();
                }

               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }

        }
        //Working
        public static void FbCommentList()
        {
           
            try
            {
                string folderPath = @"D:\FacebookIntegration\";
                Directory.CreateDirectory(folderPath);
                string commentname = "";
                string commentMessage = "";
                var client = new FacebookClient("");  //your access token
                string pageId = "102847089502067";
                string PostId = "";  //102847089502067_123996710705953
                dynamic posts = client.Get($"{pageId}/posts");

                Dictionary<Int32,string> searchResults = new Dictionary<Int32, string>();
               // Dictionary<Int32,string> searchResults = new Dictionary<Int32, string>();
                int count = 0;
                foreach (dynamic post in posts.data)
                {
                    string postId = post.id;
                    if (!string.IsNullOrEmpty(PostId))
                    {
                        if (postId == PostId)
                        {
                            dynamic comments = client.Get($"{postId}/comments");

                            while (true)
                            {
                                foreach (dynamic comment in comments.data)
                                {
                                    count++;
                                    commentname = comment.from.name;
                                    commentMessage = comment.message + " name:-" + commentname;
                                    //searchResults.Add(count,commentname);
                                    searchResults.Add(count, commentMessage);
                                    //  Console.WriteLine("Comments: " + comment);
                                }

                                if (comments.paging == null || comments.paging.next == null)
                                {
                                    break;
                                }

                                comments = client.Get(comments.paging.next);
                            }
                        }
                    }
                    else
                    {
                        dynamic comments = client.Get($"{postId}/comments");

                        while (true)
                        {
                            foreach (dynamic comment in comments.data)
                            {
                                count++;
                                commentname = comment.from.name;
                                commentMessage = comment.message + " name:-" + commentname;
                                //searchResults.Add(commentMessage);
                                searchResults.Add(count, commentMessage);
                                //  Console.WriteLine("Comments: " + comment);
                            }

                            if (comments.paging == null || comments.paging.next == null)
                            {
                                break;
                            }

                            comments = client.Get(comments.paging.next);
                        }
                    }
                   
                }

                using (StreamWriter writer = new StreamWriter(folderPath + "comment_list1.txt"))
                {
                    foreach (var result in searchResults)
                    {
                        writer.WriteLine(result);
                    }
                }

                Console.WriteLine("Total Comments: " + searchResults.Count);
                Console.WriteLine("Please your random no of comments");
                var sub = Console.ReadLine();

                if (int.TryParse(sub, out int randomNoOfComments))
                {
                    var randomComments = searchResults.Where(x => x.Key==randomNoOfComments).ToList();

                    Console.WriteLine("Your random comments is:");
                    foreach (var comment in randomComments)
                    {
                        Console.WriteLine(comment);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }
        public static void FbMessageList(string accessToken)
        {
            try
            {
                //string folderPath = @"D:\FacebookIntegration\";
                //Directory.CreateDirectory(folderPath);
                 string pageId = "102847089502067";

                var client = new FacebookClient(accessToken);

                // Specify the desired fields and subfields
                string fields = "conversations{messages{message}}";

                // Make the API request
                dynamic result = client.Get($"{pageId}?fields={fields}");

                // Extract conversations and their messages
                dynamic conversations = result["conversations"];
                var paging = conversations["paging"];
                while (true)
                {

                    foreach (dynamic conversation in conversations["data"])
                    {
                        dynamic messages = conversation["messages"];
                        foreach (dynamic message in messages["data"])
                        {
                            // string senderId = message["id"];  // ID of the sender
                            string senderName = message["from"]["name"];  // Name of the sender
                            string messageText = message["message"];  // Text of the message

                            // Process the message as needed
                            //Console.WriteLine($"Id: {senderId}");
                            //Console.WriteLine($"Sender: {senderName}");
                            Console.WriteLine($"Message: {messageText}");
                            Console.WriteLine();
                        }
                    }
                    if (paging.ContainsKey("next"))
                    {
                        conversations = client.Get(paging["next"].ToString());
                        paging = conversations["paging"];
                    }
                    else
                    {
                        break;
                    }
                }

                Console.ReadLine();
           
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }          
        }

        //Working
        public static void FbMessageReply(string accessToken)
        {
            try
            {    
                string pageId = "102847089502067";
                var client = new FacebookClient(accessToken);
                string fields = "conversations{messages{message,from}}";
                dynamic result = client.Get($"{pageId}?fields={fields}");
                dynamic conversations = result["conversations"];
                var paging = conversations["paging"];

                while (true)
                {
                    foreach (dynamic conversation in conversations["data"])
                    {
                        dynamic messages = conversation["messages"];
                        string conversationId = conversation["id"]; // Get the conversation ID
                        string conversationIdPrefix = "t_";

                        // Check if the conversation ID has the expected prefix
                        if (!conversationId.StartsWith(conversationIdPrefix))
                        {
                            Console.WriteLine($"Invalid conversation ID format: {conversationId}");
                            continue;
                        }

                        foreach (dynamic message in messages["data"])
                        {
                            string senderName = message["from"]["name"];
                            string fromid = message["from"]["id"];
                            string messageText = message["message"];
                            
                            Console.WriteLine($"Sender: {senderName}");
                            Console.WriteLine($"senderid: {fromid}");
                            Console.WriteLine($"Message: {messageText}");
                            Console.WriteLine();
                        }

                    }

                    if (paging.ContainsKey("next"))
                    {
                        conversations = client.Get(paging["next"].ToString());
                        paging = conversations["paging"];
                    }
                    else
                    {
                        break;
                    }
                }

            
                //Reply Messages with recipient id
                Console.Write("Enter the message: ");
                string messageText1 = Console.ReadLine();

                var recipientId = "6266613243428129";
                string url = $"https://graph.facebook.com/v13.0/{pageId}/messages?access_token={accessToken}";

                string jsonPayload = $@"
        {{
            ""recipient"": {{
                ""id"": ""{recipientId}""
            }},
            ""message"": {{
                ""text"": ""{messageText1}""
            }},
            ""messaging_type"": ""RESPONSE""
        }}";

                using (HttpClient client1 = new HttpClient())
                {
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client1.PostAsync(url, content).GetAwaiter().GetResult();
                    string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Message sent successfully!");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Failed to send message. Response:");
                        Console.WriteLine(responseContent);
                        Console.ReadLine();
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            Console.ReadLine();


        }
        public static void InstaCommentText(string accesstoken)
        {
            try
            {
                string folderPath = @"D:\Divya\demo\Demo\InstagramIntegrated\";
                Directory.CreateDirectory(folderPath);
                string commentMessage = "";
                var accessToken = "";  //your access token
                var postId = "17968289699274524";

                var fb = new FacebookClient(accessToken);
                List<string> searchResults = new List<string>();
                var parameters = new Dictionary<string, object>
                {
                    { "fields", "comments{text}" } // Include the "comments{message}" field to retrieve the comment text
                };

                dynamic comments = fb.Get(postId, parameters);

                foreach (dynamic item in comments.comments.data)
                {
                    commentMessage = item.text;
                    searchResults.Add(commentMessage);
                    Console.WriteLine("Comments of Instagram :" + commentMessage);
                }

                using (StreamWriter writer = new StreamWriter(folderPath + "" + "ListofComment.txt"))
                {
                    foreach (string result in searchResults)
                    {
                        writer.WriteLine(result);
                    }
                }
                Console.WriteLine("Comments of Instagram :" + commentMessage);
                Console.ReadLine();
            }
            catch (FacebookApiException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        public static string FacebookSharerName(string accesstoken)
        {
            string resp = "";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string pageId = "102847089502067";
                   
                  
                    string url = $"https://graph.facebook.com/v12.0/{pageId}?fields=feed%7Bsharedposts%7Bfrom%2Cname%7D%7D&access_token={accesstoken}";

                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = response.Content.ReadAsStringAsync().Result;

                        JObject json = JObject.Parse(jsonResponse);

                        // Check if feed and sharedposts exist
                        if (json["feed"] != null && json["feed"]["data"] != null)
                        {
                            // Extract data array
                            JArray data = (JArray)json["feed"]["data"];

                            // Iterate through each entry in the data array
                            foreach (JToken entry in data)
                            {
                                // Check if sharedposts field exists
                                if (entry["sharedposts"] != null)
                                {
                                   
                                    // Extract sharedposts array
                                    JArray sharedPosts = (JArray)entry["sharedposts"]["data"];
                                   
                                    // Iterate through each shared post
                                    foreach (JToken post in sharedPosts)
                                    {
                                  
                                        // Check if from and name fields exist
                                        if (post["from"] != null && post["from"]["name"] != null)
                                        {
                                            string sharerName = (string)post["from"]["name"];
                                            //string message = (string)post["message"];
                                            //string message = (post["message"] != null) ? (string)post["message"] : "No Message Available";
                                            //string postId = (string)post["id"];

                                            //Console.WriteLine($"Sharer Name: {sharerName}");
                                            resp = sharerName;
                                            // Console.WriteLine($"Message: {message}");
                                            //Console.WriteLine($"Post ID: {postId}");
                                        }
                                    }
                                   
                                }
                            }
                          return resp;
                        }
                        else
                        {
                            //Console.WriteLine("Feed or sharedposts field not found in the response.");
                            //Console.ReadLine();
                            return resp;
                        }
                    }
                    else
                    {
                       Console.WriteLine($"Error: {response.StatusCode}");
                       resp= Console.ReadLine();
                        return resp;
                    }
                }

            }
            catch (FacebookApiException ex)
            {
                Console.WriteLine(ex.Message);
                resp=Console.ReadLine();
                return resp;
            }
        }

        private static readonly HttpClient client = new HttpClient();
        private static object fileName;

        //Working
        public static void FbReply(string accessToken)
        {
            try
            {
                string pageId = "102847089502067";
                //string accessToken = "YOUR_ACCESS_TOKEN";
                string recipientId = "6266613243428129"; //"6342358575855164";
                Console.Write("Enter the message: ");
                string messageText = Console.ReadLine();

                string url = $"https://graph.facebook.com/v13.0/{pageId}/messages?access_token={accessToken}";

                string jsonPayload = $@"
                {{
                    ""recipient"": {{
                        ""id"": ""{recipientId}""
                    }},
                    ""message"": {{
                        ""text"": ""{messageText}""
                    }},
                    ""messaging_type"": ""RESPONSE""
                }}";

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(url, content).GetAwaiter().GetResult();
                    string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Message sent successfully!");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Failed to send message. Response:");
                        Console.WriteLine(responseContent);
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        const string AppId = "1474401586645973";
        const string AppSecret = "7cc1c7225a3a75fb14d2fe194626e260";
        // Replace with your callback URL
        const string RedirectUri = "https://localhost:44340/";

        public static string CredentialsFilePath { get; private set; }
        public static string ApplicationName { get; private set; }
        public static object FolderId { get; private set; }
        public static IEnumerable<string> Scopes { get; private set; }

        public static void LoginWithFacebook()
        {
            try
            {
                Console.WriteLine("Facebook Login Example");

                var fb = new FacebookClient();

                var loginUrl = fb.GetLoginUrl(new
                {
                    client_id = AppId,
                    redirect_uri = RedirectUri,
                    response_type = "code",
                    scope = "email" // Add any additional permissions you need
                });

                Console.WriteLine($"Open the following URL in your browser: {loginUrl}");
                Console.WriteLine("After logging in and granting permission, enter the code from the callback URL:");

                var code = Console.ReadLine();

                dynamic result = fb.Get("oauth/access_token", new
                {
                    client_id = AppId,
                    client_secret = AppSecret,
                    redirect_uri = RedirectUri,
                    code
                });

                var accessToken = result.access_token;

                fb.AccessToken = accessToken;

                dynamic me = fb.Get("/me?fields=name,email");
                string name = me.name;
                string email = me.email;

                Console.WriteLine($"Logged in as: {name} ({email})");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public static void GetPostId(string accessToken)
        {
            string pageId = "102847089502067";
            string fields = "posts";

            var client = new FacebookClient(accessToken);

            dynamic result = client.Get(pageId + "?fields=" + fields);
            var posts = result["posts"]["data"];

            foreach (var post in posts)
            {
                string postId = post["id"];
                string postName = post["message"];

                Console.WriteLine("Post ID: " + postId);
                Console.WriteLine("Post Name: " + postName);
            }

            Console.ReadLine();


            //using (HttpClient client = new HttpClient())
            //{
            //    string apiUrl = $"https://api.example.com/{pageId}?fields=posts&access_token={accessToken}";
            //   // string apiUrl = $"https://api.example.com/{pageId}?fields=posts&access_token={accessToken}";

            //    try
            //    {
            //        // Send a GET request to the API
            //        HttpResponseMessage response = client.GetAsync(apiUrl).GetAwaiter().GetResult();


            //        // Check if the request was successful
            //        if (response.IsSuccessStatusCode)
            //        {
            //            // Read the response content as a string
            //            string responseBody = response.Content.ReadAsStringAsync().Result;

            //            // Deserialize the JSON response into a dynamic object
            //            dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);

            //            // Access the 'posts' field from the response
            //            var posts = jsonResponse.posts;

            //            // Iterate over each post and retrieve the ID and name
            //            foreach (var post in posts)
            //            {
            //                string postId = post.id;
            //                string postName = post.name;

            //                Console.WriteLine($"Post ID: {postId}");
            //                Console.WriteLine($"Post Name: {postName}");
            //                Console.WriteLine();
            //                Console.ReadLine ();
            //            }
            //        }
            //        else
            //        {
            //            Console.WriteLine($"Failed to retrieve data. Status Code: {response.StatusCode}");
            //            Console.ReadLine();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"An error occurred: {ex.Message}");
            //        Console.ReadLine();
            //    }
            //}
        }

        //public static void SaveFileInDrive()
        //{
        //    try
        //    {




        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.ReadLine();
        //    }
        //}
        public static void SaveFile()
        {

            string filePath = @"D:\\pdf\\pdf1.pdf";
            try
            {
                /* Load pre-authorized user credentials from the environment.
                TODO(developer) - See https://developers.google.com/identity for
                guides on implementing OAuth2 for your application. */
                byte[] byteArray = System.IO.File.ReadAllBytes(filePath);
                string filename = Path.GetFileName(filePath);
                string mimeType = System.Web.MimeMapping.GetMimeMapping(filename);

                string[] scopes = new string[] { DriveService.Scope.Drive,
        DriveService.Scope.DriveFile,};
                var clientId = "11601186347-t8o2mur2dh1u3ahvg03e6cdf48tbq1nu.apps.googleusercontent.com";      // From https://console.developers.google.com
                var clientSecret = "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";          // From https://console.developers.google.com

                var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                }, scopes, Environment.UserName, CancellationToken.None).Result;

                DriveService service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Web client 2",
                });

                var permission = new Permission()
                {
                    Type = "anyone",
                    Role = "reader"
                };

                // Create Drive API service.

                // Upload file photo.jpg on drive.
                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = filename
                };
                FilesResource.CreateMediaUpload request;
                // Create a new file on drive.
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    // Create a new file, with metadata and stream.
                    request = service.Files.Create(fileMetadata, stream, mimeType);
                    request.Fields = "id";
                    request.Upload();
                }

                var file = request.ResponseBody;

                // Set permission
                PermissionsResource.CreateRequest request1;
                request1 = service.Permissions.Create(permission, file.Id);
                request1.Execute();
                Console.WriteLine("File Uploaded Successfully");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                // TODO(developer) - handle error appropriately
                if (ex is AggregateException)
                {
                    Console.WriteLine("Credential Not found");
                    Console.ReadLine();
                }
                else if (ex is FileNotFoundException)
                {
                    Console.WriteLine("File not found");
                    Console.ReadLine();
                }
                else
                {
                    throw;
                }
            }

        }
        private static UserCredential GetCredentials()
        {
            string[] scopes = { DriveService.Scope.Drive };

            UserCredential credential;
            using (var stream = new FileStream(@"D:/youtube/drive.txt", FileMode.Open, FileAccess.Read))
            {
                string credPath = @"D:/youtube/drive11.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            return credential;
        }


    }

}
