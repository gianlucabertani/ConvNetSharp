﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConvNetSharp.Tests
{
    [TestClass]
    public class VolumeTests
    {
        [TestMethod]
        public void FlipTest()
        {
            var volume = new Volume(3, 3, 2, 0.0);
            volume.Set(1, 1, 0, 1.0);
            volume.Set(1, 1, 1, 2.0);

            volume.Set(0, 0, 0, 10.0);
            volume.Set(0, 0, 1, 15.0);

            volume.Set(0, 2, 0, 20.0);
            volume.Set(0, 2, 1, 25.0);

            volume.Set(2, 0, 0, 30.0);
            volume.Set(2, 0, 1, 35.0);

            volume.Set(2, 2, 0, 40.0);
            volume.Set(2, 2, 1, 45.0);

            var flipped = VolumeUtilities.Flip(volume, VolumeUtilities.FlipMode.LeftRight);
            Assert.AreEqual(1.0, flipped.Get(1, 1, 0));
            Assert.AreEqual(2.0, flipped.Get(1, 1, 1));

            Assert.AreEqual(10.0, flipped.Get(2, 0, 0));
            Assert.AreEqual(15.0, flipped.Get(2, 0, 1));

            Assert.AreEqual(20.0, flipped.Get(2, 2, 0));
            Assert.AreEqual(25.0, flipped.Get(2, 2, 1));

            Assert.AreEqual(30.0, flipped.Get(0, 0, 0));
            Assert.AreEqual(35.0, flipped.Get(0, 0, 1));

            Assert.AreEqual(40.0, flipped.Get(0, 2, 0));
            Assert.AreEqual(45.0, flipped.Get(0, 2, 1));

            flipped = VolumeUtilities.Flip(volume, VolumeUtilities.FlipMode.UpDown);
            Assert.AreEqual(1.0, flipped.Get(1, 1, 0));
            Assert.AreEqual(2.0, flipped.Get(1, 1, 1));

            Assert.AreEqual(10.0, flipped.Get(0, 2, 0));
            Assert.AreEqual(15.0, flipped.Get(0, 2, 1));

            Assert.AreEqual(20.0, flipped.Get(0, 0, 0));
            Assert.AreEqual(25.0, flipped.Get(0, 0, 1));

            Assert.AreEqual(30.0, flipped.Get(2, 2, 0));
            Assert.AreEqual(35.0, flipped.Get(2, 2, 1));

            Assert.AreEqual(40.0, flipped.Get(2, 0, 0));
            Assert.AreEqual(45.0, flipped.Get(2, 0, 1));

            flipped = VolumeUtilities.Flip(volume, VolumeUtilities.FlipMode.Both);
            Assert.AreEqual(1.0, flipped.Get(1, 1, 0));
            Assert.AreEqual(2.0, flipped.Get(1, 1, 1));

            Assert.AreEqual(10.0, flipped.Get(2, 2, 0));
            Assert.AreEqual(15.0, flipped.Get(2, 2, 1));

            Assert.AreEqual(20.0, flipped.Get(2, 0, 0));
            Assert.AreEqual(25.0, flipped.Get(2, 0, 1));

            Assert.AreEqual(30.0, flipped.Get(0, 2, 0));
            Assert.AreEqual(35.0, flipped.Get(0, 2, 1));

            Assert.AreEqual(40.0, flipped.Get(0, 0, 0));
            Assert.AreEqual(45.0, flipped.Get(0, 0, 1));
        }

        [TestMethod]
        public void SerializationTest()
        {
            var volume = new Volume(30, 30, 30); // filled with random values
            for (int i = 0; i < volume.Length; i++)
            {
                volume.SetGradient(i, i);
            }

            Volume deserialized;
            using (var ms = new MemoryStream())
            {
                // Serialize
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, volume);

                // Deserialize
                ms.Position = 0;
                deserialized = formatter.Deserialize(ms) as Volume;
            }

            Assert.AreEqual(volume.Length, deserialized.Length);

            for (int i = 0; i < volume.Length; i++)
            {
                Assert.AreEqual(volume.Get(i), deserialized.Get(i));
                Assert.AreEqual(volume.GetGradient(i), deserialized.GetGradient(i));
            }
        }

        [TestMethod]
        public void CloneTest()
        {
            var vol = new Volume(10, 10, 10);
            var clone = vol.Clone();

            for (int i = 0; i < vol.Length; i++)
            {
                Assert.AreEqual(vol.Get(i), clone.Get(i));
            }
        }
    }
}
