﻿// Вариант 40
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
    public void Add(Node newNode)
    {
        // Проверка на существование вносимого элемента в дереве.
        // Если элемент с подобным значением существует, то новый узел не создается.
        for (int i = 0; i < _brotherList.Count; i++)
        {
            if (_brotherList[i][0].Value == newNode.Value) return;
        }
        
        // Создается новый узел.
        //Node node = new Node(index, newValue);
        Node[] nodes = new Node[3];
        nodes[0] = newNode;
        nodes[1] = Lyambda;
        nodes[2] = Lyambda;
        index++;
        _brotherList.Add(nodes);
        
        // В случае, если список пуст, вводимый элемент становится корнем дерева.
        if (_brotherList.Count == 1)
        {
            _root = newNode;
            return;
        }

        // Добавление нового элемента в зависимости.
        Node currentNode = _brotherList[0][0];
        for (int i = 0; i < _brotherList.Count; i++)
        {
            if (currentNode.Value == _brotherList[i][0].Value)
            {
                if (_brotherList[i][0].Value > newNode.Value)
                {
                    if (_brotherList[i][1] == Lyambda) _brotherList[i][1] = newNode;
                    else currentNode = _brotherList[i][1];
                }
                else
                {
                    if (_brotherList[i][2] == Lyambda) _brotherList[i][2] = newNode;
                    else currentNode = _brotherList[i][2];
                }
            }
        }
    }

    public void Add(int newValue)
    {
        Node node = new Node(index, newValue);
        Add(node);
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
    // Если параметр label равен true, то выводятся также метки узлов.
    public void PrintTree(bool label = false)
    {
        string temp;
        for (int i = 0; i < _brotherList.Count; i++)
        {
            Console.Write((i + 1) + ": ");
            for (int j = 0; j < 3; j++)
            {

                if (_brotherList[i][j] == Lyambda) temp = "NULL";
                else
                {
                    temp = _brotherList[i][j].Value.ToString();
                    if (label) temp += " Label: " + _brotherList[i][j].label.ToString();
                }
                
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
    
    
    // Удаляет узел со значением n.
    // При удалении происходит прямой обход поддерева узла со значением n.
    public void DeleteNode(int? n)
    {
        // Если узла со значением n не существует, то происходит выход из метода.
        if (FindNode(n) == Lyambda) return;

        // Обнуление меток для всех узлов в дереве.
        for (int i = 0; i < _brotherList.Count; i++)
        {
            _brotherList[i][0].label = 0;
        }
        
        // Сюда заносятся узлы, которые будут заново внесены в дерево.
        Queue<Node> nodeQueue = new Queue<Node>();
        // Стек для обхода дерева в глубину.
        Stack<Node> nodeStack = new Stack<Node>();

        Node nStart = FindNode(n);
        nodeStack.Push(nStart);

        // Делает обход дерева в глубину со стартовой позицией n.
        // Узел со значением n остается со значением label = 1.
        // Остальные узлы, являющиеся поддеревом узла со значением n имеют label = 2.
        while (nodeStack.Count > 0)
        {
            Node NCurrent = nodeStack.Peek();
            NCurrent.label = 1;

            if (LeftChild(NCurrent) != Lyambda && LeftChild(NCurrent).label != 2)
            {
                nodeStack.Push(LeftChild(NCurrent));
            }
            else
            {
                
                NCurrent.label = 2;
                nodeQueue.Enqueue(nodeStack.Pop());
                if (RightSibling(NCurrent) != Lyambda && RightSibling(NCurrent).label != 2) nodeStack.Push(RightSibling(NCurrent));
            }

            if (nStart == nodeStack.Peek()) break;

        }

        // Удаляет все узлы с label = 2 из списка сыновей.
        for (int i = 0; i < _brotherList.Count; i++)
        {
            if (_brotherList[i][0].label == 2 || _brotherList[i][0].label == 1)
            {
                _brotherList.RemoveAt(i);
                i--;
            }
        }

        // Убирает зависимость удаляемого узла от родителя.
        if (Parent(nStart) != Lyambda)
        {
            Node temp = Parent(nStart);
            for (int i = 0; i < _brotherList.Count; i++)
            {
                if (temp.Value == _brotherList[i][0].Value)
                {
                    if (nStart.Value == _brotherList[i][1].Value) _brotherList[i][1] = Lyambda;
                    else _brotherList[i][2] = Lyambda;
                }
            }
        }

        while (nodeQueue.Count > 0)
        {
            Add(nodeQueue.Dequeue());
        }

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
    public void PrintTree(string treeName, bool label = false)
    {
        _trees[treeName].PrintTree(label);
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
        // Trees trees = new Trees();
        // trees.AddTree("T1");
        //
        // trees.AddNode(8, "T1");
        // trees.AddNode(3, "T1");
        // trees.AddNode(10, "T1");
        // trees.AddNode(1, "T1");
        // trees.AddNode(6, "T1");
        // trees.AddNode(14, "T1");
        // trees.AddNode(4, "T1");
        // trees.AddNode(13, "T1");
        // trees.AddNode(7, "T1");
        //
        // trees.PrintTree("T1", true);

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
        
        tree.DeleteNode(3);
        tree.PrintTree(true);
        

    }
}