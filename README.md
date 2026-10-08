# Optimal Food Combination Solver

This repository contains algorithmic solutions in **C#** and **PHP** to solve the **Optimal Food Combination Problem**. Given a fixed stock of ingredients and a set of recipes with varying yields, the goal is to determine the combination of foods that feeds the maximum number of people.

---

## Task Overview & Problem Statement

### Available Inventory
* **Cucumber:** 2
* **Olives:** 2
* **Lettuce:** 3
* **Meat:** 6
* **Tomato:** 6
* **Cheese:** 8
* **Dough:** 10

### Recipe Specs & People Fed
| Recipe | Feeds | Required Ingredients |
| :--- | :---: | :--- |
| **Sandwich** | 1 | 1 Dough, 1 Lettuce, 1 Cucumber, 1 Cheese |
| **Burger** | 1 | 1 Meat, 2 Dough, 1 Tomato, 1 Cheese |
| **Pie** | 1 | 2 Meat, 1 Dough |
| **Pasta** | 2 | 2 Dough, 1 Meat, 2 Cheese |
| **Salad** | 3 | 2 Lettuce, 1 Tomato, 1 Olives |
| **Pizza** | 4 | 3 Dough, 2 Tomato, 3 Cheese, 1 Olives |

---

## Solution Summary & Findings

* **Maximum People Fed:** **10 people**
* **Optimal Food Combination:**
  * **1 × Pizza** (Feeds 4)
  * **1 × Salad** (Feeds 3)
  * **1 × Pasta** (Feeds 2)
  * **1 × Sandwich** (Feeds 1)

### Remaining Inventory
* **Unused:** 4 Dough, 5 Meat, 2 Tomato, 2 Cheese, 1 Cucumber, 0 Olives, 0 Lettuce.

---

## Code Pattern & Algorithmic Approach

Both implementations utilize a **Recursive Backtracking with Depth-First Search (DFS)** design pattern:

1. **State Space Exploration:** The algorithm iterates through each recipe and recursively explores making $0, 1, 2, \dots, N$ instances of that recipe as long as sufficient ingredients remain in stock.
2. **Backtracking:** When a specific search branch is fully evaluated (or ingredients run out), the algorithm restores the inventory state (**backtracks**) and explores alternative recipe branches.
3. **Optimization Tracking:** A global variable tracks the maximum number of people fed across all valid paths, storing the optimal item combination whenever a new high score is discovered.

---

## Project Structure

```text
.
├── README.md
├── csharp/
│   ├── Program.cs
│   └── CSharp.csproj
└── PHP/
    └── index.php
```
---

## How to Setup and Run

### Option 1: Running the C# Solution

#### Prerequisites
* [.NET SDK 6.0+](https://dotnet.microsoft.com/download) installed on your system.

#### Running via Terminal
1. Navigate to the C# folder:
   ```bash
   cd csharp

2. Build and run the project:
  ```bash
  dotnet run
  ```

### Option 2: Running the PHP Solution

#### Prerequisites
* [PHP 8.0+](https://www.php.net/downloads.php) installed locally and accessible via your Command Line Interface (CLI).

#### Running via Terminal
1. Navigate to the PHP folder:
   ```bash
   cd php

2. Build and run the project:
  ```bash
  php index.php