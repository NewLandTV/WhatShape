import torch
import torch.nn as nn
import torch.optim as optim
import torchvision
import torchvision.transforms as transforms
from Model import CNN

batchSize = 4
transform = transforms.Compose([
    transforms.ToTensor()
])

# Load train dataset
trainSet = torchvision.datasets.ImageFolder(
    root = "./Dataset/Train/",
    transform = transform
)
trainLoader = torch.utils.data.DataLoader(
    trainSet,
    batch_size = batchSize,
    shuffle = True,
    num_workers = 0
)

# Model training
epochs = 20
learningRate = 1e-4
model = CNN()
criterion = nn.CrossEntropyLoss()
optimizer = optim.Adam(model.parameters(), lr = learningRate)

model.train()

for epoch in range(epochs):
    for images, labels in trainLoader:
        optimizer.zero_grad()

        outputs = model(images)
        loss = criterion(outputs, labels)

        loss.backward()
        optimizer.step()

    print(f"[Epoch: {epoch + 1:5d}/{epochs}] Loss: {loss.item()}")

torch.save(model.state_dict(), "Model.pth")

print("Finished Training!")
