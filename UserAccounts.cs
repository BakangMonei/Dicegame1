using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace STEM_ASSIGNMENT
{
    class UserAccounts
    {
        
        private string userFilePath = "Users.txt";
        private string userLogsFilePath = "log.txt";
        public string userName = "";
        public UserAccounts()
        {

        }
        public bool addUser(string userUsername, string userPassword, string userEmail, string userFirstName, string userSurname, string userPhysicalAddress, string userPhoneNumber)
        {
            string userDetails = userUsername + "#" + userPassword + "#" + userEmail + "#" + userFirstName + "#" + userSurname + "#" + userPhysicalAddress + "#" + userPhoneNumber;
            bool userAdded = false;
          
        
            
            if (!userExists(userDetails))
            {
                fileWriter(userDetails, userFilePath);
                userAdded = true;
            }
            return userAdded;

        }

        public bool userLogin(string username, string password)
        {
            bool loginSuccessful = false;
            if (File.Exists(userFilePath))
                using (StreamReader streamReader = File.OpenText(userFilePath))
                {
                    string line = "";

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] lineDetails = line.Split('#');
                        if (lineDetails[0].Equals(username) && lineDetails[1].Equals(password)) //username and password are a match
                        {
                            loginSuccessful = true;
                            string logString = username + ", " + DateTime.Now.ToString(); //to record time when user logged in
                            fileWriter(logString, userLogsFilePath);
                        }

                    }
                    streamReader.Close();
                }

            return loginSuccessful;

        }
    
        private bool userExists(String user)
        {
            bool userExists = false;
            string[] userDetails = user.Split('#');
            if (File.Exists(userFilePath))
                using (StreamReader streamReader = File.OpenText(userFilePath))
                {
                    string line = "";
                
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            string[] lineDetails = line.Split('#');
                            if (userDetails[0].Equals(lineDetails[0]) || userDetails[2].Equals(lineDetails[2])) //username and email must be unique
                                userExists = true;
                        }
                    streamReader.Close();
                }

        return userExists;
        }


        private void fileWriter(string content, string filePath)
        {
           

            StreamWriter streamWriter;
            if (!File.Exists(filePath))
            {
                // Create a file to write to.
                streamWriter = new StreamWriter(filePath, true);
            }
            else
                streamWriter = File.AppendText(filePath);

            streamWriter.WriteLine(content);

            streamWriter.Close();

        }


    }
}
