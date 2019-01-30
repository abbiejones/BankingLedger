import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

//login menu
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
  userId: number;
  error: boolean;

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
    this.error = false;
    this.authService.login(this.userName, this.password).subscribe(() => {
      //this.setMessage();
      if (this.authService.isLoggedIn) {
        // Get the redirect URL from our auth service
        // If no redirect has been set, use the default
        this.userId = this.authService.userId;
        let redirect = this.authService.redirectUrl ? this.authService.redirectUrl : '/usermenu';


        // Redirect the user
        this.router.navigate([redirect]);
        this.authService.getId(this.userId);
      } else {
        this.error = true;
        this.userName = "";
        this.password = "";
      }
    });
  }

}
