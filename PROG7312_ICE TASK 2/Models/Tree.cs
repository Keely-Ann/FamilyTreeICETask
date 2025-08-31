namespace PROG7312_ICE_TASK_2.Models
{
     class Tree<T>
    {
        public FamilyTreeNode<T> Root { get; set; }

        public void PrintTree(FamilyTreeNode<T> node, string indent = "", bool last = true)
        {
            if (node == null) return;
            Console.WriteLine($"{indent} +- {node.Data}");
            // Trying to make it look pretty
            indent += last ? "  " : "| ";
            for (int i = 0; i < node.Children.Count; i++)
            {
                PrintTree(node.Children[i], indent, i == node.Children.Count - 1);
            }
        }

        public FamilyTreeNode<T> FindNode(FamilyTreeNode<T> node, string name)
        {
            if (node == null) return null;
            if (node.Data is FamilyMember member && member.Name == name)
                return node;
            foreach (var child in node.Children)
            {
                var result = FindNode(child, name);
                // Loop will break here
                if (result != null) return result;
            }
            // Can't find what its looking for, the following will be returned
            return null;
        }

        public void PrintTree(FamilyTreeNode<T> node, FamilyTreeNode<T> highlightNode, string indent = "", bool last = true)
        {
            if (node == null) return;
            if (node.Equals(highlightNode))
                Console.WriteLine($"{indent} +- [{node.Data}]");
            else
                Console.WriteLine($"{indent} +- {node.Data}");
            indent += last ? "  " : "| ";
            for (int i = 0; i < node.Children.Count; i++)
            {
                PrintTree(node.Children[i], highlightNode, indent, i == node.Children.Count - 1);
            }
        }

        //Breadth-First Search (BFS)
        public List<T> BreadthFirstSearch()
        {
            if (Root == null)
            {
                return new List<T>();
            }

            var visited = new List<T>();
            // Use a Queue for BFS
            var queue = new Queue<FamilyTreeNode<T>>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                // Get the net node in the queue
                FamilyTreeNode<T> currentNode = queue.Dequeue();
                // Visit it 
                visited.Add(currentNode.Data);

                // Add all of its children to the back of the queue
                foreach (var child in currentNode.Children)
                {
                    queue.Enqueue(child);
                }
            }
            return visited;
        }

        // Helper method for the recursive DFS logic 
        private void DFSHelper(FamilyTreeNode<T> node, List<T> visited)
        {
            if (node == null) return;

            // Visit the current node
            visited.Add(node.Data);

            // Recursively visit each child to go deeper into the tree
            foreach (var child in node.Children)
            {
                DFSHelper(child, visited);
            }
        }

        // Depth-First Search (DFS)
        public List<T> DepthFirstSearch()
        {
            if (Root == null)
            {
                return new List<T>();
            }
            var visited = new List<T>();
            // Start the recursive search from the root
            DFSHelper(Root, visited);
            return visited;
        }

    }
}
