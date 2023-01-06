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

    public int Label(Node node)
    {
        return Label(node.Value);
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
    // Хранит деревья через словарь.
    // string означает название дерева.
    // Tree само дерево.
    private Dictionary<string, Tree> _trees;
    
    // Создает новое дерево и добавляет его в _trees.
    public void AddTree(string treeName)
    {
        Tree tree = new Tree();
        _trees.Add(treeName, tree);
    }

    // Добавляет новый узел в дерево с именем treeName.
    public void AddNode(int newValue, string treeName)
    {
        _trees[treeName].Add(newValue);
    }
    
    // Печатает список сыновей конкретного дерева.
    public void PrintTree(string treeName)
    {
        _trees[treeName].PrintTree();
    }
    
    
    // Возвращает родителя узла n из дерева T.
    public Node Parent(int n, string T)
    {
        return _trees[T].Parent(n);
    }

    public Node Parent(Node node, string T)
    {
        return _trees[T].Parent(node);
    }
    
    // Возвращает левого сына узла n дерева T.
    public Node LeftChild(int n, string T)
    {
        return _trees[T].LeftChild(n);
    }

    public Node LeftChild(Node node, string T)
    {
        return _trees[T].LeftChild(node);
    }
    
    // Возвращает правого брата узла n в дереве Т.
    public Node RightSibling(int n, string T)
    {
        return _trees[T].RightSibling(n);
    }

    public Node RightSibling(Node node, string T)
    {
        return _trees[T].RightSibling(node);
    }
    
    // Возвращает метку узла n дерева Т.
    public int Label(int n, string T)
    {
        return _trees[T].Label(n);
    }

    public int Label(Node node, string T)
    {
        return _trees[T].Label(node);
    }

    // Возвращает узел, являющимся корнем дерева Т.
    public Node Root(string T)
    {
        return _trees[T].Root;
    }
    
    // Делает дерево T пустым деревом.
    public void MakeNull(string T)
    {
        _trees[T] = new Tree();
    }
    
    

    // Конструктор.
    public Trees()
    {
        _trees = new Dictionary<string, Tree>();
    }
}


class Program
{
    static void Main()
    {
        Trees trees = new Trees();
        trees.AddTree("T1");
        
        trees.AddNode(8, "T1");
        trees.AddNode(3, "T1");
        trees.AddNode(10, "T1");
        trees.AddNode(1, "T1");
        trees.AddNode(6, "T1");
        trees.AddNode(14, "T1");
        trees.AddNode(4, "T1");
        trees.AddNode(13, "T1");
        trees.AddNode(7, "T1");
        
        
        trees.PrintTree("T1");
        Console.WriteLine();
        Node.Print(trees.Parent(8, "T1"));
        Node.Print(trees.Parent(10, "T1"));
        Node.Print(trees.LeftChild(8, "T1"));
        Node.Print(trees.RightSibling(3, "T1"));
        Console.WriteLine(trees.Label(8, "T1"));
        Console.WriteLine(trees.Label(666, "T1"));
        Node.Print(trees.Root("T1"));
        trees.MakeNull("T1");
        trees.PrintTree("T1");
    }
}