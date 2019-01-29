import { Component, OnInit } from '@angular/core';
import { RouterModule, Routes, RouterStateSnapshot } from '@angular/router';
import { AuthService }            from '../auth/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private authService: AuthService) {}

  ngOnInit() {
  }

  checkLogin(): boolean {
    if (this.authService.isLoggedIn) { return true; }
    return false;
  }

  logout(){
    this.authService.logout();
  }

}