This project uses the .NET Secret Manager to securely store your OpenWeather API key during local development.
 This keeps your credentials safe and ensures they are not committed to source control (e.g., GitHub)

-How to Set the API Key
From the terminal, navigate to the root directory of the OpenWeather project and run the following commands:
   dotnet user-secrets init
   dotnet user-secrets set "OpenWeather:ApiKey" "your_api_key_here"

Replace "your_api_key_here" with your actual OpenWeather API key.


-How to Remove the API Key
To remove the API key or clear all secrets, you can run the following:
   dotnet user-secrets remove "OpenWeather:ApiKey"
   dotnet user-secrets clear

If you want to completely disable Secret Manager for this project, 
delete the <UserSecretsId> tag from the .csproj file in the OpenWeather project:

   <UserSecretsId>your-guid-here</UserSecretsId>

Note: This approach ensures that anyone who clones the repository won’t have access to your key. 
Developers can use their own keys locally without changing shared config files
