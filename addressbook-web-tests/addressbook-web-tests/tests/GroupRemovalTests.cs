using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            if (!app.Groups.IsAnyGroupPresent())
            {
                GroupData groop = new GroupData("aaa");
                groop.Header = "ddd";
                groop.Footer = "fff";

                app.Groups.Create(groop);
            }

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Remove(0);

            //System.Console.Out.Write((oldGroups.Count - 1) + "\n");
            //System.Console.Out.Write(app.Groups.GetGroupCount() + "\n");

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            //Assert.AreEqual(oldGroups.Count - 1, newGroups.Count);

            GroupData toBeRemoved = oldGroups[0];
            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
