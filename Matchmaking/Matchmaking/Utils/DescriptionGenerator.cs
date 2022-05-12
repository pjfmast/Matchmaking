﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matchmaking.Utils
{
    public class DescriptionGenerator
    {
        private string[] _adjectives = { "nice", "horrible", "great", "terribly old", "brand new" };
        private string[] _other = { "picture of grandpa", "car", "photo of a forest", "duck" };
        private static Random random = new Random();

        public string Generate()
        {
            var a = _adjectives[random.Next(_adjectives.Count())];
            var b = _other[random.Next(_other.Count())];
            return $"A {a} {b}";
        }
    }

}
