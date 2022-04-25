import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../service/auth/auth.service';
import { TokenStorageService } from '../service/auth/token-storage.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  rCpf: string = '';
  rEmail: string = '';
  rPassword: string = '';
  email: string = '';
  password: string = '';  
  isSuccessful = false;
  isSignUpFailed = false;
  errorMessage = '';

  isLoggedIn = false;
  isLoginFailed = false;
  roles: string[] = [];

  constructor(private _authService: AuthService, private tokenStorage: TokenStorageService, private router: Router) { 
  }

  ngOnInit(): void {    
    if (this.tokenStorage.getToken()) {
      this.isLoggedIn = true;
      this.router.navigate(['home'])
    }    
  }

  register(): void {      
      this._authService.create(this.rCpf, this.rEmail, this.rPassword).subscribe(
        () => {
          this.isSuccessful = true;
          this.isSignUpFailed = false;
          window.alert('Usuário cadastrado com sucesso');
          this.reloadPage();
        },
        err => {
          window.alert(err.error);
          this.errorMessage = err.error;
          this.isSignUpFailed = true;
        }
      );
  }

  login(): void {    
    this._authService.login(this.email, this.password).subscribe(
      data => {
        this.tokenStorage.saveToken(data.token);
        this.tokenStorage.saveUser(data.user);
        this.isLoginFailed = false;
        this.isLoggedIn = true;
        this.reloadPage();
      },
      err => {        
        this.errorMessage = err.error.message;
        this.isLoginFailed = true;
      }
    );
  }

  reloadPage(): void {
    window.location.reload();
  }
}



