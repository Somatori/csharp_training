using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        private List<ContactData> contactCache = null;

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(int v, ContactData newData)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(v);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int v)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(v);
            RemoveContact();
            manager.Navigator.GoToHomePage();
            return this;
        }


        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            IList<IWebElement> elements = driver.FindElements(By.XPath("//img[@alt='Edit']"));
            elements[index + 1].Click();

            //driver.FindElement(By.XPath("(//img[@alt='edit'])[" + (index + 1) + "]")).Click();
            //driver.FindElement(By.XPath("(//img[@alt='EDIT'])[" + (v + 1) + "]")).Click();

            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.LastName);
            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }

        public bool IsAnyContactPresent()
        {
            manager.Navigator.GoToHomePage();
            return IsElementPresent(By.Name("selected[]"));
        }

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name='entry']"));

                foreach (IWebElement element in elements)
                {
                    IList<IWebElement> cells = element.FindElements(By.TagName("td"));

                    contactCache.Add(new ContactData(cells[2].Text, cells[1].Text));
                    //System.Console.Out.Write(cells[2].Text + cells[1].Text + "\n");
                }
            }

            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("tr[name='entry']")).Count;
        }
    }
}
