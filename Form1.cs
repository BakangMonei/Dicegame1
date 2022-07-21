using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STEM_ASSIGNMENT
{

    public partial class Form1 : Form
    {
        bool passwordMatching = false;
        UserAccounts userAccounts = new UserAccounts();
        CustomerAccounts customerAccounts = new CustomerAccounts();
        CustomerBalance customerBalance = new CustomerBalance();
        string currentCustomerNumber;
        Decimal MAX_BALANCE = 2500.0M;
        public Form1()
        {
            InitializeComponent();
            hideAllPanels();
            loginPanel.Visible = true;

        }

        private void register_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loginPanel.Visible = false;
            userRegisterationPanel.Visible = true;
        }

        private void cancelRegistration_Click_1(object sender, EventArgs e)
        {
            loginPanel.Visible = true;
            userRegisterationPanel.Visible = false;
            ControlsExtensions.ClearControls(this);
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string userEntry = "";
            //validate filling all fields
            if (userUsername.Text.Contains("#") || userPassword.Text.Contains("#") || userEmail.Text.Contains("#") || userFirstName.Text.Contains("#") || userSurname.Text.Contains("#") || userPhysicalAddress.Text.Contains("#") || userPhoneNumber.Text.Contains("#"))
                MessageBox.Show("'#' character not allowed");
            else if (userUsername.Text.Equals("") || userPassword.Text.Equals("") || userEmail.Text.Equals("") || userFirstName.Text.Equals("") || userSurname.Text.Equals("") || userPhysicalAddress.Text.Equals("") || userPhoneNumber.Text.Equals(""))
                MessageBox.Show("All fields must be filled");
            //validate email address format
            else if (!userEmail.Text.Contains("@"))
                MessageBox.Show("Invalid email address");
            //validate password confirmation
            else if (!passwordMatching)
                MessageBox.Show("Passwords not matching");
            else
            {

                if (!userAccounts.addUser(userUsername.Text, userPassword.Text, userEmail.Text, userFirstName.Text, userSurname.Text, userPhysicalAddress.Text, userPhoneNumber.Text))
                    MessageBox.Show("account not created, user exists:");
                else
                {
                    MessageBox.Show("Account created successfuly");
                    hideAllPanels();
                    loginPanel.Visible = true;
                    loginPassword.Clear();
                    loginUsername.Clear();
                }

            }


        }

        private void login_Click(object sender, EventArgs e)
        {

            if (userAccounts.userLogin(loginUsername.Text, loginPassword.Text))
            {
                hideAllPanels();
                welcomeUser.Text = loginUsername.Text;
                loggedInPanel.Visible = true;
                menuPanel.Visible = true;
            }
            else
            {
                MessageBox.Show("Login failed");
                ControlsExtensions.ClearControls(this);
            }




        }
        private void createNewCustomer_Click(object sender, EventArgs e)
        {
            if (newCustomerFirstname.Text.Contains("#") || newCustomerSurname.Text.Contains("#") || newCustomerCustomerNumber.Text.Contains("#") || newCustomerContact.Text.Contains("#"))
                MessageBox.Show("'#' character not allowed");
            else if (newCustomerFirstname.Text.Equals("") || newCustomerSurname.Text.Equals("") || newCustomerCustomerNumber.Text.Equals("") || newCustomerContact.Text.Equals(""))
                MessageBox.Show("All fields must be filled");
            else
            {

                if (!customerAccounts.addCustomer(newCustomerFirstname.Text, newCustomerSurname.Text, newCustomerCustomerNumber.Text, newCustomerContact.Text))
                    MessageBox.Show("Customer exists");
                else
                {
                    hideAllPanels();
                    loggedInPanel.Visible = true;
                    menuPanel.Visible = true;
                    MessageBox.Show("Customer account created");
                }



            }
        }

        private void hideAllPanels()
        {
            loggedInPanel.Visible = false;
            loginPanel.Visible = false;
            menuPanel.Visible = false;
            newCustomerPanel.Visible = false;
            existingCustomerPanel.Visible = false;
            overdueLoansPanel.Visible = false;
            userRegisterationPanel.Visible = false;
        }

        private void returnToMenu()
        {
            hideAllPanels();
            loggedInPanel.Visible = true;
            menuPanel.Visible = true;
        }

        private void loadCustomerDetails()
        {

            string[] customerDetailsArray = customerAccounts.getCustomerDetails(customerNumberTextBox.Text);
            string[] AccountDetailsArray = customerBalance.getLoanDetails(customerNumberTextBox.Text);
            if (customerDetailsArray != null)
            {
                customerFirstnameLabel.Text = customerDetailsArray[0];
                customerSurnameLabel.Text = customerDetailsArray[1];
                customerNumberLabel.Text = customerDetailsArray[2];
                currentCustomerNumber = customerDetailsArray[2];
                customerContactLabel.Text = customerDetailsArray[3];
                customerDateCreatedLabel.Text = customerDetailsArray[4];
                if (AccountDetailsArray != null)
                {
                    if (Decimal.Parse(AccountDetailsArray[1]) != 0)
                    {
                        accountBalanceLabel.Text = "P" + AccountDetailsArray[1];
                        dateIssuedLabel.Text = AccountDetailsArray[2];
                        Double days = Math.Ceiling((DateTime.Today - (DateTime.Parse(AccountDetailsArray[2]))).TotalDays);
                        daysToDateLabel.Text = days.ToString();
                    }
                    else
                    {
                        accountBalanceLabel.Text = "P0.00";
                        dateIssuedLabel.Text = "-";
                        daysToDateLabel.Text = "-";
                    }
                }
                else
                {
                    accountBalanceLabel.Text = "P0.00";
                    dateIssuedLabel.Text = "-";
                    daysToDateLabel.Text = "-";
                }
                hideAllPanels();
                loggedInPanel.Visible = true;
                existingCustomerPanel.Visible = true;
            }

            else
            {
                MessageBox.Show("Customer not found");
            }
        }

        private void confirmPassword_TextChanged(object sender, EventArgs e)
        {
            validatePassword();
        }

        private void password_TextChanged(object sender, EventArgs e)
        {
            validatePassword();
        }

        private void validatePassword()
        {
            if (userPassword.Text.Equals(userConfirmPassword.Text))
            {
                passwordMatchTextNote.Text = "password matching";
                passwordMatchTextNote.ForeColor = Color.Green;
                passwordMatching = true;

            }
            else
            {
                passwordMatchTextNote.Text = "password not matching";
                passwordMatchTextNote.ForeColor = Color.Red;
                passwordMatching = false;
            }

        }

        private void logout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            hideAllPanels();
            loginPanel.Visible = true;
            ControlsExtensions.ClearControls(this);


        }

        private void cancelNewCustomerRegistration_Click(object sender, EventArgs e)
        {
            ControlsExtensions.ClearControls(this);
            returnToMenu();

        }



        private void selectMenuBtn_Click(object sender, EventArgs e)
        {
            if (existingCustomerRadioBtn.Checked)
            {
                loadCustomerDetails();

            }

            if (overdueAccountsRadioBtn.Checked)
            {
                hideAllPanels();
                loggedInPanel.Visible = true;
                overdueLoansPanel.Visible = true;
                loansViewBox.Clear();
                string[] BalanceFileContent = customerBalance.getAllCustomerLoans();
                if(BalanceFileContent !=null)
                    foreach (string line in BalanceFileContent)
                    {
                        string[] AccountDetails = line.Split('#');
                        Double days = Math.Ceiling((DateTime.Today - (DateTime.Parse(AccountDetails[2]))).TotalDays);

                        string customerNumber = AccountDetails[0];
                        string[] customerDetails = customerAccounts.getCustomerDetails(customerNumber);
                        string viewString = customerDetails[2] + " | " + customerDetails[0] + " " + customerDetails[1] + " | " + customerDetails[2] + " | " + customerDetails[3] + " | " + AccountDetails[2] + " | " + days.ToString() + "\n";
                        if (days > 62)
                            RichTextBoxExtensions.AppendText(loansViewBox, viewString, Color.Red);
                        else
                            RichTextBoxExtensions.AppendText(loansViewBox, viewString, Color.Black);
                    }

            }
            if (newCustomerRadioBtn.Checked)
            {
                hideAllPanels();
                loggedInPanel.Visible = true;
                newCustomerPanel.Visible = true;

            }
        }

        private void cancelExistingCustomerUpdate_Click(object sender, EventArgs e)
        {
            ControlsExtensions.ClearControls(this);
            returnToMenu();
        }


        private void updateExistingCustomer_Click(object sender, EventArgs e)
        {
            string[] AccountDetailsArray = customerBalance.getLoanDetails(currentCustomerNumber);
            if (issueLoanBtn.Checked)
            {
                if (AccountDetailsArray != null)
                {

                    if (Decimal.Parse(AccountDetailsArray[1]) > 0)
                    {
                        MessageBox.Show("Customer has to withdraw first");
                    }
                    else
                    {
                        if (Decimal.Parse(amount.Text) > MAX_BALANCE)
                        {
                            MessageBox.Show("Amount exceeds maximum allowed of BWP2500.00");
                        }
                        else
                        {
                            if (customerBalance.updateLoan(currentCustomerNumber, amount.Text))
                            {
                                MessageBox.Show("Deposit issued successfully");
                                loadCustomerDetails();
                            }
                            else
                            {
                                MessageBox.Show("Failed to deposit funds");
                            }
                        }
                    }
                }
                else
                {
                    if (Decimal.Parse(amount.Text) > MAX_BALANCE)
                    {
                        MessageBox.Show("Amount exceeds maximum allowed of BWP2500.00");
                    }
                    else
                    {
                        if (customerBalance.addLoan(currentCustomerNumber, amount.Text))
                        {
                            MessageBox.Show("Deposit issued successfully");
                            loadCustomerDetails();
                        }
                        else
                        {
                            MessageBox.Show("Failed to deposit funds");
                        }
                    }
                }
            }
            if (payLoanBtn.Checked)
            {
                if (Decimal.Parse(AccountDetailsArray[1]) == 0)
                {
                    MessageBox.Show("Customer account balance is BWP0.00");
                }
                else if (Decimal.Parse(amount.Text) > Decimal.Parse(AccountDetailsArray[1]))
                {
                    MessageBox.Show("account balance less than withdrawal issued");
                }
                else
                {
                    if (customerBalance.updateLoan(currentCustomerNumber, "-" + amount.Text))
                    {
                        MessageBox.Show("Account updated successfully");
                        loadCustomerDetails();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update");
                    }

                }
            }
        }

        private void loanViewCancel_Click(object sender, EventArgs e)
        {

            returnToMenu();
        }

        private void existingCustomerRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            customerNumberTextBox.Enabled = true;
        }

        private void overdueLoansRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            customerNumberTextBox.Enabled = false;
        }

        private void newCustomerRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            customerNumberTextBox.Enabled = false;
        }
    }
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string str, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionColor = color;
            box.AppendText(str);
            box.SelectionColor = box.ForeColor;
        }
    }
    public static class ControlsExtensions
    {
        public static void ClearControls(this Control frm)
        {
            foreach (Control control in frm.Controls)
            {
                if (control is TextBox)
                {
                    control.ResetText();
                }

                if (control.Controls.Count > 0)
                {
                    control.ClearControls();
                }
            }
        }
    }
}
