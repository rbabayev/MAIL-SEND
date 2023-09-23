using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using User;
using Admin;
using Post;
using System.Security.Cryptography.X509Certificates;

//=======================================================================
//=======================================================================

namespace Post
{
    public class Post
    {
        public int PostId { get; }
        public string Content { get; set; }
        public string CreationDateTime { get; set; }
        public int LikeCount { get; set; }
        public int ViewCount { get; set; }
        public List<Post> Posts { get; set; } = new List<Post> { };
        public int ID { get; set; }

        public Post(int postId,string content , string creationTime , int likeCount , int viewCount)
        {
            PostId = postId;
            Content = content;
          CreationDateTime = creationTime;
          LikeCount = likeCount;
          ViewCount = viewCount;
          Posts = new List<Post>();
        }

        public Post(int postId, string content)
        {
            PostId = postId;
            Content = content;
        }

        public Post()
        {
        }


        public void UserMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Show post list");
            Console.WriteLine("2. Like and show post\n");
            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            while (true)
            {
                switch (choice)
                {
                    case 1:
                        ShowPostList();
                        break;
                    case 2:
                        LikeAndShowPost();
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }


        public void CreateDelPost()
        {
            Console.Clear();
            Console.WriteLine("\n\n1. Create a post");
            Console.WriteLine("2. Delete a post");
            Console.WriteLine("3. Show post list");
            Console.WriteLine("4. Like and show post\n");
            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            
                switch (choice)
                {
                    case 1:
                        CreatePost();
                        break;
                    case 2:
                        DeletePost();
                        break;
                    case 3:
                        ShowPostList();
                        break;
                    case 4:
                        LikeAndShowPost();
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            

        }

        public void CreatePost()
        {
            Console.Write("Post : ");
            string content = Console.ReadLine();
            int postId = (Posts?.Count != null ? Posts.Count : 0)+1;
            string creationTime = DateTime.Now.ToString();
            Post post = new Post(postId, content, creationTime, 0, 0);
            Posts.Add(post);
            Console.WriteLine("Post created successfully!");
            ShowPostList();
        }

        public void DeletePost()
        {
            Console.WriteLine("Post ID For Delete: ");
            int postId = Convert.ToInt32(Console.ReadLine());

            Post postToRemove = Posts.Find(p => p.PostId == postId);
            if (postToRemove != null)
            {
                Posts.Remove(postToRemove);
                Console.WriteLine("Post deleted successfully!");
            }
            else
            {
                Console.WriteLine("Post not found!");
            }
            ShowPostList();
        }

        public void LikeAndShowPost()
        {
            Console.WriteLine("Post ID For Like : ");
            int postId = Convert.ToInt32(Console.ReadLine());

            Post postToLike = Posts.Find(p => p.PostId == postId);
           //like elediyim posta baxdigimiz ucun id artanda showda artacaq
            if (postToLike != null)
            {
                postToLike.LikeCount++;
                postToLike.ViewCount++;
               
                Console.WriteLine("Post liked successfully!");
            }
            else
            {
                Console.WriteLine("Post not found!");
            }
            ShowPostList();
        }

        public void ShowPostList()
        {
            Console.WriteLine("Post List:");
            foreach (Post post in Posts)
            {
                Console.WriteLine($"==================\n - ID: {post.PostId}\n - Content: {post.Content}\n - DateTime: {post.CreationDateTime}\n - Likes: {post.LikeCount}\n - Views: {post.ViewCount}\n==================");
            }
        }

    }


}

//=======================================================================
//=======================================================================
namespace User
{
    public class User : Post.Post
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Age { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        User(int id, string name, string surname, int age, string email, string password)
        {
            ID = id;
            Name = name;
            SurName = surname;
            Age = age;
            Email = email;
            Password = password;
        }

        public User()
        {
        }

        public void UserRegister()
        {
            bool startRegister = false;
            while (!startRegister)
            {
                Console.Write("Enter Name : ");
                Name = (string)Console.ReadLine();
                Console.Write("Enter Surname : ");
                SurName = (string)Console.ReadLine();
                Console.Write("Enter Age : ");
                Age = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Password : ");
                Password = (string)Console.ReadLine();
                Console.Write("Enter Email : ");
                Email = (string)Console.ReadLine();
                if (Email != null)
                {
                    string fromEmail = "fakexcompany@gmail.com";
                    string password = "fakex321";

                    string toEmail = Email;

                    string smtpServer = "smtp.gmail.com";
                    int smtpPort = 587;

                    string subject = "Verification Code";
                    string body = "";

                    try
                    {
                        SmtpClient client = new SmtpClient(smtpServer, smtpPort);
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(fromEmail, password);
                        Random random = new Random();
                        for (int i = 10000; i < 100000; i++)
                        {
                            int randomNumber = random.Next(10000, 100000);
                            body = $"{i}";

                            MailMessage message = new MailMessage(fromEmail, toEmail, subject, body);

                            client.Send(message);
                            Console.Clear();
                            while (true)
                            {
                                Console.Write("Enter Verification Code : ");
                                int verifCode = Convert.ToInt32(Console.ReadLine());
                                if (verifCode != Convert.ToInt32(body))
                                {
                                    UserMenu();

                                }
                                else  Console.WriteLine("Press any key to continue...");
                            }
                            Console.ReadLine();
                        }

                        Console.WriteLine("Sendded Verification Code.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Dogrulama kodu yollanmadi : " + ex.Message);
                    }
                }
            }
        }





    }


}
//=======================================================================
//=======================================================================
namespace Admin
{
    public class Admin : Post.Post
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
        public string Notification { get; set; }

        Admin(string username, string password, string email, string post, string notifcs)
        {
            UserName = username;
            Password = password;
            Email = email;
            Notification = notifcs;

        }

        public Admin()
        {
        }

        public class Post
        {
            public int ID { get; set; }
            public string Content { get; set; }

            public Post(int id, string content)
            {
                ID = id;
                Content = content;
            }
        }

        public void Login(string email, string password, string username)
        {
            
            Console.Write("\nEmail daxil edin : ");
            string logEmail = Console.ReadLine();


            Console.Write("\nSifrenizi daxil edin : ");
            string logPassw = Console.ReadLine();


            Console.Write("\nUsername daxil edin : ");
            string logUsername = Console.ReadLine();
            while(true)
            {
                if (logEmail == email && logPassw == password && logUsername == username)
                {
                    CreateDelPost();

                }
                else
                {
                    Console.WriteLine("Press any key to continue... ");
                }
                Console.ReadLine();
            }
            
        }

    }

}

//=======================================================================
//=======================================================================
class Twitter
{
    static int selectedOption = 0;
    static string[] menuOptions = { "<< Admin >>", "<< User >>" };

    static void Main(string[] args)
    {
        Admin.Admin admin = new Admin.Admin();
        Post.Post post = new Post.Post();
        User.User user = new User.User();


        Console.CursorVisible = false;
        while (true)
        {

            Console.Clear();
            DrawMenu();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedOption > 0)
                        selectedOption--;
                    break;

                case ConsoleKey.DownArrow:
                    if (selectedOption < menuOptions.Length - 1)
                        selectedOption++;
                    break;

                case ConsoleKey.Enter:
                    Console.Clear();
                    Console.WriteLine("Selected option: " + menuOptions[selectedOption]);
                   if (selectedOption == 0)
                    {
                        admin.Login("a","Admin","admin");
                    }
                   else if (selectedOption == 1) 
                    {
                        user.UserRegister();
                    }
                    break;
            }
        }

    }


    static void DrawMenu()
    {
        Console.WriteLine(@"
 ▀████    ▐████▀ 
   ███▌   ████▀  
    ███  ▐███    
    ▀███▄███▀    
    ████▀██▄     
   ▐███  ▀███    
  ▄███     ███▄  
 ████       ███▄ 

");
        for (int i = 0; i < menuOptions.Length; i++)
        {
            if (i == selectedOption)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }

            Console.WriteLine(menuOptions[i]);

            Console.ResetColor();
        }
    }



}