using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSharper;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;
using InstaSharper.Logger;


namespace instantBot
{
    class Program
    {

        #region Hidden
        private const string username = "username";
        private const string password = "password";
        #endregion
        
        private static UserSessionData user;
        private static IInstaApi api;
        static void Main(string[] args)
        {
            user = new UserSessionData();
            user.UserName = username;
            user.Password = password;
            Login();
            
            Console.Read();
        }

        public static async void Login()
        {
            api = InstaApiBuilder.CreateBuilder()
                .SetUser(user)
                .UseLogger(new DebugLogger(LogLevel.Exceptions))
                .SetRequestDelay(TimeSpan.FromSeconds(8))
                .Build();

            var loginRequest = await api.LoginAsync();
            if (loginRequest.Succeeded)
            {

                int NumberOfImagesAndQuotes = 2;
                int Time_you_want_in_between_posts = 15000;
                for (int i = 0; i <= NumberOfImagesAndQuotes; i++)
                {
                    
                    string[] quotes = new string[] { "quotes","you","want" };
                    string[] images = new string[] {@"path\to\image",@"path\to\another\image",@"path\to\yet\another\image" };

                    var mediaImage = new InstaImage
                    {
                        Height = 1080,
                        Width = 1080,
                        URI = new Uri(images[i], UriKind.Absolute).LocalPath
                    };
                    var result = await api.UploadPhotoAsync(mediaImage, quotes[i]);
                    var anotherResult = await api.UploadStoryPhotoAsync(mediaImage, "");
                    System.Threading.Thread.Sleep(Time_you_want_in_between_posts);

                }
            }


            else
            {
                Console.WriteLine("You fricking heck you messed it up, have a free error message:\n" + loginRequest.Info.Message);
            }
        } 

    }
}
