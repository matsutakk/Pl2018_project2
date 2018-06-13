using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FileOperation
{
    public class ServerRepository
    {

        private string convertTextCoding(string text)
        {
            return text;
        }

        public string getUserAccount(string userName)
        {
            string file_path = System.IO.Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "UserInfo.txt");

            if (!System.IO.File.Exists(file_path))
            {
                return "getUserAccount: the file doesn't exist";
            }

            string file_data = string.Empty;
            try
            {
                using (FileStream fs = new FileStream(file_path,
                    FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (TextReader sr = new StreamReader(fs,
                        Encoding.GetEncoding("shift-jis")))
                    {
                        file_data = sr.ReadToEnd();

                    }
                }
            }
            catch(Exception e){
                Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
                Debug.WriteLine("The file could not be read:");
                Debug.WriteLine(e.Message);
            }
            return convertTextCoding(file_data);
        }

        public void updateUserAccount(string userName, string new_password)
        {
            string file_data = getUserAccount(userName);

            string[] arr = file_data.Split(' ');

            arr[1] = new_password;

            string new_file_data = string.Join(" ", arr);

            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.WriteLine(new_file_data);
            string file_path = System.IO.Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "UserInfo.txt");

            // overwrite
            StreamWriter sw = new System.IO.StreamWriter(file_path, false);
            sw.Write(new_file_data);
            sw.Close();

        }

        public string getFriendsList(string userName)
        {
            string file_path = System.IO.Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "FriendsList.txt");

            if (!System.IO.File.Exists(file_path))
            {
                return "getFriendsList : the file doesn't exist";
            }

            string file_data = string.Empty;

            StreamReader sr = new System.IO.StreamReader(file_path);

            file_data = sr.ReadToEnd();

            return convertTextCoding(file_data);
        }

        //TODO:
        public void insertFriendList(string userName, string friendName)
        {

        }


        public string getChatHistroy(string userName, string userNameToTalk)
        {
            string file_path = System.IO.Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "ChatHistory", userNameToTalk+".txt");

            if (!System.IO.File.Exists(file_path))
            {
                return "getChatHistroy : the file doesn't exist";
            }

            string file_data = string.Empty;

            StreamReader sr = new System.IO.StreamReader(file_path);

            file_data = sr.ReadToEnd();

            return convertTextCoding(file_data);
        }

        //TODO:
        public void updateChatHistory(string userName, string userNameToTalk)
        {

        }

        public string getQuiz(string userName)
        {
            string file_path = System.IO.Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "Quiz.txt");

            if (!System.IO.File.Exists(file_path))
            {
                return "getQuiz : the file doesn't exist";
            }

            string file_data = string.Empty;

            StreamReader sr = new System.IO.StreamReader(file_path);

            file_data = sr.ReadToEnd();

            return convertTextCoding(file_data);
        }



        static void Main(string[] args)
        {
            ServerRepository sr = new ServerRepository();

            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.WriteLine(sr.getUserAccount("User1"));
            sr.updateUserAccount("User1", "sss");
        }
    }
}
