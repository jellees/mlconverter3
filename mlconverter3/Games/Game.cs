using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mlconverter3.Games
{
    abstract class Game
    {
        /// <summary>
        /// Identifying string
        /// </summary>
        public const string Identifier = "";

        /// <summary>
        /// The games' good name
        /// </summary>
        public abstract string GoodName { get; }

        /// <summary>
        /// Total count of sequences in the game
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// Creates a new game object
        /// </summary>
        /// <param name="binaryReader"></param>
        public Game(BinaryReader binaryReader) { }

        /// <summary>
        /// Get a sequence
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public abstract Sequences.Sequence GetSequence(int index);

        /// <summary>
        /// Get an array of pointers
        /// </summary>
        /// <returns></returns>
        public abstract int[] GetPointers();
    }
}
