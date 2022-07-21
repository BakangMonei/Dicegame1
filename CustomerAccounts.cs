using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace STEM_ASSIGNMENT
{
    class StudentAccounts
    {

        private string customerFilePath = "StudentDetails.txt";
        public StudentAccounts()
        {

        }
        public bool addCustomer(string newCustomerFirstname, string newCustomerSurname, string newCustomerCustomerNumber, string newCustomerContact)
        {
            bool customerAdded = false;
            string customerDetails = newCustomerFirstname + "#"+ newCustomerSurname + "#" + newCustomerCustomerNumber + "#" + newCustomerContact + "#"+ DateTime.Now.ToString();


            if (!customerExists(customerDetails))
            {
                this.fileWriter(customerDetails, customerFilePath);
                customerAdded = true;
            }
            return customerAdded;

        }


        private bool customerExists(String customerDetails)
        {
            bool customerExists = false;
            string[] customerDetailsArray = customerDetails.Split('#');
            string[] existingCustomerDetailsArray = this.getCustomerDetails(customerDetailsArray[2]);

      
                        if (existingCustomerDetailsArray != null) //custmer exists
                            customerExists = true;
                    

            return customerExists;
        }

        public string[] getCustomerDetails(string customerNumber)
        {
            string[] customerDetailsArray = null;
            if (File.Exists(customerFilePath))
                using (StreamReader streamReader = File.OpenText(customerFilePath))
                {
                    string line = "";

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] lineDetails = line.Split('#');
                        if (lineDetails[2].Equals(customerNumber))
                            customerDetailsArray = lineDetails;

                    }
                    streamReader.Close();
                }
            return customerDetailsArray;
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

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
