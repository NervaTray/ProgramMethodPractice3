// Вариант 40
using System;

// Класс описывает одно дерево двоичного поиска.
class Tree
{
    private int index;
    private List<Node[]> _brotherList;

    // Добавление нового узла в дерево.
    public void Add(int newValue)
    {
        // Проверка на существование вносимого элемента в дереве.
        for (int i = 0; i < _brotherList.Count; i++)
        {
            if (_brotherList[i][0].Value == newValue) return;
        }
        
        // Создается новый узел.
        Node node = new Node(index, newValue);
        Node[] nodes = new Node[3];
        nodes[0] = node;
        index++;
        _brotherList.Add(nodes);
        
        // В случае, если список пуст, вводимый элемент становится корнем дерева.
        if (_brotherList.Count == 1) return;

        // Добавление нового элемента
        Node currentNode = _brotherList[0][0];
        for (int i = 0; i < _brotherList.Count; i++)
        {
            if (currentNode.Value == _brotherList[i][0].Value)
            {
                if (_brotherList[i][0].Value > node.Value)
                {
                    if (_brotherList[i][1] == null) _brotherList[i][1] = node;
                    else currentNode = _brotherList[i][1];
                }
                else
                {
                    if (_brotherList[i][2] == null) _brotherList[i][2] = node;
                    else currentNode = _brotherList[i][2];
                }
            }
        }
    }

    // Вывод списка сыновей.
    public void PrintTree()
    {
        string temp;
        for (int i = 0; i < _brotherList.Count; i++)
        {
            Console.Write((i + 1) + ": ");
            for (int j = 0; j < 3; j++)
            {

                if (_brotherList[i][j] == null) temp = "NULL";
                else temp = _brotherList[i][j].Value.ToString();
                
                switch (j)
                {
                    case 0:
                        Console.Write("Parent Node: {0}\t", temp);
                        break;
                    case 1:
                        Console.Write("Left Child Node: {0}\t", temp);
                        break;
                    case 2:
                        Console.Write("Right Child Node: {0}\t", temp);
                        break;
                }
                
            }
            Console.WriteLine();
        }
    }


    public Tree()
    {
        index = 1;
        _brotherList = new List<Node[]>();
    }
    
}

// Описывает узел.
class Node
{
    private int _index;
    private int _value;

    public int Value => _value;

    public Node(int index, int value)
    {
        _index = index;
        _value = value;
    }
}

// Данный класс работает с несколькими деревьями.
class Trees
{
    
}


class Program
{
    static void Main()
    {
        Tree tree = new Tree();
        
        tree.Add(8);
        tree.Add(3);
        tree.Add(10);
        tree.Add(1);
        tree.Add(6);
        tree.Add(14);
        tree.Add(4);
        tree.Add(13);
        tree.Add(7);
        
        tree.PrintTree();
    }
}