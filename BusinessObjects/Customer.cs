/* Author: Gabor Buzasi (40338733)
 * Description: Customer class specifies the properties and fields that should be stored about a Customer
 * Date last modified: 19 Oct 2018
 */

using System;
using System.Net.Mail;

namespace BusinessObjects
{
    public class Customer
    {
        private int _customerId;
        private string _firstName;
        private string _surname;
        private string _emailAddress;
        private string _skypeId;
        private string _telephone;
        private PreferredContactEnum _preferredContact;

        /// <summary>
        /// Holds the identifier for a customer
        /// </summary>
        public int Id
        {
            get
            {
                return _customerId;
            }
            set
            {
                if (value < 10001 && value > 50000)
                {
                    throw new ArgumentException("Customer ID needs to be in the range of 10001 and 50000");
                }

                _customerId = value;
            }
        }

        /// <summary>
        /// Holds the first name of a customer
        /// </summary>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("First Name is required");
                }

                _firstName = value;
            }
        }

        /// <summary>
        /// Holds the surname of a customer
        /// </summary>
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Surname is required");
                }

                _surname = value;
            }
        }

        /// <summary>
        /// Concatenates customer's id and first name and surname for display
        /// </summary>
        public string DisplayCustomer
        {
            get
            {
                return $"{_customerId} - {_firstName} {_surname}";
            }
        }

        /// <summary>
        /// Holds the email address of a customer
        /// </summary>
        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                try
                {
                    MailAddress m = new MailAddress(value);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Please enter a valid email address");
                }

                _emailAddress = value;
            }
        }

        /// <summary>
        /// Holds the Skype username of a customer
        /// </summary>
        public string SkypeId
        {
            get
            {
                return _skypeId;
            }
            set
            {
                _skypeId = value;
            }
        }

        /// <summary>
        /// Holds the telephone number of a customer
        /// </summary>
        public string Telephone
        {
            get
            {
                return _telephone;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Please enter a phone number");
                }
                _telephone = value;
            }
        }

        /// <summary>
        /// Holds the preferred contact method for a customer: Email, Skype or Tel
        /// </summary>
        public PreferredContactEnum PreferredContact
        {
            get
            {
                return _preferredContact;
            }
            set
            {
                _preferredContact = value;
            }
        }

        /// <summary>
        /// Retrieves the preferred contact method for customer
        /// </summary>
        /// <returns></returns>
        public string getPreferredContact()
        {
            string preferredContactDetails = string.Empty;

            switch (PreferredContact)
            {
                case PreferredContactEnum.Email:
                    preferredContactDetails = $"{PreferredContact}: {EmailAddress}";
                    break;
                case PreferredContactEnum.Skype:
                    preferredContactDetails = $"{PreferredContact}: {SkypeId}";
                    break;
                case PreferredContactEnum.Tel:
                    preferredContactDetails = $"{PreferredContact}: {Telephone}";
                    break;
            }

            return preferredContactDetails;
        }

    }
}
