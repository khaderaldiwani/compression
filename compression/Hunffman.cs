using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compression
{
    class PriorityQueue<T>
    {
        private List<(T Item, int Priority)> _items = new List<(T, int)>();

        public int Count => _items.Count;

        public void Enqueue(T item, int priority)
        {
            _items.Add((item, priority));
            _items.Sort((a, b) => a.Priority.CompareTo(b.Priority));
        }

        public T Dequeue()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            T item = _items[0].Item;
            _items.RemoveAt(0);
            return item;
        }
    }

    class Node
    {
        public Node leftNode;
        public Node rightNode;
        public byte value;
        public int repeate;


        public Node(byte value, int repeate = 0, Node leftNode = null, Node rightNode = null)
        {
            this.value = value;
            this.repeate = repeate;
            this.leftNode = leftNode;
            this.rightNode = rightNode;

        }
        public bool isLeaf => leftNode == null && rightNode == null;
    }
    class Hunffman
    {
        Node root;
        Dictionary<byte, string> dictionaryCodes = new Dictionary<byte, string>();


        public Dictionary<byte, int> calRepeate(byte[] input)
        {
            Dictionary<byte, int> dictionary = new Dictionary<byte, int>();
            foreach (byte c in input)
            {
                if (!dictionary.ContainsKey(c))
                {
                    dictionary[c] = 0;
                }
                dictionary[c]++;
            }
            return dictionary;
        }
        public Node buildTree(Dictionary<byte, int> dictionaryRepeate)
        {
            PriorityQueue<Node> p = new PriorityQueue<Node>();
            Dictionary<byte, int> repeate = dictionaryRepeate;
            foreach (var d in repeate)
            {
                p.Enqueue(new Node(d.Key, d.Value), d.Value);
            }

            while (p.Count > 1)
            {
                Node left = p.Dequeue();
                Node right = p.Dequeue();
                Node result = new Node(0, left.repeate + right.repeate, left, right);
                p.Enqueue(result, result.repeate);
            }
            return p.Dequeue();
        }
        public void GenerateCodes(Node root, string code)
        {

            if (root == null)
                return;
            if (root.isLeaf)
            {
                dictionaryCodes[root.value] = code;
            }

            GenerateCodes(root.leftNode, code + "0");
            GenerateCodes(root.rightNode, code + "1");

        }
        public string Compress(byte[] input)
        {
            var dictionaryRepeate = calRepeate(input);
            root = buildTree(dictionaryRepeate);
            GenerateCodes(root, "");
            StringBuilder result = new StringBuilder();
            foreach (byte c in input)
            {
                result.Append(dictionaryCodes[c]);
            }
            return result.ToString();
        }

        public BitArray Tobitarray(byte[] data)
        {

            List<bool> bits = new List<bool>();
            foreach (byte b in data)
            {
                string code = dictionaryCodes[b];
                bits.AddRange(code.Select(c => c == '1'));
            }
            return new BitArray(bits.ToArray());
        }
        public byte[] BitArrayToByteArray(BitArray bits)
        {
            int numBytes = (bits.Length + 7) / 8;
            byte[] bytes = new byte[numBytes];
            bits.CopyTo(bytes, 0);
            return bytes;
        }
        public string GetEncodedTree()
        {
            StringBuilder sb = new StringBuilder();
            SaveTree(root, sb);
            return sb.ToString();
        }

        private void SaveTree(Node node, StringBuilder sb)
        {
            if (node == null) return;
            if (node.isLeaf)
            {
                sb.Append("1");
                sb.Append((char)node.value);
            }
            else
            {
                sb.Append("0");
                SaveTree(node.leftNode, sb);
                SaveTree(node.rightNode, sb);
            }
        }


        public void LoadEncodedTree(string data)
        {
            int index = 0;
            root = LoadTree(data, ref index);
        }

        private Node LoadTree(string data, ref int index)
        {
            if (data[index++] == '1')
            {
                return new Node((byte)data[index++], 0);
            }
            else
            {
                Node left = LoadTree(data, ref index);
                Node right = LoadTree(data, ref index);
                return new Node(0, 0, left, right);
            }
        }
        public byte[] Decode(BitArray bits)
        {
            List<byte> result = new List<byte>();
            Node current = root;
            foreach (bool bit in bits)
            {
                current = bit ? current.rightNode : current.leftNode;
                if (current.isLeaf)
                {
                    result.Add(current.value);
                    current = root;
                }
            }
            return result.ToArray();
        }



    }
}