import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CookieService } from 'ngx-cookie';
import { IdentityService } from 'src/app/modules/shared/services/identity.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public formModel: FormGroup = this.formBuilder.group({
    usernameEmail: ['', Validators.required],
    password: ['', Validators.required]
  });

  constructor(private formBuilder: FormBuilder, private identityService: IdentityService, private cookieService: CookieService) { }

  public ngOnInit(): void {
  }

  public async onSubmit(): Promise<void> {
    return this.identityService.sendLoginRequest({usernameEmail: this.formModel.value.usernameEmail, password: this.formModel.value.password}).then((response: any) => {
      this.cookieService.put(environment.authTokenHeaderKey, response.content.token)
    });
  }
}
