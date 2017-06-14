using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            if (!app.Contacts.IsAnyContactPresent())
            {
                ContactData contact = new ContactData("John", "Johnson");

                app.Contacts.Create(contact);
            }

            ContactData newData = new ContactData("Robert", "Robertson");

            app.Contacts.Modify(0, newData);
        }
    }
}
