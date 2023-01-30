import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  public loginForm!: FormGroup;
  constructor(private auth: AuthService, private fb: FormBuilder) { 
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(18)]]
    });
  }

  onlogin(){
    if (!this.loginForm.valid) {
      alert("Login form is invalid");
      return;
    }
    this.auth.login(this.loginForm.value)
    .subscribe({
      next:(res)=> {
        this.loginForm.reset();
      }
    });
  }
}

