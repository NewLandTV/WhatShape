using System;
using System.Linq;
using Unity.Barracuda;
using UnityEngine;

public class WhatShape : MonoBehaviour
{
    public NNModel model;
    private Model runtimeModel;
    public Texture2D inputTexture;

    private IWorker engine;

    [Serializable]
    public struct Prediction
    {
        public int predictedValue;
        public float[] predicted;

        public void SetPrediction(Tensor tensor)
        {
            predicted = tensor.AsFloats();
            predictedValue = Array.IndexOf(predicted, predicted.Max());

            string[] classes = { "Circle", "Square", "Triangle" };

#if UNITY_EDITOR
            Debug.Log($"Predicted : {classes[predictedValue]}");
#endif
        }
    }

    public Prediction prediction = new Prediction();

    private void Awake()
    {
        runtimeModel = ModelLoader.Load(model);
        engine = WorkerFactory.CreateWorker(runtimeModel, WorkerFactory.Device.GPU);
    }

    private void OnDestroy()
    {
        engine?.Dispose();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Predict();
        }
    }

    public void Predict()
    {
        int channelCount = 3;

        Tensor inputTensor = new Tensor(inputTexture, channelCount);
        Tensor outputTensor = engine.Execute(inputTensor).PeekOutput();

        prediction.SetPrediction(outputTensor);

        inputTensor.Dispose();
    }
}
