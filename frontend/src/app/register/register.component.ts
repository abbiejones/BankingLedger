import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

//registration menu
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  message: string;
  firstName: string = "";
  lastName: string = "";
  userName: string = "";
  password: string = "";
  error: boolean;

  ngOnInit(){

  }
  submitted: boolean = false;

  onSubmit() { 
    this.submitted = true; 
    this.register();
  }

  constructor(public authService: AuthService, public router: Router) {
    this.setMessage();
  }
 
  setMessage() {
    this.message = 'Logged ' + (this.authService.isLoggedIn ? 'in' : 'out');
  }


  register() {
    this.message = 'Trying to log in ...';
    this.error = false;
    this.authService.register(this.firstName, this.lastName, this.userName, this.password).subscribe(() => {
      //this.setMessage();
      if (this.authService.isLoggedIn) {
        // Get the redirect URL from our auth service
        // If no redirect has been set, use the default
        let redirect = this.authService.redirectUrl ? this.authService.redirectUrl : '/usermenu';
 
        // Redirect the user
        this.router.navigate([redirect]);
      } else {
        this.error = true;
        this.firstName = "";
        this.lastName = "";
        this.userName = "";
        this.password = "";
      }
    });
  }
}
