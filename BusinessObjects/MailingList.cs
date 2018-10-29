/* Author: Gabor Buzasi (40338733)
 * Description: MailingList class contains an
 * object (List) to store the entered customers in memory
 * and relevant functions in order to manipulate that list
 * 
 * Date last modified: 29 Oct 2018
 */

using System.Collections.Generic;
using System.Linq;

namespace BusinessObjects
{
    public class MailingList
    {
        private List<Customer> _list = new List<Customer>();

        public void Add(Customer newCustomer)
        {
            _list.Add(newCustomer);
        }

        public Customer Find(int id)
        {
            return _list.FirstOrDefault(x => x.Id == id);
        }

        public void Delete(int id)
        {
            Customer c = this.Find(id);
            if (c != null)
            {
                _list.Remove(c);
            }
        }

        public List<int> Ids
        {
            get
            {
                return _list.Select(x => x.Id).ToList();
            }
           
        }

        public List<Customer> Customers
        {
            get
            {
                return _list;
            }
        }
    }
}
