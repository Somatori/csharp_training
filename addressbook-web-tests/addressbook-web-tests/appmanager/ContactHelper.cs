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
            //IList<IWebElement> elements = driver.FindElements(By.XPath("//img[@alt='Edit']"));
            //elements[index].Click();
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper OpenContactViewPage(int index)
        {
            //IList<IWebElement> elements = driver.FindElements(By.XPath("//img[@alt='Edit']"));
            //elements[index].Click();
            driver.FindElement(By.XPath("(//img[@alt='Details'])[" + (index + 1) + "]")).Click();
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

                    contactCache.Add(new ContactData(cells[2].Text, cells[1].Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                    //System.Console.Out.Write(cells[2].Text + cells[1].Text + ", id=" + contact.Id + "\n");
                }
            }

            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("tr[name='entry']")).Count;
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones
            };
        }

        public string GetContactInformationFromViewPage(int index)
        {
            manager.Navigator.GoToHomePage();
            OpenContactViewPage(index);
            string content = driver.FindElement(By.Id("content")).Text;
            //System.Console.Out.Write(content);
            return content;

        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };
        }

        public string GetContactTextInformationFromEditForm(ContactData contact)
        {
            string fullName = null;
            string address = null;
            string homePhone = null;
            string mobilePhone = null;
            string workPhone = null;

            if (contact.FirstName.Length == 0 && contact.LastName.Length == 0)
            {
                fullName = null;
            }
            else
            {
                fullName = (contact.FirstName + " " + contact.LastName).Trim() + "\r\n";
            }

            if (contact.Address.Length == 0)
            {
                address = "\r\n";
            }
            else
            {
                address = contact.Address + "\r\n\r\n";
            }

            if (contact.HomePhone.Length != 0)
            {
                homePhone = "H: " + contact.HomePhone + "\r\n";
            }

            if (contact.MobilePhone.Length != 0)
            {
                mobilePhone = "M: " + contact.MobilePhone + "\r\n";
            }

            if (contact.WorkPhone.Length != 0)
            {
                workPhone = "W: " + contact.WorkPhone +"\r\n";
            }
            
            string contactContent = (fullName + address + homePhone + mobilePhone + workPhone).Trim();
            //System.Console.Out.Write(contactContent);
            return contactContent;
        }
    }
}
