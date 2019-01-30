import { Component, OnInit } from '@angular/core';
import { User }              from '../../assets/User';
import { BankingService }    from '../services/banking.service'
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { BalanceDialog, DepositDialog, WithdrawDialog, TransactionDialog} from './userdialogs/userdialogs.component'

export interface BankAccount{
  accountNumber: number,
  balance: number,
  amount: number
}

@Component({
  selector: 'app-usermenu',
  templateUrl: './usermenu.component.html',
  styleUrls: ['./usermenu.component.css']
})
export class UserMenuComponent implements OnInit {

  constructor(private bankingService: BankingService, public dialog: MatDialog) {}

  accountNumber : number;
  amount: number;
  balance: number;
  transactions: string[];

  ngOnInit() {
  }

  checkBalance(){
    this.bankingService.checkBalance().subscribe((res) => {
        this.accountNumber = res.accountNumber;
        this.balance = res.balance;
    });
    // this.accountNumber = 123;
    // this.balance = 1.34;
    this.openBalance();
  }

  checkAccount(){
    this.bankingService.checkBalance().subscribe((res) => {
      this.accountNumber = res.accountNumber;
      this.balance = res.balance;
  });
  }

  openBalance(): void{
    const dialogRef = this.dialog.open(BalanceDialog, {
      width: '350px',
      height: '200px',
      data: {accountNumber: this.accountNumber, balance: this.balance, amount: this.amount}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  withdraw(){
    this.checkAccount();

    this.bankingService.withdraw(this.accountNumber, this.amount).subscribe((res) => {
      this.balance = res.balance;
      this.accountNumber = res.accountNumber;
    });
  }

  openWithdraw(){
    const dialogRef = this.dialog.open(WithdrawDialog, {
      width: '350px',
      height: '200px',
      data: {accountNumber: this.accountNumber, balance: this.balance, amount: this.amount}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.amount = result;
      //this.amount = result.amount;
    });
  }

  deposit(){    
    this.checkAccount();

    this.bankingService.deposit(this.accountNumber, this.amount).subscribe((res) => {
      this.balance = res.balance;
      this.accountNumber = res.accountNumber;
    });
  }

  openDeposit(){
    const dialogRef = this.dialog.open(DepositDialog, {
      width: '350px',
      height: '200px',
      data: {accountNumber: this.accountNumber, balance: this.balance, amount: this.amount}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.amount = result;
      //this.amount = result.amount;
    });
  }

  viewTransactionHistory(){
      this.bankingService.checkBalance().subscribe((res) => {
        this.transactions = res;
    });

    this.openTransaction();
  }

  openTransaction(){
    const dialogRef = this.dialog.open(TransactionDialog, {
      width: '350px',
      height: '350px',
      data: {accountNumber: this.accountNumber, balance: this.balance, amount: this.amount}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }
}


