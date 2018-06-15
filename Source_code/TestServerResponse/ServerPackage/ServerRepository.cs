using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
//using System.Diagnostics;

namespace ServerPackage
{

    //TODO :: replace Debug
   

    public class ServerRepository
    {
        //////////////////////////////////////////////////////////////
        //*-------------------- About Password -----------------*//
        //////////////////////////////////////////////////////////////

        public string getPassword(string userName)
        {
            //string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "UserInfo.txt");
            string file_path = new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "UserInfo.txt" }.Aggregate(Path.Combine);



            if (!File.Exists(file_path))
            {
                //createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName), file_path);
                createDirAndFile(new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName }.Aggregate(Path.Combine), file_path);

                return "getPassword : the file doesn't exist";
            }

            string file_data = string.Empty;

            file_data = readFile(file_path);

            return file_data;
        }

        public string updatePassword(string userName, string new_password)
        {
            string file_data = getPassword(userName);

            string[] arr = file_data.Split(' ');

            arr[1] = new_password;

            string new_file_data = string.Join(" ", arr);

            Debug.Log(new_file_data);

            //string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "UserInfo.txt");
            string file_path = new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "UserInfo.txt" }.Aggregate(Path.Combine);

            // overwrite
            StreamWriter sw = new StreamWriter(file_path, false);
            sw.Write(new_file_data);
            sw.Close();

            return string.Format("updateAccount : update {0}", file_path);
        }


        //////////////////////////////////////////////////////////////
        //*-------------------- About FriendsList -----------------*//
        //////////////////////////////////////////////////////////////

        public string getFriendsList(string userName)
        {
            //string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), Assets, "UsersData", userName, "FriendsList.txt");
            string file_path = new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "FriendsList.txt" }.Aggregate(Path.Combine);


            if (!File.Exists(file_path))
            {
                //createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets","UsersData", userName), file_path);
                createDirAndFile(new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName }.Aggregate(Path.Combine), file_path);

                Debug.Log(string.Format("getFriendsList : {0} has been created", file_path));
            }

            string file_data = string.Empty;

            file_data = readFile(file_path);

            return file_data;
        }


        public string insertFriendList(string userName, string friendName)
        {
            string file_data = getFriendsList(userName);

            if (!file_data.Contains(friendName))
            {

                //string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), Assets, "UsersData", userName, "FriendsList.txt");
                string file_path = new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "FriendsList.txt" }.Aggregate(Path.Combine);

                if (!File.Exists(file_path))
                {
                    //createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets","UsersData", userName), file_path);
                    createDirAndFile(new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName }.Aggregate(Path.Combine), file_path);

                    Debug.Log(string.Format("insertFriendsList : {0} has been created", file_path));
                }
                StreamWriter sw = new StreamWriter(file_path, true);
                sw.Write(" " + friendName);
                sw.Close();
                return string.Format("insertFriendsList : {0} has been inserted in {1}'s FriendsList", friendName, userName);
            }
            else
            {
                Debug.Log(string.Format("insertFriendsList : {0} has already existed", friendName));
                return string.Format("insertFriendsList : {0} has already existed in {1}'s FriendsList", friendName, userName);
            }
        }


        //////////////////////////////////////////////////////////////
        //*-------------------- About ChatHistory -----------------*//
        //////////////////////////////////////////////////////////////

        public string getChatHistroy(string userName, string userNameToTalk)
        {

            // friend to user
            //string file_path_friend_user = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets","UsersData", userNameToTalk, "ChatHistory", userName + ".txt");
            string file_path_friend_user = new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userNameToTalk, "ChatHistory", userName + ".txt" }.Aggregate(Path.Combine);

            if (!File.Exists(file_path_friend_user))
            {
                if (getFriendsList(userNameToTalk).Contains(userName))
                {
                    //createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()),  "Assets","UsersData", userNameToTalk, "ChatHistory"), file_path_friend_user);
                    createDirAndFile(new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userNameToTalk, "ChatHistory" }.Aggregate(Path.Combine), file_path_friend_user);


                   
                    Debug.Log(string.Format("getChatHistroy (friend to user) : created chat history with {0}", userName));
                }
                else
                {
                    return string.Format("getChatHistroy (friend to user): {0} is not in {1}'s FriendsList", userName, userNameToTalk);
                }
            }

            // user to friend 
            //string file_path_user_friend = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets","UsersData", userName, "ChatHistory", userNameToTalk + ".txt");
            string file_path_user_friend = new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "ChatHistory", userNameToTalk + ".txt" }.Aggregate(Path.Combine);


            if (!File.Exists(file_path_user_friend))
            {
                if (getFriendsList(userName).Contains(userNameToTalk))
                {
                    //createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "ChatHistory"), file_path_user_friend);
                    createDirAndFile(new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "ChatHistory" }.Aggregate(Path.Combine), file_path_user_friend);

                   
                    Debug.Log(string.Format("getChatHistroy (user to friend) : created chat history with {0}", userNameToTalk));
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


        public string updateChatHistory(string userName, string userNameToTalk, string chatContents)
        {
            // friend to user
            //string file_path_friend_user = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userNameToTalk, "ChatHistory", userName + ".txt");
            string file_path_friend_user = new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userNameToTalk, "ChatHistory", userName + ".txt" }.Aggregate(Path.Combine);


            if (!File.Exists(file_path_friend_user))
            {
                if (getFriendsList(userNameToTalk).Contains(userName))
                {
                    //createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()),  "Assets","UsersData", userNameToTalk, "ChatHistory"), file_path_friend_user);
                    createDirAndFile(new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userNameToTalk, "ChatHistory" }.Aggregate(Path.Combine), file_path_friend_user);

                   
                    Debug.Log(string.Format("updateChatHistory (friend to user) : created chat history with {0}", userName));
                }
                else
                {
                    Debug.Log(string.Format("updateChatHistory (friend to user): {0} is not in {1}'s FriendsList", userName, userNameToTalk));
                    return string.Format("updateChatHistory (friend to user): {0} is not in {1}'s FriendsList", userName, userNameToTalk);

                }
            }

            // user to friend 
            //string file_path_user_friend = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets","UsersData", userName, "ChatHistory", userNameToTalk + ".txt");
            string file_path_user_friend = new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "ChatHistory", userNameToTalk + ".txt" }.Aggregate(Path.Combine);

            if (!File.Exists(file_path_user_friend))
            {
                if (getFriendsList(userName).Contains(userNameToTalk))
                {
                    //createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "ChatHistory"), file_path_user_friend);
                    createDirAndFile(new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "ChatHistory" }.Aggregate(Path.Combine), file_path_user_friend);

                   
                    Debug.Log(string.Format("updateChatHistory (user to friend) : created chat history with {0}", userNameToTalk));
                }
                else
                {
                    Debug.Log(string.Format("updateChatHistory (user to friend) : {0} is not in {1}'s FriendsList", userNameToTalk, userName));
                    return string.Format("updateChatHistory (user to friend) : {0} is not in {1}'s FriendsList", userNameToTalk, userName);
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

            return string.Format("updateChatHistory : updated {0} \n and {1}", file_path_user_friend, file_path_friend_user);

        }

        //////////////////////////////////////////////////////////////
        //*-------------------- About Quiz-------------------*////////
        //////////////////////////////////////////////////////////////

        public string getQuiz(string userName)
        {
            //string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets","UsersData", userName, "Quiz.txt");
            string file_path = new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Assets", "UsersData", userName, "Quiz.txt" }.Aggregate(Path.Combine);

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
               
                Debug.Log("The file could not be read:");
                Debug.Log(e.Message);
                file_data = "error";
            }
            return file_data;
        }

    }
}
