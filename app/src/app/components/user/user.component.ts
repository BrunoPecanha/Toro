import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'src/app/helpers/dialog-service';
import { AuthService } from 'src/app/service/auth/auth.service';
import { TokenStorageService } from 'src/app/service/auth/token-storage.service';


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
  userObject: any;
  

  constructor(private dialog: DialogService, private _authService: AuthService, private tokenStorage: TokenStorageService, private router: Router) { 
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
          this.dialog.showAlert('Sucesso', 'Usuário criado com sucesso !'); 
          this.reloadPage();
        },
        err => {
          window.alert(err.error);
          this.errorMessage = err.error;
          this.isSignUpFailed = true;
        }
      );
  }

 async loginAsync() {    
  try {    
        await this._authService.login(this.email, this.password).then(data => {
             sessionStorage.setItem('user', JSON.stringify(data));
            this.tokenStorage.saveToken(data.token);
            this.tokenStorage.saveUser(data.user);
        });     
    
        this.isLoginFailed = false;
        this.isLoggedIn = true;
        this.reloadPage();
      } catch (error: any) {
        debugger
        this.dialog.showErr('Atenção', error.error);      
      }
}

  reloadPage(): void {
    window.location.reload();
  }
}



