import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { tap, delay } from 'rxjs/operators';
import { HttpHeaders } from '@angular/common/http';
import { stringify } from '@angular/core/src/render3/util';
import { map, catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class BankingService {

  httpOptions: any;

  constructor(private http: HttpClient) {
    this.httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'my-auth-token'
      })
    };
   }

  deposit(accountNumber: number, amount: number): Observable<any> {
    var accountInfo = {accountNumber: accountNumber, amount: amount};
    return this.http.post("http://localhost:5000/api/bankaccount/deposit/", accountInfo, this.httpOptions);
  }

  withdraw(accountNumber: number, amount: number): Observable<any> {
    var accountInfo = {accountNumber: accountNumber, amount: amount};
    return this.http.post("http://localhost:5000/api/bankaccount/withdraw/", accountInfo, this.httpOptions);
  }

   checkBalance(): Observable<any> {
    return this.http.get("http://localhost:5000/api/bankaccount/balance/", this.httpOptions);
  }

  transactionHistory(): Observable<any> {
    return this.http.get("http://localhost:5000/api/bankaccount/transaction/", this.httpOptions);
  }




}
