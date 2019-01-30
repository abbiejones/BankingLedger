# BankingLedger

This app simulates a banking ledger. Through both web and console applications, the following actions are supported:

- Create a new account
- Login
- Record a deposit
- Record a withdrawal
- Check balance
- See transaction history
- Logout

**Angular 6** front end (only in web app) / **.Net Core** backend / **Sqlite** database

## Installation

```
git clone git@github.com:abbiejones/BankingLedger.git
```

## Running Console App
```
cd BankingLedger/backend/
```
Open `Program.cs` and confirm that `app` is set to `appType.CONSOLE`
Return to your terminal and execute:

```
dotnet run
```
## Running Web App

```
cd BankingLedger/backend/
```
Open `Program.cs` and confirm that `app` is set to `appType.WEB`
Return to your terminal and execute:

```
dotnet run
```

In a new terminal:
```
cd BankingLedger/frontend/
ng serve
```

Navigate to `https://localhost:4200` in your browser.
