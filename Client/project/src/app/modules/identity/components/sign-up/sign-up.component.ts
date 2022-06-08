import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IdentityService } from 'src/app/modules/shared/services/identity.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {
  public formModel: FormGroup = this.formBuilder.group({
    username: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    passwords: this.formBuilder.group({
      password: ['', [Validators.required, Validators.minLength(8), Validators.pattern(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9\d]).{0,}$/)]],
      confirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })
  });

  constructor(private router: Router, private formBuilder: FormBuilder, private identityService: IdentityService) { }

  public ngOnInit() {
  }

  public comparePasswords(formGroup: FormGroup): void {
    let confirmPwdCtrl = formGroup.get('confirmPassword')!;
    
    if (confirmPwdCtrl.errors == null || 'passwordMismatch' in confirmPwdCtrl.errors) {
      if (formGroup.get('password')!.value != confirmPwdCtrl.value)
        confirmPwdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPwdCtrl.setErrors(null);
    }
  }

  public async onSubmit(): Promise<void> {
    return await this.identityService.sendSignUpRequest({
      username: this.formModel.value.username,
      email: this.formModel.value.email,
      password: this.formModel.value.passwords.password
    });
  }
}
