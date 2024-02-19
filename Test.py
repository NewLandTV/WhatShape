import torch
import torchvision
import torchvision.transforms as transforms
from Model import CNN

batchSize = 4
transform = transforms.Compose([
    transforms.ToTensor()
])

# Load test dataset
testSet = torchvision.datasets.ImageFolder(
    root = "./Dataset/Test/",
    transform = transform
)
testLoader = torch.utils.data.DataLoader(
    testSet,
    batch_size = batchSize,
    shuffle = True,
    num_workers = 0
)

# Load model
model = CNN()

model.load_state_dict(torch.load("Model.pth"))
model.eval()

correct = 0
total = 0

with torch.no_grad():
    for inputs, labels in testLoader:
        outputs = model(inputs)
        _, predicted = torch.max(outputs.data, 1)
        total += labels.size(0)
        correct += (predicted == labels).sum().item()

accuracy = correct / total * 100

print(f"Accuracy: {accuracy:.2f} %")
