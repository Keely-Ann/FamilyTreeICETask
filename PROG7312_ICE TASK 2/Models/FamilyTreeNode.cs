namespace PROG7312_ICE_TASK_2.Models
{
    public class FamilyTreeNode<T>
    {
        public T Data { get; set; }
        public FamilyTreeNode<T> Parent { get; set; }
        public List<FamilyTreeNode<T>> Children { get; set; }
            = new List<FamilyTreeNode<T>>();

        public void AddChild(FamilyTreeNode<T> child)
        {
            child.Parent = this;
            Children.Add(child);
        }
    }
}
