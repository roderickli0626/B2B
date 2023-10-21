using PayPal.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SendEmail_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;

            string email = TxtEmail.Text.Trim();

            // Validate email address
            if (!IsValidEmail(email))
            {
                ServerValidator.ErrorMessage = "Invalid email address";
                ServerValidator.IsValid = false;
                return;
            }

            // Check if email exists in the database
            if (!IsEmailExists(email))
            {
                ServerValidator.ErrorMessage = "Email address does not exist.";
                ServerValidator.IsValid = false;
                return;
            }

            // Generate a unique token for password reset
            string resetToken = Guid.NewGuid().ToString();

            // Store the token and its associated user in the database
            StoreResetToken(email, resetToken);

            // Send the password reset email
            SendResetPasswordEmail(email, resetToken);

            ServerValidator.ErrorMessage = "Password reset email sent. Please check your email.";
            ServerValidator.IsValid = false;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool IsEmailExists(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["B2BConnectionString"].ConnectionString;
            string query = "SELECT COUNT(*) FROM Host WHERE Email = @Email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void StoreResetToken(string email, string resetToken)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["B2BConnectionString"].ConnectionString;
            string query = "UPDATE Host SET ResetToken = @ResetToken, ResetTokenExpiry = DATEADD(HOUR, @ExpiryHours, GETDATE()) WHERE Email = @Email";

            int expiryHours = 1; // Set the desired expiration time in hours

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ResetToken", resetToken);
                    command.Parameters.AddWithValue("@ExpiryHours", expiryHours);
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void SendResetPasswordEmail(string email, string resetToken)
        {
            // Compose the email
            string subject = "Reset Your Password";
            string body = "<a href='http://www.bnbhosts.it/ResetPassword.aspx?email=" + email + "&token=" + resetToken + "' class='btn btn-success btn-lg'>Reset Password</a>";

            // Configure the SMTP client
            //SmtpClient smtpClient = new SmtpClient("localhost", 1025);
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("krandall2005@gmail.com", "fyjlmiowttdaovfi");

            // Create the email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("Krandall2005@gmail.com", "BnB Host");
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.High;

            try
            {
                // Send the email
                smtpClient.Send(mailMessage);
                Console.WriteLine("Reset password email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending reset password email: " + ex.Message);
            }
        }
    }
}