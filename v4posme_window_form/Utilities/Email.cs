namespace DevExpress.UITemplates.Collection.Utilities {
    using System;

    public static class Email {
        public static bool Validate(string email) {
            if(string.IsNullOrEmpty(email))
                return false;
            if(email.Length < 3)
                return false;
            if(email.IndexOf("@", 0) < 0)
                return false;
            try {
                new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch(FormatException) {
                return false;
            }
        }
        public static bool Validate(string email, out string error) {
            error = string.Empty;
            if(string.IsNullOrEmpty(email)) {
                error = "Empty address.";
                return false;
            }
            if(email.Length < 3) {
                error = "Incomplete address.";
                return false;
            }
            if(email.IndexOf("@", 0) < 0) {
                error = "Email addresses must include the @ character.";
                return false;
            }
            try {
                new System.Net.Mail.MailAddress(email);
            }
            catch(FormatException e) {
                error = e.Message;
            }
            return string.IsNullOrEmpty(error);
        }
    }
}
