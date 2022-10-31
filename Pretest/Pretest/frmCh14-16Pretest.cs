using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Pretest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //  Global Constants
        const int MINEMPNUMBER  = 1000;     //  Minimum employee number
        const int MAXEMPNUMBER  = 9999;     //  Maximum employee number

        //  HourlyEmployee Constants
        const decimal MINHOURS  =  0.00m;   //  Minimum hours worked
        const decimal MAXHOURS  = 84.00m;   //  Maximum hours worked
        const decimal MINHRATE  =  0.00m;   //  Minimum hourly rate
        const decimal MAXHRATE  = 99.99m;   //  Maximum hourly rate
        const decimal MAXNONOT  = 40.00m;   //  Maximum hours worked w/ no OT
        const decimal OTRATE    =  1.5m;    //  Overtime rate (time and one-half)

        //  Pieceworker Employee Constants
        const int MINPIECES     =   0;      //  Minimum pieces
        const int MAXPIECES     = 100;      //  Maximum pieces
        const decimal MINPPP    = 0.00m;    //  Minimum price per piece
        const decimal MAXPPP    = 1.00m;    //  Maximum price per piece

        //  Global variable
        decimal grossPay        = 0.00m;    //  Gross pay

        //  Code that executes when Calculate button in clicked
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            bool success;

            //  Check for neither checkbox chosen
            if ((!radHourlyEmployee.Checked) &&
                (!radPieceworkerEmployee.Checked))
            {
                showMessage("You Must Choose An Employee Type!",
                            "NO EMPLOYEE TYPE CHOSEN");
                return;
            }

            //  One of the checkboxes was selected
            success = validateCommonFields();
            {
                //  If success == false, return
                if (!success)
                {
                    return;
                }

                //  If success == true, the three textboxes, i.e.,
                //  first name, last name, and emp number all valid.
                //  Check which radio button was selected and program accordingly.
                if (radHourlyEmployee.Checked)
                {
                    success = validateHourlyEmployee();

                    if (!success)
                    {
                        return;
                    }

                    //  All hourlyEmployee fields validated
                    //  Instantiate hourlyEmployee object
                    int empNum = Convert.ToInt32(txtEmployeeNumber.Text);
                    decimal hours = Convert.ToDecimal(textBox5.Text);
                    decimal rate = Convert.ToDecimal(textBox6.Text);

                    HourlyEmployee h = new HourlyEmployee(
                                        txtFirstName.Text,
                                        txtLastName.Text,
                                        empNum, hours, rate);

                    //  Calculate gross pay for HourlyEmployee
                    if (hours <= MAXNONOT)  //  Employee worked <= 40 hours
                    {
                        grossPay = hours * rate;
                    }
                    else
                    {                       //  Employee worked some overtime
                        grossPay = (MAXNONOT * rate)  +
                                  ((hours - MAXNONOT) * rate * OTRATE);
                    }

                    txtResults.Text = "HOURLY EMPLOYEE STATS:" +
                                     "\r\n" + h.displayText()  +
                                     "\r\n" + "Union Status: " +
                                     chkUnionMember.Checked    +
                                     "\r\n" + "Gross Pay: "    +
                                     grossPay.ToString("c");
                }
                else if (radPieceworkerEmployee.Checked)
                {
                    success = validatePieceworkerEmployee();

                    if (!success)
                    {
                        return;
                    }

                    //  All pieceworkerEmployee fields validated
                    //  Instantiate pieceWorkerEmployee object
                    int empNum = Convert.ToInt32(txtEmployeeNumber.Text);
                    int pieces = Convert.ToInt32(textBox5.Text);
                    decimal priceperpiece = Convert.ToDecimal(textBox6.Text);

                    PieceworkerEmployee p = new PieceworkerEmployee(
                                        txtFirstName.Text,
                                        txtLastName.Text,
                                        empNum, pieces, priceperpiece);

                    //  Calculate gross pay for PieceworkderEmployee
                    //  Assume each pieceworker works 40 hours/week
                    grossPay = pieces * priceperpiece * MAXNONOT;

                    txtResults.Text = "Pieceworker EMPLOYEE STATS:" +
                                     "\r\n" + p.displayText() +
                                     "\r\n" + "Union Status: " +
                                     chkUnionMember.Checked +
                                     "\r\n" + "Gross Pay: " +
                                     grossPay.ToString("c");
                }
            }
        }

        private bool validateCommonFields()
        {
            bool success = true;
            string errorMessage = "";

            //  Check for presence/absence of first name value in textbox
            errorMessage += Validator.IsPresent(txtFirstName.Text,
                                                txtFirstName.Tag.ToString());

            //  Check for presence/absence of last name value in textbox
            errorMessage += Validator.IsPresent(txtLastName.Text,
                                                txtLastName.Tag.ToString());

            //  Check for integer value in employee number textbox
            errorMessage += Validator.IsInt32(txtEmployeeNumber.Text,
                                              txtEmployeeNumber.Tag.ToString());

            //  Range Check for value in employee number textbox
            errorMessage += Validator.IsWithinRange(txtEmployeeNumber.Text,
                                                    txtEmployeeNumber.Tag.ToString(), 
                                                    MINEMPNUMBER, MAXEMPNUMBER);

            //  Either we have errors (errorMessage not equal to "")
            //  or we have no errors (all fields validated correctly).
            if (errorMessage.Trim() != "")
            {
                success = false;
                showMessage(errorMessage,
                            "THE FOLLOWING ERRORS OCCURRED");
            }

            return success;
        }

        public bool validateHourlyEmployee()
        {
            bool success = true;
            string errorMessage = "";

            //  Set tag values
            textBox5.Tag = "hoursworked";
            textBox6.Tag = "hourlyrate";

            //  Validate hoursworked
            errorMessage += Validator.IsDecimal(textBox5.Text, textBox5.Tag.ToString());
            errorMessage += Validator.IsWithinRange(textBox5.Text, textBox5.Tag.ToString(),
                                                    MINHOURS, MAXHOURS);

            //  Validate hourlyrate
            errorMessage += Validator.IsDecimal(textBox6.Text, textBox6.Tag.ToString());
            errorMessage += Validator.IsWithinRange(textBox6.Text, textBox6.Tag.ToString(),
                                                    MINHRATE, MAXHRATE);

            //  Either we have errors (errorMessage not equal to "")
            //  or we have no errors (all fields validated correctly).
            if (errorMessage.Trim() != "")
            {
                success = false;
                showMessage(errorMessage,
                            "THE FOLLOWING ERRORS OCCURRED");
            }

            return success;
        }

        private bool validatePieceworkerEmployee()
        {
            bool success = true;
            string errorMessage = "";

            //  Set tag values
            textBox5.Tag = "pieces";
            textBox6.Tag = "priceperpiece";

            //  Validate pieces made
            errorMessage += Validator.IsInt32(textBox5.Text, textBox5.Tag.ToString());
            errorMessage += Validator.IsWithinRange(textBox5.Text, textBox5.Tag.ToString(),
                                                    MINPIECES, MAXPIECES);

            //  Validate price per piece
            errorMessage += Validator.IsDecimal(textBox6.Text, textBox6.Tag.ToString());
            errorMessage += Validator.IsWithinRange(textBox6.Text, textBox6.Tag.ToString(),
                                                    MINPPP, MAXPPP);

            //  Either we have errors (errorMessage not equal to "")
            //  or we have no errors (all fields validated correctly).
            if (errorMessage.Trim() != "")
            {
                success = false;
                showMessage(errorMessage,
                            "THE FOLLOWING ERRORS OCCURRED");
            }

            return success;
        }

        //  Code that executes when Clear button in clicked
        void btnClear_Click(object sender, EventArgs e)
        {
            clearAndSetFocus();
        }

        private void clearAndSetFocus()
        {
            clearCommonInfo();
            makeTypeInfoInvisible();
            txtFirstName.Focus();
        }

        private void clearCommonInfo()
        {
            radHourlyEmployee.Checked       = false;
            radPieceworkerEmployee.Checked  = false;
            txtFirstName.Text               = "";
            txtLastName.Text                = "";
            txtEmployeeNumber.Text          = "";
            chkUnionMember.Checked          = false;
            txtResults.Text                 = "";
        }

        private void makeTypeInfoInvisible()
        {
            textBox5.Text       = "";
            textBox6.Text       = "";
            textBox5.Tag        = "";
            textBox6.Tag        = "";
            label5.Text         = "";
            label6.Text         = "";
            label5.Visible      = false;
            label6.Visible      = false;
            textBox5.Visible    = false;
            textBox6.Visible    = false;
        }

        private void makeTypeInfoVisible()
        {
            label5.Visible   = true;
            label6.Visible   = true;
            textBox5.Visible = true;
            textBox6.Visible = true;
        }

        //  Code that executes when Exit button in clicked
        private void btnExit_Click(object sender, EventArgs e)
        {
            exitProgramOrNot();
        }

        private void exitProgramOrNot()
        {
            DialogResult dialog = MessageBox.Show(
                "Do You Really Want To Exit The Program?",
                "EXIT NOW?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        //  Code that executes when frmCh14-16Pretest loaded into memory
        private void Form1_Load(object sender, EventArgs e)
        {
            makeTypeInfoInvisible();
        }

        //  Custom message box function
        private void showMessage(string msg, string title)
        {
            MessageBox.Show(msg, title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }

        //  Code that executes when hourly employee radio button clicked
        private void radHourlyEmployee_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Tag = "hoursworked";
            textBox6.Tag = "hourlyrate";
            label5.Text  = "Hours Worked";
            label6.Text  = "Hourly Rate";
            makeTypeInfoVisible();
        }

        //  Code that executes when pieceworker employee radio button clicked
        private void radPieceworkerEmployee_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Tag = "pieces";
            textBox6.Tag = "priceperpiece";
            label5.Text  = "# Pieces Made";
            label6.Text  = "Price/Piece";
            makeTypeInfoVisible();
        }
    }
}
