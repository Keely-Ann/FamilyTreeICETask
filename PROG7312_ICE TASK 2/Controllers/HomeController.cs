using System.Diagnostics;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using PROG7312_ICE_TASK_2.Models;

namespace PROG7312_ICE_TASK_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly FamilyTreeNode<FamilyMember> _familyTreeNode;

        public HomeController() 
        {
            _familyTreeNode = BuildFamilyTree();
        }

        public IActionResult Index()
        {
            return View(_familyTreeNode);
        }

        // Displaying the BFS and DFS search alorithms
        public IActionResult SearchAlgorithm()
        {
            var tree = new Tree<FamilyMember> { Root = _familyTreeNode };

            var searchView = new FamilySearchAlgorithm
            {
                BFSResults = tree.BreadthFirstSearch().Select(x => x.Name).ToList(),
                DFSResults = tree.DepthFirstSearch().Select(x => x.Name).ToList()
            };
            return View(searchView);
        }

        // Displaying the tree structure and the search bar
        public IActionResult FamilyTreeDisplay()
        {
            return View(_familyTreeNode);
        }

        // Displaying full Family tree structure
        public IActionResult FamilyTree()
        {
            var rootNode = BuildFamilyTree();
            return View("FamilyTree", rootNode);
        }

        // Searching by name 
        [HttpGet]
        public IActionResult Search(string name)
        {
            ViewBag.SearchInput = name;

            if (string.IsNullOrEmpty(name))
            return View("FamilyTreeDisplay", _familyTreeNode); 

            var found = FindMember(_familyTreeNode, name);

            if (found == null)
            {
                ViewBag.Message = $"No results found for '{name}'.";
                return View("FamilyTreeDisplay", _familyTreeNode);
            }

            int position = GetThronePosition(found);
            ViewBag.HighlightNode = found;
            ViewBag.Position = position;
            return View("FamilyTreeDisplay", _familyTreeNode);

        }

        // Finding the royal family member
        private FamilyTreeNode<FamilyMember> FindMember(FamilyTreeNode<FamilyMember> node, string name)
        {
            if (node.Data.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                return node;

            foreach (var child in node.Children)
            {
                var found = FindMember(child, name);
                if (found != null)
                    return found;
            }

            return null;
        }

        // Adding the Family members and creating the tree structure
        private FamilyTreeNode<FamilyMember> BuildFamilyTree()
        {
               //King Charles III - Root Node
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
                var princeGeorge = new FamilyMember { Name = "Prince George of Wales", DateOfBirth = new DateTime(2013, 07, 22), isAlive = true };
                var princessCharlotte = new FamilyMember { Name = "Princess Charlotte of Wales", DateOfBirth = new DateTime(2015, 05, 02), isAlive = true };
                var princeLouis = new FamilyMember { Name = "Prince Louis of Wales", DateOfBirth = new DateTime(2018, 4, 23), isAlive = true };

                williamNode.AddChild(new FamilyTreeNode<FamilyMember> { Data = princeGeorge });
                williamNode.AddChild(new FamilyTreeNode<FamilyMember> { Data = princessCharlotte });
                williamNode.AddChild(new FamilyTreeNode<FamilyMember> { Data = princeLouis });

                // Adding Harry's children
                var princeArchie = new FamilyMember { Name = "Prince Archie of Sussex", DateOfBirth = new DateTime(2019, 05, 06), isAlive = true };
                var princessLilibet = new FamilyMember { Name = "Princess Lilibet of Sussex", DateOfBirth = new DateTime(2021, 06, 04), isAlive = true };

                harryNode.AddChild(new FamilyTreeNode<FamilyMember> { Data = princeArchie });
                harryNode.AddChild(new FamilyTreeNode<FamilyMember> { Data = princessLilibet });

                return rootNode;
        }

        // Getting the throne position
        private int GetThronePosition(FamilyTreeNode<FamilyMember> node)
        {
            var list = new List<FamilyTreeNode<FamilyMember>>();
            CreateList(_familyTreeNode, list);
            int position = list.IndexOf(node) +1 ;
            return position;
        }

        // Creating a list 
        private void CreateList(FamilyTreeNode<FamilyMember> node, List<FamilyTreeNode<FamilyMember>> list)
        {
            if (node.Data.isAlive)
            {
                list.Add(node);
            }

            foreach (var child in node.Children)
            {
                CreateList(child, list);
            }
        }
    }
}

// Royal Information Reference: https://metro.co.uk/2024/11/14/full-list-royal-familys-birthday-dates-king-charles-turns-76-21986352/#:~:text=The%20King%20and%20Queen%20are%20in%20their%2070s%2C,17%2C%201947%20and%20is%20currently%2077%20years%20old.
