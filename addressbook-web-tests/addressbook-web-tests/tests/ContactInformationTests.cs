﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            // verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }

        [Test]
        public void TestFullContactInformation()
        {
            string textFromViewPage = app.Contacts.GetContactInformationFromViewPage(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);
            string textFromEditPage = app.Contacts.GetContactTextInformationFromEditForm(fromForm);

            // verification
            Assert.AreEqual(textFromViewPage, textFromEditPage);
        }
    }
}
