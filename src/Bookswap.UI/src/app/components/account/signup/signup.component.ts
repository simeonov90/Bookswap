import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  signupForm!: FormGroup;
  constructor(private auth: AuthService, private fb: FormBuilder) {
    
  }

  ngOnInit(): void {
    this.initSignupForm();
  }

  onSubmit(){
    this.auth.signUp(this.signupForm.value).subscribe({
      next:(res)=> {
        alert(res.message);
        this.signupForm.reset();
      }
    });
  }

  private initSignupForm(): void{
    this.signupForm =  this.fb.group({
      userName: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      age: ['', [Validators.required, Validators.maxLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(18)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(18)]]
    })
  }
}
