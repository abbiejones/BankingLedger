import { Component, OnInit } from '@angular/core';
import { RouterModule, Routes, RouterStateSnapshot } from '@angular/router';
import { AuthService }            from '../auth/auth.service';
import { User }              from '../../assets/User';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  currentUser: User;

  constructor(private authService: AuthService) {}

  ngOnInit() {
  }

  checkLogin(): boolean {
    if (this.authService.isLoggedIn) { 
      this.currentUser.userId = this.authService.userId;
      this.authService.getId(this.currentUser.userId);
      return true; }
    return false;
  }

  logout(){
    this.authService.logout();
  }

}
