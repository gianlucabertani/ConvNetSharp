﻿using ConvNetSharp.Layers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConvNetSharp.Tests
{
    [TestClass]
    public class MaxoutLayerTests
    {
        [TestMethod]
        public void GradientWrtInputCheck()
        {
            const int inputWidth = 20;
            const int inputHeight = 20;
            const int inputDepth = 2;

            // Create layer
            var layer = new MaxoutLayer(4);

            GradientCheckTools.GradientCheck(layer, inputWidth, inputHeight, inputDepth);
        }

        [TestMethod]
        public void SerializationTest()
        {
            // Create a MaxoutLayer
            var layer = new MaxoutLayer(4);
            layer.Init(10, 10, 3);

            MaxoutLayer deserialized;
            using (var ms = new MemoryStream())
            {
                // Serialize
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, layer);

                // Deserialize
                ms.Position = 0;
                deserialized = formatter.Deserialize(ms) as MaxoutLayer;
            }

            Assert.AreEqual(layer.InputDepth, deserialized.InputDepth);
            Assert.AreEqual(layer.InputHeight, deserialized.InputHeight);
            Assert.AreEqual(layer.InputWidth, deserialized.InputWidth);
            Assert.AreEqual(layer.OutputDepth, deserialized.OutputDepth);
            Assert.AreEqual(layer.OutputHeight, deserialized.OutputHeight);
            Assert.AreEqual(layer.OutputWidth, deserialized.OutputWidth);
            Assert.AreEqual(layer.GroupSize, deserialized.GroupSize);
        }
    }
}