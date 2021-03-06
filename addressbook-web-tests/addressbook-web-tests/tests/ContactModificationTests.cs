﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
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

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeUpdated = oldContacts[0];

            app.Contacts.Modify(toBeUpdated, newData);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            toBeUpdated.FirstName = newData.FirstName;
            toBeUpdated.LastName = newData.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == toBeUpdated.Id)
                {
                    Assert.AreEqual(newData.FirstName, contact.FirstName);
                    Assert.AreEqual(newData.LastName, contact.LastName);
                }
            }
        }
    }
}
