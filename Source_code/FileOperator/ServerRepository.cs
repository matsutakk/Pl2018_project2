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

        //////////////////////////////////////////////////////////////
        //*-------------------- About Account -----------------*//
        //////////////////////////////////////////////////////////////

        public string getAccount(string userName)
        {
            string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "UserInfo.txt");

            if (!File.Exists(file_path))
            {
                createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName), file_path);

                return "getUserAccount: the file doesn't exist";
            }

            string file_data = string.Empty;

            file_data = readFile(file_path);

            return file_data;
        }

        public void updateAccount(string userName, string new_password)
        {
            string file_data = getAccount(userName);

            string[] arr = file_data.Split(' ');

            arr[1] = new_password;

            string new_file_data = string.Join(" ", arr);

            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.WriteLine(new_file_data);
            string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "UserInfo.txt");

            // overwrite
            StreamWriter sw = new StreamWriter(file_path, false);
            sw.Write(new_file_data);
            sw.Close();

        }


        //////////////////////////////////////////////////////////////
        //*-------------------- About FriendsList -----------------*//
        //////////////////////////////////////////////////////////////

        public string getFriendsList(string userName)
        {
            string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "FriendsList.txt");

            if (!File.Exists(file_path))
            {
                createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName), file_path);
                Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
                Debug.WriteLine(string.Format("getFriendsList : {0} has been created", file_path));
                return "";
            }

            string file_data = string.Empty;

            file_data = readFile(file_path);

            return file_data;
        }


        public void insertFriendList(string userName, string friendName)
        {
            string file_data = getFriendsList(userName);

            if (!file_data.Contains(friendName))
            {
                string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "FriendsList.txt");

                if (!File.Exists(file_path))
                {
                    createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName), file_path);
                    Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
                    Debug.WriteLine(string.Format("insertFriendsList : {0} has been created", file_path));
                }
                StreamWriter sw = new StreamWriter(file_path, true);
                sw.Write(" " + friendName);
                sw.Close();
            }
            else
            {
                Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
                Debug.WriteLine(string.Format("insertFriendsList : {0} has already existed", friendName));
            }
        }


        //////////////////////////////////////////////////////////////
        //*-------------------- About ChatHistory -----------------*//
        //////////////////////////////////////////////////////////////

        public string getChatHistroy(string userName, string userNameToTalk)
        {

            // friend to user
            string file_path_friend_user = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userNameToTalk, "ChatHistory", userName + ".txt");

            if (!File.Exists(file_path_friend_user))
            {
                if (getFriendsList(userNameToTalk).Contains(userName))
                {
                    createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userNameToTalk, "ChatHistory"), file_path_friend_user);
                    Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
                    Debug.WriteLine(string.Format("getChatHistroy (friend to user) : created chat history with {0}", userName));
                }
                else
                {
                    return string.Format("getChatHistroy (friend to user): {0} is not in {1}'s FriendsList", userName, userNameToTalk);
                }
            }

            // user to friend 
            string file_path_user_friend = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "ChatHistory", userNameToTalk+".txt");
            if (!File.Exists(file_path_user_friend))
            {
                if (getFriendsList(userName).Contains(userNameToTalk))
                {
                    createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "ChatHistory"), file_path_user_friend);
                    Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
                    Debug.WriteLine(string.Format("getChatHistroy (user to friend) : created chat history with {0}", userNameToTalk));
                }
                else
                {
                    return string.Format("getChatHistroy (user to friend) : {0} is not in {1}'s FriendsList", userNameToTalk, userName);
                }
            }

            string file_data = string.Empty;

            file_data = readFile(file_path_user_friend);
           
            return file_data;
        }


        public void updateChatHistory(string userName, string userNameToTalk, string chatContents)
        {
            // friend to user
            string file_path_friend_user = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userNameToTalk, "ChatHistory", userName + ".txt");

            if (!File.Exists(file_path_friend_user))
            {
                if (getFriendsList(userNameToTalk).Contains(userName))
                {
                    createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userNameToTalk, "ChatHistory"), file_path_friend_user);
                    Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
                    Debug.WriteLine(string.Format("updateChatHistory (friend to user) : created chat history with {0}", userName));
                }
                else
                {
                    Debug.WriteLine(string.Format("updateChatHistory (friend to user): {0} is not in {1}'s FriendsList", userName, userNameToTalk));
                }
            }

            // user to friend 
            string file_path_user_friend = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "ChatHistory", userNameToTalk + ".txt");
            if (!File.Exists(file_path_user_friend))
            {
                if (getFriendsList(userName).Contains(userNameToTalk))
                {
                    createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "ChatHistory"), file_path_user_friend);
                    Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
                    Debug.WriteLine(string.Format("updateChatHistory (user to friend) : created chat history with {0}", userNameToTalk));
                }
                else
                {
                    Debug.WriteLine(string.Format("updateChatHistory (user to friend) : {0} is not in {1}'s FriendsList", userNameToTalk, userName));
                }
            }

            // overwrite

            // user to friend 
            StreamWriter sw1 = new StreamWriter(file_path_user_friend, false);
            sw1.Write(chatContents);
            sw1.Close();

            // friend to user
            StreamWriter sw2 = new StreamWriter(file_path_friend_user, false);
            sw2.Write(chatContents);
            sw2.Close();

        }

        //////////////////////////////////////////////////////////////
        //*-------------------- About Quiz-------------------*////////
        //////////////////////////////////////////////////////////////

        public string getQuiz(string userName)
        {
            string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "Quiz.txt");

            if (!File.Exists(file_path))
            {
                return "getQuiz : the file doesn't exist";
            }

            string file_data = string.Empty;

            file_data = readFile(file_path);

            return file_data;
        }

        //////////////////////////////////////////////////////////////
        //*-------------------- Sub Methods -----------------*////////
        //////////////////////////////////////////////////////////////

        public void createDirAndFile(string directoryPath, string filePath)
        {
            Directory.CreateDirectory(directoryPath);
            using (FileStream fs = File.Create(filePath))
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
         }

        public string readFile(string path)
        {
            string file_data = "";
            try
            {
                using (FileStream fs = new FileStream(path,
                    FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (TextReader sr = new StreamReader(fs,
                        Encoding.GetEncoding("shift-jis")))
                    {
                        file_data = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
                Debug.WriteLine("The file could not be read:");
                Debug.WriteLine(e.Message);
                file_data = "error";
            }
            return file_data;
        }



        //static void Main(string[] args)
        //{
        //    ServerRepository sr = new ServerRepository();

        //    Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));

        //    // test
        //    //Debug.WriteLine(sr.getAccount("yoshida"));
        //    //sr.updateAccount("yoshida", "sss");
        //    //Debug.WriteLine(sr.getAccount("yoshida"));

        //    //Debug.WriteLine(sr.getFriendsList("yoshida"));
        //    //sr.insertFriendList("yoshida", "kagawa");
        //    //Debug.WriteLine(sr.getFriendsList("yoshida"));


        //    //Debug.WriteLine(sr.getChatHistroy("yoshida", "kagawa"));
        //    //sr.updateChatHistory("yoshida", "kagawa", "sss");
        //    //Debug.WriteLine(sr.getChatHistroy("yoshida", "kagawa"));
        //    //Debug.WriteLine(sr.getChatHistroy("kagawa", "yoshida"));

        //}
    }
}
