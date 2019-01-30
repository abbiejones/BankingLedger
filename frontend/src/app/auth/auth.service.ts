import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { tap, delay } from 'rxjs/operators';
import { HttpHeaders } from '@angular/common/http';
import { stringify } from '@angular/core/src/render3/util';
import { map, catchError } from 'rxjs/operators';
import { Subject }    from 'rxjs';

//authorization service
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  httpOptions: any;
  getId$: Observable<number>;
  private getIdSubject = new Subject<number>();

  constructor(private http: HttpClient) {
    this.httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'my-auth-token'
      })
    };
   }

  isLoggedIn = false;
  userId : number;
  // store the URL so we can redirect after logging in
  redirectUrl: string;

  login(userName: string, password: string): Observable<any> {
    var user = {firstName: "", lastName: "", userName: userName, password: password};
    return this.http.post("http://localhost:5000/api/user/login/", user, this.httpOptions)
      .pipe(
        map((res) => this.checkUser(res))
      )
  }

  getId(data){
    this.getIdSubject.next(data);
  }

  checkUser(response: any){
     this.userId = +response;
      if (this.userId >= 1){
        this.isLoggedIn = true;
      }
  }

  register(firstName:string, lastName:string, userName: string, password: string): Observable<any> {
    var user = {firstName: firstName, lastName: lastName, userName: userName, password: password};
    return this.http.post("http://localhost:5000/api/user/register/", user, this.httpOptions)
      .pipe(
        map((res) => this.checkUser(res))
      )
  }

  logout(): void {
    this.isLoggedIn = false;
  }
}
