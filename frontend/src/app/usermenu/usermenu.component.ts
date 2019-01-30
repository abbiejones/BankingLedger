import { Component, OnInit } from '@angular/core';
import { User }              from '../../assets/User';
import { BankingService }    from '../services/banking.service'
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { BalanceDialog, DepositDialog, WithdrawDialog, TransactionDialog} from './userdialogs/userdialogs.component'
import { AuthService } from '../auth/auth.service';

export interface BankAccount{
  accountNumber: number,
  balance: number,
  amount: number
}

export interface Transactions{
  transactions: string
}

@Component({
  selector: 'app-usermenu',
  templateUrl: './usermenu.component.html',
  styleUrls: ['./usermenu.component.css']
})
export class UserMenuComponent implements OnInit {

  constructor(private bankingService: BankingService, private authService: AuthService, public dialog: MatDialog) {
    this.userId = this.authService.userId;
  }

  accountNumber : number;
  amount: number;
  balance: number;
  transactions: string;
  userId : number;
  error: boolean;

  ngOnInit() {

  }

  checkBalance(){
    this.error = false;
    this.bankingService.checkBalance(true, this.userId).subscribe((res) => {
        this.accountNumber = res.item1;
        this.balance = res.item2;
        this.openBalance();
    });

  }

  checkAccount(){
    this.error = false;
    this.bankingService.checkBalance(false, this.userId).subscribe((res) => {
      this.accountNumber = res.item1;
      this.balance = res.item2;
  });
  }

  openBalance(): void{
    this.error = false;
    this.checkAccount();
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
    this.error = false;
    this.bankingService.checkBalance(false, this.userId).subscribe((res) => {
      this.accountNumber = res.item1;
      this.balance = res.item2;

      this.bankingService.withdraw(this.userId, this.accountNumber, this.amount).subscribe((res) => {
        this.balance = res.item2;
        this.accountNumber = res.item1;
      });
    });

    this.checkAccount();
  }

  openWithdraw(){
    this.error = false;
    this.bankingService.checkBalance(false, this.userId).subscribe((res) => {
      this.accountNumber = res.item1;
      this.balance = res.item2;

      const dialogRef = this.dialog.open(WithdrawDialog, {
        width: '400px',
        height: '200px',
        data: {accountNumber: this.accountNumber, balance: this.balance, amount: this.amount}
      });

      dialogRef.afterClosed().subscribe(result => {
        console.log('The dialog was closed');
        this.amount = result;
        if (this.amount <= this.balance && this.amount > 0){
          this.withdraw();
        } else if (this.amount != 0 && this.amount != null){
          this.error = true;
          this.amount = 0;
        }
      });
    });
  }

  deposit(){    
    this.error = false;
    this.bankingService.checkBalance(false, this.userId).subscribe((res) => {
      this.accountNumber = res.item1;
      this.balance = res.item2;

        this.bankingService.deposit(this.userId, this.accountNumber, this.amount).subscribe((res) => {
          this.balance = res.item2;
          this.accountNumber = res.item1;
        });
    });
    this.checkAccount();
  }

  openDeposit(){
    this.error = false;
    this.bankingService.checkBalance(false, this.userId).subscribe((res) => {
      this.accountNumber = res.item1;
      this.balance = res.item2;

      const dialogRef = this.dialog.open(DepositDialog, {
        width: '400px',
        height: '200px',
        data: {accountNumber: this.accountNumber, balance: this.balance, amount: this.amount}
      });

      dialogRef.afterClosed().subscribe(result => {
        console.log('The dialog was closed');
        this.amount = result;
        if (this.amount <= 10000 && this.amount > 0){
          this.deposit();
        } else if (this.amount != 0 && this.amount != null){
          this.error = true;
          this.amount = 0;
        }
      });
    });
  }

  viewTransactionHistory(){
    this.error = false;
    this.checkAccount();

      this.bankingService.transactionHistory(this.userId).subscribe((res) => {
        this.transactions = res;
        this.openTransaction();
    });

  }

  openTransaction(){
    this.error = false;
    this.checkAccount();
    const dialogRef = this.dialog.open(TransactionDialog, {
      width: '500px',
      height: '700px',
      data: this.transactions
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  exited(){
    this.error = false;
  }
}


