# Broadway Boogie Weggie

School assignment for **Advanced Algorithms** – a Windows desktop application built with **C#**, **.NET**, and **WPF** to visualize and compare different algorithms.

> Original project name: `broadway-boogie-beggie`  
> Author: **Ferran Hendriks (2130858)**

---

## 📚 Overview

**Broadway Boogie Weggie** is a WPF desktop application created as part of an Advanced Algorithms course.  
Its purpose is to:

- Implement multiple classic algorithms.
- Provide interactive controls to test and compare them.
- Show visual or textual insights into how each algorithm behaves step-by-step.
- Demonstrate differences in speed, complexity, and logic.

---

## ✨ Features

*(Features extracted generically based on project structure — adjust if needed.)*

- Multiple algorithm implementations.
- Visual or textual output for each algorithm.
- Interactive parameter inputs.
- Clean WPF UI structure.
- Educational focus on understanding algorithm flow, not just the final result.

---

## 📁 Project Structure

Broadway Boogie Weggie/ ├─ Broadway Boogie Weggie/           # Main WPF project ├─ Broadway Boogie Weggie Ferran Hendriks 2130858.zip ├─ .gitignore └─ README.md

The main code lives inside the `Broadway Boogie Weggie` folder, containing:

- `.sln` – Visual Studio solution file  
- `/Views` – XAML UI screens  
- `/Algorithms` – Algorithm classes (depending on assignment)  
- `/Models` – Any data structures used  
- `/Resources` – Images, icons, etc.  

---

## 🚀 Getting Started

### **Requirements**
- Windows 10/11  
- **Visual Studio 2019 or later**
  - Workload: **.NET desktop development**

### **Clone the Repository**
```bash
git clone https://github.com/ferrannl/Broadway-Boogie-Weggie.git
cd Broadway-Boogie-Weggie
```

Open & Run

1. Open Visual Studio


2. File → Open → Project/Solution…


3. Select the .sln file in the main folder


4. Build the project (Ctrl+Shift+B)


5. Run (F5)




---

🧪 Usage

When the application launches, you can:

Select an algorithm from the UI

Provide inputs or configure parameters

Run or step-through the algorithm

Watch a visualization or read output logs


This allows comparing how different algorithms behave under different inputs.


---

🧩 Extending the Project

To add new algorithms or views:

1. Add a new class inside the appropriate folder (e.g., Algorithms/).


2. Implement your logic following the style of existing algorithms.


3. Add UI elements in XAML (e.g., dropdown entries or buttons).


4. Bind algorithm execution results to the UI.



WPF enables MVVM-style structuring, though this project may mix approaches depending on the assignment.


---

📦 Releases

If available, compiled versions appear under:
GitHub → Releases (right side of the repo page)

You can download and run the .exe directly on Windows.


---

👤 Author

Ferran Hendriks
Student ID: 2130858

Originally created for the Advanced Algorithms course.


---

📜 License

This project does not include an explicit license.
Assume personal / educational use unless permission is granted by the author.
