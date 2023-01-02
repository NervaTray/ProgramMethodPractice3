// Вариант 40
using System;

// Класс описывает одно дерево двоичного поиска.
class Tree
{
    private int index;
    private List<Node[]> _brotherList;
    private Node Lyambda;
    private Node _root;

    // Возвращает корень дерева.
    public Node Root => _root;

    // Добавление нового узла в дерево.
    public void Add(int newValue)
    {
        // Проверка на существование вносимого элемента в дереве.
        // Если элемент с подобным значением существует, то новый узел не создается.
        for (int i = 0; i < _brotherList.Count; i++)
        {
            if (_brotherList[i][0].Value == newValue) return;
        }
        
        // Создается новый узел.
        Node node = new Node(index, newValue);
        Node[] nodes = new Node[3];
        nodes[0] = node;
        nodes[1] = Lyambda;
        nodes[2] = Lyambda;
        index++;
        _brotherList.Add(nodes);
        
        // В случае, если список пуст, вводимый элемент становится корнем дерева.
        if (_brotherList.Count == 1)
        {
            _root = node;
            return;
        }

        // Добавление нового элемента в зависимости.
        Node currentNode = _brotherList[0][0];
        for (int i = 0; i < _brotherList.Count; i++)
        {
            if (currentNode.Value == _brotherList[i][0].Value)
            {
                if (_brotherList[i][0].Value > node.Value)
                {
                    if (_brotherList[i][1] == Lyambda) _brotherList[i][1] = node;
                    else currentNode = _brotherList[i][1];
                }
                else
                {
                    if (_brotherList[i][2] == Lyambda) _brotherList[i][2] = node;
                    else currentNode = _brotherList[i][2];
                }
            }
        }
    }

    // Функция возвращает родителя узла со значение n. 
    public Node Parent(int? n)
    {
        for (int i = 0; i < _brotherList.Count; i++)
        {
            if (_brotherList[i][1].Value == n || _brotherList[i][2].Value == n) return _brotherList[i][0];
        }

        return Lyambda;
    }

    // Версия метода Parent с параметром типа Node.
    public Node Parent(Node node)
    {
        return Parent(node.Value);
    }
    
    // Возвращает левого сына узла со значением n.
    public Node LeftChild(int? n)
    {
        for (int i = 0; i < _brotherList.Count; i++)
        {
            if (_brotherList[i][0].Value == n) return _brotherList[i][1];
        }

        return Lyambda;
    }

    public Node LeftChild(Node node)
    {
        return LeftChild(node.Value);
    }

    // Возвращает правого брата узла со значением n.
    public Node RightSibling(int? n)
    {
        for (int i = 0; i < _brotherList.Count; i++)
        {
            if (_brotherList[i][1].Value == n) return _brotherList[i][2];
        }

        return Lyambda;
    }

    public Node RightSibling(Node node)
    {
        return RightSibling(node.Value);
    }
    
    // Возвращает метку узла.
    // У лямбды метка равна -1.
    public int Label(int? n)
    {
        return FindNode(n).label;
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

                if (_brotherList[i][j] == Lyambda) temp = "NULL";
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

    // Находит узел со значением n.
    public Node FindNode(int? n)
    {
        for (int i = 0; i < _brotherList.Count; i++)
            if (_brotherList[i][0].Value == n)
                return _brotherList[i][0];

        return Lyambda;
    }

    public Tree()
    {
        index = 1;
        _brotherList = new List<Node[]>();
        Lyambda = new Node(0, null, -1);
        _root = Lyambda;
    }
    
}

// Описывает узел.
class Node
{
    private int _index;
    private int? _value;
    public int label;

    public int? Value => _value;

    // Конструктор. 
    public Node(int index, int? value, int label = 0)
    {
        _index = index;
        _value = value;
        this.label = label;
    }

    // Печатает информацию об узле.
    public static void Print(Node node)
    {
        Console.WriteLine("Value: {0}; index: {1}; label: {2}.", node._value, node._index, node.label);
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
        Console.WriteLine();
        Node.Print(tree.Root);
        Console.WriteLine(tree.Label(tree.Root.Value));
        

    }
}