﻿using ConvNetSharp.Layers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConvNetSharp.Tests
{
    [TestClass]
    public class SVMLayerTests
    {
        [TestMethod]
        public void SerializationTest()
        {
            // Create a SvmLayer
            var layer = new SvmLayer(7);
            layer.Init(10, 10, 3);

            SvmLayer deserialized;
            using (var ms = new MemoryStream())
            {
                // Serialize
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, layer);

                // Deserialize
                ms.Position = 0;
                deserialized = formatter.Deserialize(ms) as SvmLayer;
            }

            Assert.AreEqual(layer.InputDepth, deserialized.InputDepth);
            Assert.AreEqual(layer.InputHeight, deserialized.InputHeight);
            Assert.AreEqual(layer.InputWidth, deserialized.InputWidth);
            Assert.AreEqual(layer.OutputDepth, deserialized.OutputDepth);
            Assert.AreEqual(layer.OutputHeight, deserialized.OutputHeight);
            Assert.AreEqual(layer.OutputWidth, deserialized.OutputWidth);
            Assert.AreEqual(layer.ClassCount, deserialized.ClassCount);
        }
    }
}
