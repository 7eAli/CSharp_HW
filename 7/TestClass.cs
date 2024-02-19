﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7
{
    public class TestClass
    {
        public int I { get; set; }
        public string? S { get; set; }
        public decimal D { get; set; }
        public char[]? C { get; set; }

        public TestClass()
        { }
        private TestClass(int i)
        {
            this.I = i;
        }
        public TestClass(int i, string s, decimal d, char[] c) : this(i)
        {
            this.S = s;
            this.D = d;
            this.C = c;
        }

        public override string ToString()
        {
            return $"I={I}, S={S}, D={D}, C={C[0]} {C[1]} {C[2]}";
        }
    }
}
