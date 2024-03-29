import onnx
import torch
import torch.onnx
from Model import CNN

batchSize = 4

# Load model
model = CNN()

model.load_state_dict(torch.load("Model.pth"))
model.eval()

# Export onnx
x = torch.randn(batchSize, 3, 28, 28, requires_grad = True)
outputs = model(x)
_, predicted = torch.max(outputs.data, 1)
path = "Model.onnx"

torch.onnx.export(
    model,
    x,
    path,
    export_params = True,
    opset_version = 13,
    do_constant_folding = True,
    input_names = ["input"],
    output_names = ["output"],
    dynamic_axes = {
        "input": {
            0: "batch_size"
        },
        "output": {
            0: "batch_size"
        }
    }
)

onnx.save(onnx.shape_inference.infer_shapes(onnx.load(path)), path)
