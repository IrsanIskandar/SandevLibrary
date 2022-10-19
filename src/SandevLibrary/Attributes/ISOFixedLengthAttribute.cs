using System;

namespace SandevLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class ISOFixedLengthAttribute : Attribute
    {
        public int LengthIso { get; set; }

        public ISOPosition Position { get; set; }

        public char CharaterString { get; set; }

        public string ResultIsoString { get; private set; }

        public ISOFixedLengthAttribute(int lengthIso, ISOPosition position, char charaterString)
        {
            this.LengthIso = lengthIso;
            this.Position = position;
            this.CharaterString = charaterString;
        }
    }

    public enum ISOPosition
    {
        Left = 2,
        Right = 3,
    }
}
