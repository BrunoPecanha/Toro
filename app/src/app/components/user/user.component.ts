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
  data: any;
  
  

  constructor(private dialog: DialogService, private _authService: AuthService, private tokenStorage: TokenStorageService, private router: Router) { 
  }

  ngOnInit(): void {    
    if (this.tokenStorage.getToken()) {
      this.isLoggedIn = true;
      this.router.navigate(['home'])
    }    
  }

  async registerAsync() {  
      try {    
        const result = await this._authService.create(this.rCpf, this.rEmail, this.rPassword);
        this.isSuccessful = true;
        this.isSignUpFailed = false;
        this.dialog.showAlert('Sucesso', 'Usuário criado com sucesso !'); 
        this.dialog.reloadPage();
      } catch (error: any) {        
        this.dialog.showErr('Atenção', error.error);   
        this.isSignUpFailed = true;   
      }
}


 async loginAsync() {    
  try {    
        const result = await this._authService.login(this.email, this.password);
        this.tokenStorage.saveToken(result.message);
        this.tokenStorage.saveUser(result.data);
    
        this.isLoginFailed = false;
        this.isLoggedIn = true;
        this.dialog.reloadPage();

      } catch (error: any) {        
        this.dialog.showErr('Atenção', error.error);      
      }
  }  
}



