import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  message: string;
  userName: string;
  password: string;
  submitted: boolean;

  ngOnInit() {
  }

  constructor(public authService: AuthService, public router: Router) {
    this.setMessage();
  }
 
  setMessage() {
    this.message = 'Logged ' + (this.authService.isLoggedIn ? 'in' : 'out');
  }
 
  onSubmit(){
    this.submitted = true;
    this.login();
  }

  login() {
    this.message = 'Trying to log in ...';
 
    this.authService.login(this.userName, this.password).subscribe(() => {
      //this.setMessage();
      if (this.authService.isLoggedIn) {
        // Get the redirect URL from our auth service
        // If no redirect has been set, use the default
        let redirect = this.authService.redirectUrl ? this.authService.redirectUrl : '/usermenu';
        // Redirect the user
        this.router.navigate([redirect]);
      } else {
        //either password is incorrect or user does not exist
      }
    });
  }

}
