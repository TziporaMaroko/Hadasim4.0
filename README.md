# Hadasim4.0
# Overview
This project is a web application developed to manage member data within a Health Maintenance Organization (HMO), with a particular emphasis on tracking COVID-19 cases and vaccination status. It provides a comprehensive dashboard for monitoring COVID-19 related statistics, including the number of infected members, vaccination rates, and trends over time.

# Screenshots
<img src="https://github.com/TziporaMaroko/Hadasim4.0/assets/116155777/ddc329a2-47c5-4ce9-b4cb-16c21e461fff" alt="צילום מסך 2024-03-28 004939" width="500">
<img src="https://github.com/TziporaMaroko/Hadasim4.0/assets/116155777/61a51cd3-f055-4839-992f-2d776c8e941c" alt="צילום מסך 2024-03-28 010311" width="500">
<img src="https://github.com/TziporaMaroko/Hadasim4.0/assets/116155777/981e4cc1-1951-4397-9aeb-bac502c81733" alt="צילום מסך 2024-03-28 005013" width="500">
<img src="https://github.com/TziporaMaroko/Hadasim4.0/assets/116155777/288acd7e-2845-474a-ac61-8a25a9628214" alt="צילום מסך 2024-03-28 005036" width="500">
<img src="https://github.com/TziporaMaroko/Hadasim4.0/assets/116155777/12e88f02-b93c-43ab-ad74-92dff461b181" alt="צילום מסך 2024-03-28 005936" width="500">
<img src="https://github.com/TziporaMaroko/Hadasim4.0/assets/116155777/f051e0e9-dfe4-43be-8cc7-73b750174d49" alt="צילום מסך 2024-03-28 005151" width="500">
<img src="https://github.com/TziporaMaroko/Hadasim4.0/assets/116155777/ff55d66e-6c51-4739-ba33-eae9f854c124" alt="צילום מסך 2024-03-28 005210" width="500">

# Usage
To utilize this application effectively, follow these steps:

1. **Installation**:
   - Clone the repository to your local machine.
   - Ensure you have the necessary dependencies installed. This may include .NET Core SDK, Entity Framework Core, and any required packages specified in the project's `csproj` file.

2. **Configuration**:
   - Update the connection string in the `appsettings.json` file to point to your database if necessary.
   - Make sure the database is created and migrated using Entity Framework Core. You can do this by running `dotnet ef database update` in the terminal.

3. **Running the Application**:
   - Build the project using `dotnet build`.
   - Run the application with `dotnet run`.
   - Access the application through a web browser at the specified URL (usually `https://localhost:5001`).


## External Dependencies
This project relies on several external libraries and services:

- **Moment.js**: A JavaScript library for parsing, validating, manipulating, and formatting dates.
- **Flatpickr**: A lightweight and powerful datetime picker library.
- **jQuery**: A fast, small, and feature-rich JavaScript library.
- **Morris.js**: A library for drawing visually appealing charts based on Raphael.js.

Ensure these dependencies are included in your project either through CDN links or installed via package managers like npm or yarn.
