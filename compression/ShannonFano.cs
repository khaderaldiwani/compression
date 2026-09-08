
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace compression
{
    public class ShannonFano
    {
        class SFNode
        {
            public byte? Symbol;
            public int Frequency;
            public string Code;
 }

        private Dictionary<byte, string> symbolCodes = new Dictionary<byte, string>();
        private Dictionary<string, byte> reverseCodes = new Dictionary<string, byte>();

        public string Compress(byte[] input)
        {
            var freq = input.GroupBy(b => b)
                            .Select(g => new SFNode { Symbol = g.Key, Frequency = g.Count(), Code = "" })
                            .OrderByDescending(n => n.Frequency)
                            .ToList();

            BuildTree(freq);

            symbolCodes = freq.ToDictionary(n => n.Symbol.Value, n => n.Code);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in input)
            {
                sb.Append(symbolCodes[b]);
            }
            return sb.ToString();
        }

        private void BuildTree(List<SFNode> list)
        {
            if (list.Count <= 1)
                return;

            int total = list.Sum(n => n.Frequency);
            int half = total / 2;

            int index = 0;
            int sum = 0;
            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i].Frequency;
                if (sum >= half)
                {
                    index = i;
                    break;
                }
            }

            for (int i = 0; i <= index; i++)
                list[i].Code += "0";
            for (int i = index + 1; i < list.Count; i++)
                list[i].Code += "1";

            BuildTree(list.GetRange(0, index + 1));
            BuildTree(list.GetRange(index + 1, list.Count - (index + 1)));
        }

        public BitArray Tobitarray(byte[] input)
        {
            List<bool> bits = new List<bool>();
            foreach (byte b in input)
            {
                string code = symbolCodes[b];
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

        public void LoadCodes(Dictionary<byte, string> codes)
        {
            symbolCodes = codes;
            reverseCodes = codes.ToDictionary(kv => kv.Value, kv => kv.Key);
        }

        public byte[] Decode(BitArray bits)
        {
            List<byte> result = new List<byte>();
            StringBuilder sb = new StringBuilder(); 

            foreach (bool bit in bits)
            {
                sb.Append(bit ? "1" : "0");
                if (reverseCodes.ContainsKey(sb.ToString()))
                {
                    result.Add(reverseCodes[sb.ToString()]);
                    sb.Clear();
                }
            }

            return result.ToArray();
        }
        public string GetEncodedTree()
        {
            return string.Join("|", symbolCodes.Select(kv => $"{kv.Key}:{kv.Value}"));
        }
        public void LoadEncodedTree(string data)
        {
            var dict = new Dictionary<byte, string>();
            var pairs = data.Split('|');
            foreach (var pair in pairs)
            {
                var parts = pair.Split(':');
                if (parts.Length == 2)
                {
                    byte key = byte.Parse(parts[0]);
                    string value = parts[1];
                    dict[key] = value;
                }
            }
            LoadCodes(dict);
        }


    }
}

