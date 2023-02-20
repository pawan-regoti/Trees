using Nodes;

namespace BinaryTrees;

public class BinaryTree
{
    public Node? Root { get; set; }

    public bool Insert(int value)
    {
        var newNode = new Node
        {
            Data = value
        };

        if (Root == null) //Tree is empty
        {
            Root = newNode;
            return true;
        }

        var lastVisitedNode = Root;
        var currentNode = Root;
        while (currentNode != null)
        {
            // copy current node to last visited node
            lastVisitedNode = currentNode;

            // Is new node in left tree?
            if (newNode.Data < currentNode.Data)
            {
                currentNode = currentNode.LeftNode;
            }
            // Is new node in right tree?
            else if (newNode.Data > currentNode.Data)
            {
                currentNode = currentNode.RightNode;
            }
            else
            {
                //Exist same value
                return false;
            }
        }

        if (newNode.Data < lastVisitedNode.Data)
        {
            lastVisitedNode.LeftNode = newNode;
        }
        else
        {
            lastVisitedNode.RightNode = newNode;
        }

        return true;
    }

    public Node? Search(int value)

    {
        return Search(value, Root);
    }

    private Node? Search(int value, Node? parent)

    {
        if (parent == null)
            return null;

        if (value == parent.Data)
            return parent;

        if (value < parent.Data)
        {
            return Search(value, parent.LeftNode);
        }

        return Search(value, parent.RightNode);
    }

    public void Delete(int value)
    {
        Root = Delete(Root, value);
    }

    private Node? Delete(Node? parent, int value)
    {
        if (parent == null)
            return parent;

        if (value < parent.Data)
            parent.LeftNode = Delete(parent.LeftNode, value);
        else if (value > parent.Data)
            parent.RightNode = Delete(parent.RightNode, value);

        // if the value is the same as the parent's value, then this node is to be deleted
        else
        {
            // the node with one or no child
            if (parent.LeftNode == null)
                return parent.RightNode;

            else if (parent.RightNode == null)
                return parent.LeftNode;

            // node with two children: Get the inorder successor (smallest in the right subtree)
            parent.Data = MinValueInRightSubTree(parent.RightNode);

            // Delete the inorder successor
            parent.RightNode = Delete(parent.RightNode, parent.Data);
        }

        return parent;
    }

    private int MinValueInRightSubTree(Node node)

    {
        var minValue = node.Data;

        while (node.LeftNode != null)
        {
            minValue = node.LeftNode.Data;
            node = node.LeftNode;
        }

        return minValue;
    }

    public int GetDepth()

    {
        return GetDepth(Root);
    }

    private int GetDepth(Node? parent)

    {
        return parent == null ? 0 : Math.Max(GetDepth(parent.LeftNode), GetDepth(parent.RightNode)) + 1;
    }

    public void PreOrder(Node? parent)
    {
        if (parent == null)
            return;

        Console.Write(parent.Data + " ");
        PreOrder(parent.LeftNode);
        PreOrder(parent.RightNode);
    }

    public void InOrder(Node? parent)

    {
        if (parent == null)
            return;

        InOrder(parent.LeftNode);
        Console.Write(parent.Data + " ");
        InOrder(parent.RightNode);
    }

    public void PostOrder(Node? parent)

    {
        if (parent == null)
            return;

        PostOrder(parent.LeftNode);
        PostOrder(parent.RightNode);
        Console.Write(parent.Data + " ");
    }
}
