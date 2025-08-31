using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PROG7312_ICE_TASK_2.Models;

namespace PROG7312_ICE_TASK_2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FamilyTree()
        {
            var rootNode = RoyalFamilyTree();
            return View("FamilyTree",rootNode);
        }

        private FamilyTreeNode<FamilyMember> RoyalFamilyTree() 
        {
            // King Charles III - Root Node
            FamilyMember kingCharlesIII = new FamilyMember
            {
                Name = "King Charles III",
                DateOfBirth = new DateTime(1948, 11, 14),
                isAlive = true
            };
            var rootNode = new FamilyTreeNode<FamilyMember> { Data = kingCharlesIII };

            // Adding King Charles first Son, William 
            var princeWilliam = new FamilyMember { Name = "Prince William", DateOfBirth = new DateTime(1982, 6, 21), isAlive = true };

            // Adding King Charles second Son, Harry 
            var princeHarry = new FamilyMember { Name = "Prince Harry", DateOfBirth = new DateTime(1984, 9, 15), isAlive = true };

            var williamNode = new FamilyTreeNode<FamilyMember> { Data = princeWilliam, Parent = rootNode };
            var harryNode = new FamilyTreeNode<FamilyMember> { Data = princeHarry, Parent = rootNode };

            // Adding William and Harry to the rootNode (Father)
            rootNode.AddChild(williamNode);
            rootNode.AddChild(harryNode);
            
            // Adding William's children 
            var princeGeorge = new FamilyMember { Name = "Prince George of Wales", DateOfBirth = new DateTime(2013,07,22), isAlive = true };
            var princessCharlotte = new FamilyMember { Name = "Princess Charlotte of Wales", DateOfBirth = new DateTime(2015, 05, 02), isAlive = true };
            var princeLouis = new FamilyMember { Name = "Prince Louis of Wales", DateOfBirth = new DateTime(2018, 4, 23), isAlive = true };

            williamNode.AddChild(new FamilyTreeNode<FamilyMember> { Data = princeGeorge});
            williamNode.AddChild(new FamilyTreeNode<FamilyMember> { Data = princessCharlotte });
            williamNode.AddChild(new FamilyTreeNode<FamilyMember> { Data = princeLouis });

            // Adding Harry's children
            var princeArchie = new FamilyMember { Name = "Prince Archie of Sussex", DateOfBirth = new DateTime(2019, 05, 06), isAlive = true };
            var princessLilibet = new FamilyMember { Name = "Princess Lilibet of Sussex", DateOfBirth = new DateTime(2021, 06, 04), isAlive = true };
            
            harryNode.AddChild(new FamilyTreeNode<FamilyMember> { Data = princeArchie });
            harryNode.AddChild(new FamilyTreeNode<FamilyMember> { Data = princessLilibet });

            return rootNode;
        }

       
    }
}

//Reference: https://metro.co.uk/2024/11/14/full-list-royal-familys-birthday-dates-king-charles-turns-76-21986352/#:~:text=The%20King%20and%20Queen%20are%20in%20their%2070s%2C,17%2C%201947%20and%20is%20currently%2077%20years%20old.
