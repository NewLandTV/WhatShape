from PIL import Image
import torch
import torchvision.transforms as transforms
from Model import CNN

transform = transforms.Compose([
    transforms.ToTensor()
])

classes = ("Circle", "Square", "Triangle")

# Load image
image = Image.open("./Input.jpeg")
image = transform(image)

# Load model
model = CNN()

model.load_state_dict(torch.load("Model.pth"))
model.eval()

with torch.no_grad():
    outputs = model(image)
    _, predicted = torch.max(outputs.data, 1)

print(classes[predicted[0]])
