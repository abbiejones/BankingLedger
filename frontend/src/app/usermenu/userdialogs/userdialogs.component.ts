import { Component, Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { BankAccount } from '../usermenu.component'
import { Transactions } from '../usermenu.component'

//all dialog components (balance, withdrawal, deposit, transaction history)
@Component({
  selector: 'balance-dialog',
  templateUrl: 'balance-dialog.html',
})
export class BalanceDialog {

  constructor(
    public dialogRef: MatDialogRef<BalanceDialog>,
    @Inject(MAT_DIALOG_DATA) public data: BankAccount) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

}

@Component({
  selector: 'deposit-dialog',
  templateUrl: 'deposit-dialog.html',
})
export class DepositDialog {

  constructor(
    public dialogRef: MatDialogRef<DepositDialog>,
    @Inject(MAT_DIALOG_DATA) public data: BankAccount) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

}

@Component({
  selector: 'withdraw-dialog',
  templateUrl: 'withdraw-dialog.html',
})
export class WithdrawDialog {

  constructor(
    public dialogRef: MatDialogRef<WithdrawDialog>,
    @Inject(MAT_DIALOG_DATA) public data: BankAccount) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

}

@Component({
  selector: 'transaction-dialog',
  templateUrl: 'transaction-dialog.html',
})
export class TransactionDialog {

  constructor(
    public dialogRef: MatDialogRef<TransactionDialog>,
    @Inject(MAT_DIALOG_DATA) public data: string) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

}