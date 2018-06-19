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
        //*-------------------- About Password -----------------*//
        //////////////////////////////////////////////////////////////

        public string getPassword(string userName)
        {
            string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "UserInfo.txt");

            if (!File.Exists(file_path))
            {
                createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName), file_path);
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

            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.WriteLine(new_file_data);
            string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "UserInfo.txt");

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
            string file_path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "FriendsList.txt");

            if (!File.Exists(file_path))
            {
                createDirAndFile(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName), file_path);
                Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
                Debug.WriteLine(string.Format("getFriendsList : {0} has been created", file_path));
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
                return string.Format("insertFriendsList : {0} has been inserted in {1}'s FriendsList", friendName, userName);
            }
            else
            {
                Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
                Debug.WriteLine(string.Format("insertFriendsList : {0} has already existed", friendName));
                return string.Format("insertFriendsList : {0} has already existed in {1}'s FriendsList", friendName, userName);
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


        public string updateChatHistory(string userName, string userNameToTalk, string chatContents)
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
                    return string.Format("updateChatHistory (friend to user): {0} is not in {1}'s FriendsList", userName, userNameToTalk);
                   
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

        public string getQuiz(string userName, string num)
        {
            string file_path = new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "Quiz", string.Format("Quiz{0}.txt", num) }.Aggregate(Path.Combine);

            if (!File.Exists(file_path))
            {
                return "getQuiz : the file doesn't exist";
            }

            string file_data = string.Empty;

            file_data = readFile(file_path);

            return file_data;
        }

        public string createQuiz(string userName, string quizContents)
        {

            string[] subFolders = Directory.GetFiles(new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "Quiz"}.Aggregate(Path.Combine), "*", SearchOption.AllDirectories);

            string file_path = new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "Quiz", string.Format("Quiz{0}.txt", (int)subFolders.Length + 1) }.Aggregate(Path.Combine);

            if (File.Exists(file_path))
            {
                return "createQuiz : the file has already existed";
            }
            else
            {
                createDirAndFile(new[] { Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "Quiz" }.Aggregate(Path.Combine), file_path);
            }

            string[] contents_splitter = {"__"};
            string[] quiz_array = quizContents.Split(contents_splitter, StringSplitOptions.None);

            if(quiz_array.Length != 4)
            {
                return string.Format("createQuiz : INVALID REQUEST : Request is not according to the protocol");
            }

            string new_file_data = string.Join(" ", quiz_array);

            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.WriteLine(new_file_data);

            // overwrite
            StreamWriter sw = new StreamWriter(file_path, false);
            sw.Write(new_file_data);
            sw.Close();

            return string.Format("createQuiz : create {0}", file_path);
        }

        public string updateQuiz(string userName, string num, string quizContents)
        {
            string file_data = getQuiz(userName, num);

            string[] arr = file_data.Split(' ');

            string[] contents_splitter = {"__"};
            string[] quiz_array = quizContents.Split(contents_splitter, StringSplitOptions.None);

            if (quiz_array.Length != 4)
            {
                return string.Format("updateQuiz : INVALID REQUEST : Request is not according to the protocol");
            }
            else if (arr.Length != 4)
            {
                return string.Format("updateQuiz : Quiz do not match the following format : {QUESTION ANSWER1 ANSWER2 ANSWER3}");
            }


                for (int i = 0; i < (int)quiz_array.Length; i++){
                if(quiz_array[i] != "NOCHANGE")
                {
                    arr[i] = quiz_array[i];
                }
            }

            string new_file_data = string.Join(" ", arr);

            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.WriteLine(new_file_data);
            string file_path = new[] {Path.GetDirectoryName(Directory.GetCurrentDirectory()), "UsersData", userName, "Quiz", string.Format("Quiz{0}.txt", num)}.Aggregate(Path.Combine);

            // overwrite
            StreamWriter sw = new StreamWriter(file_path, false);
            sw.Write(new_file_data);
            sw.Close();

            return string.Format("updateQuiz : update {0}", file_path);
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
                byte[] loadData;

                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    loadData = new byte[fileStream.Length];
                    fileStream.Read(loadData, 0, loadData.Length);
                }

                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                string sjisstr = sjisEnc.GetString(loadData);
                byte[] bytesData = System.Text.Encoding.UTF8.GetBytes(sjisstr);
                Encoding utf8Enc = Encoding.GetEncoding("UTF-8");
                file_data = utf8Enc.GetString(bytesData);
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
