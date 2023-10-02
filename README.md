# Distributed Job Queue

A distributed job queue system implemented in C# that uses consistent hashing for load balancing across worker nodes. This system allows you to enqueue and dequeue jobs on worker nodes and handles job failures and retries.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Introduction

This repository contains C# code for a distributed job queue system designed to distribute and manage jobs across multiple worker nodes. It uses consistent hashing to ensure that jobs are evenly distributed across nodes, and it provides mechanisms for handling job failures and retries.

## Features

- **Consistent Hashing:** Load balancing of jobs across worker nodes is achieved using a consistent hashing algorithm.
- **Dynamic Node Management:** Worker nodes can be added and removed from the system dynamically, and jobs are redistributed accordingly.
- **Job Retries:** Failed jobs can be retried up to a specified maximum number of times (configurable).
- **Simple and Advanced Implementations:** Choose between a simple implementation without job retries (`SimpleDistributedJobQueue`) and an advanced implementation with job retry capabilities (`AdvancedDistributedJobQueue`).

## Getting Started

To get started with this distributed job queue system, follow these steps:

1. Clone the repository to your local machine:

   ```bash
   git clone https://github.com/Ugwum/DistributedJobQueue.git
2. Open the solution in your preferred C# development environment (e.g., Visual Studio, Visual Studio Code).

3. Build and run the project to start experimenting with the distributed job queue system.

## Usage
The usage of this distributed job queue system is described in detail in the code and comments within the classes. Here's a basic overview:

1. Create an instance of the desired job queue implementation (SimpleDistributedJobQueue or AdvancedDistributedJobQueue).

2. Add worker nodes to the job queue.

3. Enqueue jobs, which will be distributed to worker nodes using consistent hashing.

4. Dequeue jobs from specific worker nodes.

5. Handle job failures and retries if using the advanced implementation (AdvancedDistributedJobQueue).

For specific code examples and usage details, refer to the code comments and documentation within the repository.

## Contributing
I welcome contributions to improve this distributed job queue system. To contribute:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Make your changes and commit them with descriptive messages.
4. Push your changes to your forked repository.
5. Create a pull request to merge your changes into the main repository.
6. Please follow the code of conduct and contribution guidelines when contributing.

## License
This distributed job queue system is open-source and available under the MIT License. Feel free to use and modify it as needed for your projects. 

